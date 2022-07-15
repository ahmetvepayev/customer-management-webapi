FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY ReportService.Worker/*.csproj ./ReportService.Worker/
COPY ReportService.Core/Domain/*.csproj ./ReportService.Core/Domain/
COPY ReportService.Core/Application/*.csproj ./ReportService.Core/Application/
COPY ReportService.Infrastructure/*.csproj ./ReportService.Infrastructure/

RUN dotnet restore

COPY ReportService.Worker/ ./ReportService.Worker/
COPY ReportService.Core/Domain/ ./ReportService.Core/Domain/
COPY ReportService.Core/Application/ ./ReportService.Core/Application/
COPY ReportService.Infrastructure/ ./ReportService.Infrastructure/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "ReportService.Worker.dll"]