#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WorkerHealthCheck/WorkerHealthCheck.csproj", "WorkerHealthCheck/"]
RUN dotnet restore "WorkerHealthCheck/WorkerHealthCheck.csproj"
COPY . .
WORKDIR "/src/WorkerHealthCheck"
RUN dotnet build "WorkerHealthCheck.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerHealthCheck.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt update && \
    apt upgrade -y && \
    rm -rf /var/lib/apt/lists/*
ENTRYPOINT ["dotnet", "WorkerHealthCheck.dll"]