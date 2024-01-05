#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/API/UniIdentity.WebApp/UniIdentity.WebApp.csproj", "API/UniIdentity.WebApp/"]
COPY ["src/API/UniIdentity.Application/UniIdentity.Application.csproj", "API/UniIdentity.Application/"]
COPY ["src/API/UniIdentity.Domain/UniIdentity.Domain.csproj", "API/UniIdentity.Domain/"]
COPY ["src/API/UniIdentity.Infrastructure/UniIdentity.Infrastructure.csproj", "API/UniIdentity.Infrastructure/"]
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