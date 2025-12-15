FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY control-service/Control.Api/Control.Api.csproj control-service/Control.Api/
COPY control-service/Control.Application/Control.Application.csproj control-service/Control.Application/
COPY control-service/Control.Domain/Control.Domain.csproj control-service/Control.Domain/
COPY control-service/Control.Infrastructure/Control.Infrastructure.csproj control-service/Control.Infrastructure/
RUN dotnet restore control-service/Control.Api/Control.Api.csproj

COPY . .
WORKDIR /src/control-service/Control.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Control.Api.dll"]
