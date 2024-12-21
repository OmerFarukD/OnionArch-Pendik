FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/projects/ECommerce.WebAPI/ECommerce.WebAPI.csproj", "src/projects/ECommerce.WebAPI/"]
COPY ["src/core/Core.CrossCuttingConcerns/Core.CrossCuttingConcerns.csproj", "src/core/Core.CrossCuttingConcerns/"]
COPY ["src/projects/ECommerce.Application/ECommerce.Application.csproj", "src/projects/ECommerce.Application/"]
COPY ["src/core/Core.Application/Core.Application.csproj", "src/core/Core.Application/"]
COPY ["src/core/Core.Security/Core.Security.csproj", "src/core/Core.Security/"]
COPY ["src/core/Core.Persistence/Core.Persistence.csproj", "src/core/Core.Persistence/"]
COPY ["src/projects/ECommerce.Domain/ECommerce.Domain.csproj", "src/projects/ECommerce.Domain/"]
COPY ["src/projects/ECommerce.Persistence/ECommerce.Persistence.csproj", "src/projects/ECommerce.Persistence/"]
COPY ["src/projects/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "src/projects/ECommerce.Infrastructure/"]
RUN dotnet restore "src/projects/ECommerce.WebAPI/ECommerce.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/projects/ECommerce.WebAPI"
RUN dotnet build "ECommerce.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.WebAPI.dll"]
