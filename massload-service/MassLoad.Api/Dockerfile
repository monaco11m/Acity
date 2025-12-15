FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY massload-service/MassLoad.Api/MassLoad.Api.csproj massload-service/MassLoad.Api/
COPY massload-service/MassLoad.Application/MassLoad.Application.csproj massload-service/MassLoad.Application/
RUN dotnet restore massload-service/MassLoad.Api/MassLoad.Api.csproj

COPY . .
WORKDIR /src/massload-service/MassLoad.Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MassLoad.Api.dll"]
