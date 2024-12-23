FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CodeClash.API/CodeClash.API.csproj", "CodeClash.API/"]
COPY ["CodeClash.Application/CodeClash.Application.csproj", "CodeClash.Application/"]
COPY ["CodeClash.Core/CodeClash.Core.csproj", "CodeClash.Core/"]
COPY ["CodeClash.Infrastructure/CodeClash.Infrastructure.csproj", "CodeClash.Infrastructure/"]
COPY ["CodeClash.Persistence/CodeClash.Persistence.csproj", "CodeClash.Persistence/"]
COPY ["CodeClash.UserSolutionTest/CodeClash.UserSolutionTest.csproj", "CodeClash.UserSolutionTest/"]
RUN dotnet restore "CodeClash.API/CodeClash.API.csproj"
COPY . .
WORKDIR "/src/CodeClash.API"
RUN dotnet build "CodeClash.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CodeClash.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeClash.API.dll"]