FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API/UniIdentity.WebApp/UniIdentity.WebApp.csproj", "API/UniIdentity.WebApp/"]
COPY ["API/UniIdentity.Application/UniIdentity.Application.csproj", "API/UniIdentity.Application/"]
COPY ["API/UniIdentity.Domain/UniIdentity.Domain.csproj", "API/UniIdentity.Domain/"]
COPY ["API/UniIdentity.Infrastructure/UniIdentity.Infrastructure.csproj", "API/UniIdentity.Infrastructure/"]
RUN dotnet restore "API/UniIdentity.WebApp/UniIdentity.WebApp.csproj"
COPY . .
WORKDIR "/src/API/UniIdentity.WebApp"
RUN dotnet build "UniIdentity.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UniIdentity.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniIdentity.WebApp.dll"]
