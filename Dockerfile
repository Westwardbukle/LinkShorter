FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/LinkShorter.Api/LinkShorter.Api.csproj", "LinkShorter.Api/"]
COPY ["src/LinkShorter.BL/LinkShorter.BL.csproj", "LinkShorter.BL/"]
COPY ["src/LinkShorter.Domain/LinkShorter.Domain.csproj", "LinkShorter.Domain/"]
COPY ["src/LinkShorter.Repository/LinkShorter.Repository.csproj", "LinkShorter.Repository/"]

RUN dotnet restore "LinkShorter/LinkShorter.csproj"
COPY . .
WORKDIR "/src/LinkShorter"
RUN dotnet build "LinkShorter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LinkShorter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinkShorter.dll"]
