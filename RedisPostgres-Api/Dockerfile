FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RedisPostgres-Api/RedisPostgres-Api.csproj", "RedisPostgres-Api/"]
RUN dotnet restore "./RedisPostgres-Api/./RedisPostgres-Api.csproj"
COPY . .
WORKDIR "/src/RedisPostgres-Api"
RUN dotnet build "./RedisPostgres-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RedisPostgres-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisPostgres-Api.dll"]