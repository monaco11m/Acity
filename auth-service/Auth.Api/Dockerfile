FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY auth-service/Auth.Api/Auth.Api.csproj auth-service/Auth.Api/
COPY auth-service/Auth.Application/Auth.Application.csproj auth-service/Auth.Application/
COPY auth-service/Auth.Domain/Auth.Domain.csproj auth-service/Auth.Domain/
COPY auth-service/Auth.Infrastructure/Auth.Infrastructure.csproj auth-service/Auth.Infrastructure/
RUN dotnet restore auth-service/Auth.Api/Auth.Api.csproj

COPY . .
WORKDIR /src/auth-service/Auth.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
