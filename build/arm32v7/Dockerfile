ARG BASE_IMAGE=wkhtmltopdf_and_aspnet_image
FROM ${BASE_IMAGE}
WORKDIR /app
COPY out .
EXPOSE 80
ENTRYPOINT ["dotnet", "HtmlToPdfRpi.dll"]
