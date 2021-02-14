#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y wget
RUN wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox_0.12.6-1.buster_amd64.deb
RUN apt install -y ./wkhtmltox_0.12.6-1.buster_amd64.deb
RUN rm ./wkhtmltox_0.12.6-1.buster_amd64.deb
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HtmlToPdfRpi/HtmlToPdfRpi.csproj", "HtmlToPdfRpi/"]
RUN dotnet restore "HtmlToPdfRpi/HtmlToPdfRpi.csproj"
COPY . .
WORKDIR "/src/HtmlToPdfRpi"
RUN dotnet build "HtmlToPdfRpi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HtmlToPdfRpi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HtmlToPdfRpi.dll"]