#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HeathCheckAPI/HeathCheckAPI.csproj", "HeathCheckAPI/"]
RUN dotnet restore "HeathCheckAPI/HeathCheckAPI.csproj"
COPY . .
WORKDIR "/src/HeathCheckAPI"
RUN dotnet build "HeathCheckAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HeathCheckAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "HeathCheckAPI.dll"]