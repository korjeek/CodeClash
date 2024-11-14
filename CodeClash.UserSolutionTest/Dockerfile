FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CodeClash.UserSolutionTest/CodeClash.UserSolutionTest.csproj", "CodeClash.UserSolutionTest/"]
RUN dotnet restore "CodeClash.UserSolutionTest/CodeClash.UserSolutionTest.csproj"
COPY . .
WORKDIR "/src/CodeClash.UserSolutionTest"
RUN dotnet build "CodeClash.UserSolutionTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CodeClash.UserSolutionTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeClash.UserSolutionTest.dll"]
