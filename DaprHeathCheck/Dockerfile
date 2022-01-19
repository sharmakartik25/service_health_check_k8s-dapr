#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DaprHeathCheck/DaprHeathCheck.csproj", "DaprHeathCheck/"]
RUN dotnet restore "DaprHeathCheck/DaprHeathCheck.csproj"
COPY . .
WORKDIR "/src/DaprHeathCheck"
RUN dotnet build "DaprHeathCheck.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaprHeathCheck.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaprHeathCheck.dll"]