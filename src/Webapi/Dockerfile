FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY CustomerManagement.Api/*.csproj ./CustomerManagement.Api/
COPY CustomerManagement.Core/Domain/*.csproj ./CustomerManagement.Core/Domain/
COPY CustomerManagement.Core/Application/*.csproj ./CustomerManagement.Core/Application/
COPY CustomerManagement.Infrastructure/*.csproj ./CustomerManagement.Infrastructure/
COPY CustomerManagement.Utility/*.csproj ./CustomerManagement.Utility/

RUN dotnet restore

COPY CustomerManagement.Api/ ./CustomerManagement.Api/
COPY CustomerManagement.Core/Domain/ ./CustomerManagement.Core/Domain/
COPY CustomerManagement.Core/Application/ ./CustomerManagement.Core/Application/
COPY CustomerManagement.Infrastructure/ ./CustomerManagement.Infrastructure/
COPY CustomerManagement.Utility/ ./CustomerManagement.Utility/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CustomerManagement.Api.dll"]