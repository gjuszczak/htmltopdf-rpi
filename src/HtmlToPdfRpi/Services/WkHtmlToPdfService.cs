using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlToPdfRpi.Services
{
    public interface IWkHtmlToPdfService
    {
        Task<Stream> FromUrlAsync(string url, CancellationToken cancellationToken = default);
    }

    public class WkHtmlToPdfService : IWkHtmlToPdfService
    {
        public async Task<Stream> FromUrlAsync(string url, CancellationToken cancellationToken)
        {
            // need to use tempfile because of bug https://github.com/wkhtmltopdf/wkhtmltopdf/issues/3119
            var tempFilePath = $"{Path.GetTempPath()}{Guid.NewGuid()}.pdf";
            var procInfo = new ProcessStartInfo
            {
                FileName = "/usr/local/bin/wkhtmltopdf",
                Arguments = $"-q {url} {tempFilePath}",
                UseShellExecute = false,
                RedirectStandardError = true,
            };
            using var proc = Process.Start(procInfo);
            using var msErr = new MemoryStream();
            using var msErrReader = new StreamReader(msErr);
            try
            {
                await proc.StandardError.BaseStream.CopyToAsync(msErr, cancellationToken);
                msErr.Seek(0, SeekOrigin.Begin);
                await proc.WaitForExitAsync(cancellationToken);
            }
            finally
            {
                if ((proc?.HasExited ?? true) == false)
                {
                    proc.Kill();
                }
            }

            var msOut = new MemoryStream();
            using (var fs = File.OpenRead(tempFilePath))
            {
                await fs.CopyToAsync(msOut, cancellationToken);
            }
            File.Delete(tempFilePath);
            msOut.Seek(0, SeekOrigin.Begin);
            if (msOut.Length == 0)
            {
                throw new Exception(msErrReader.ReadToEnd());
            }
            return msOut;
        }
    }
}