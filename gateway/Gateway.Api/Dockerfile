FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY gateway/Gateway.Api/Gateway.Api.csproj gateway/Gateway.Api/
RUN dotnet restore gateway/Gateway.Api/Gateway.Api.csproj

COPY . .
WORKDIR /src/gateway/Gateway.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Api.dll"]
