FROM --platform=linux/arm/v7 mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y wget
RUN wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox_0.12.6-1.raspberrypi.buster_armhf.deb
RUN apt install -y ./wkhtmltox_0.12.6-1.raspberrypi.buster_armhf.deb
RUN rm ./wkhtmltox_0.12.6-1.raspberrypi.buster_armhf.deb
WORKDIR /app
COPY out .
EXPOSE 80
ENTRYPOINT ["dotnet", "HtmlToPdfRpi.dll"]
