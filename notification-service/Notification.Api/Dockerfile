FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY notification-service/Notification.Api/Notification.Api.csproj notification-service/Notification.Api/
COPY notification-service/Notification.Application/Notification.Application.csproj notification-service/Notification.Application/
RUN dotnet restore notification-service/Notification.Api/Notification.Api.csproj

COPY . .
WORKDIR /src/notification-service/Notification.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Notification.Api.dll"]
