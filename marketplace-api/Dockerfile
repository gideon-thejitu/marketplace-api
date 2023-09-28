FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["marketplace-api.csproj", "marketplace-api/"]
RUN dotnet restore "marketplace-api/marketplace-api.csproj"
COPY . .
WORKDIR "/src/marketplace-api"
RUN dotnet build "marketplace-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "marketplace-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "marketplace-api.dll"]
