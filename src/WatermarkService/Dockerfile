FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY WatermarkService.Worker/*.csproj ./WatermarkService.Worker/
COPY WatermarkService.Core/Domain/*.csproj ./WatermarkService.Core/Domain/
COPY WatermarkService.Core/Application/*.csproj ./WatermarkService.Core/Application/
COPY WatermarkService.Infrastructure/*.csproj ./WatermarkService.Infrastructure/

RUN dotnet restore

COPY WatermarkService.Worker/ ./WatermarkService.Worker/
COPY WatermarkService.Core/Domain/ ./WatermarkService.Core/Domain/
COPY WatermarkService.Core/Application/ ./WatermarkService.Core/Application/
COPY WatermarkService.Infrastructure/ ./WatermarkService.Infrastructure/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "WatermarkService.Worker.dll"]