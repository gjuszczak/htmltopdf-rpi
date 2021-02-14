using HtmlToPdfRpi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlToPdfRpi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HtmlToPdfController : ControllerBase
    {
        private readonly IWkHtmlToPdfService _htmlToPdf;

        public HtmlToPdfController(IWkHtmlToPdfService htmlToPdf)
        {
            _htmlToPdf = htmlToPdf;
        }

        [HttpGet]
        public async Task<IActionResult> FromUrl([FromQuery] string url, CancellationToken cancellationToken)
        {
            var stream = await _htmlToPdf.FromUrlAsync(url, cancellationToken);
            return File(stream, "application/pdf");
        }
    }
}
