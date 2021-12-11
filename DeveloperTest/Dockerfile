#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DeveloperTest/DeveloperTest.csproj", "DeveloperTest/"]
COPY ["DeveloperTest/DeveloperTest.Application.csproj", "DeveloperTest/"]
COPY ["DeveloperTest/DeveloperTest.Domain.csproj", "DeveloperTest/"]
COPY ["DeveloperTest/DeveloperTest.Infrastructure.Storage.csproj", "DeveloperTest/"]
RUN dotnet restore "DeveloperTest/DeveloperTest.csproj"
COPY . .
WORKDIR "/src/DeveloperTest"
RUN dotnet build "DeveloperTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeveloperTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveloperTest.dll"]