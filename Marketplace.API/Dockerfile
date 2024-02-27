FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Marketplace.API/Marketplace.API.csproj", "Marketplace.API/"]
COPY . .
RUN dotnet restore "Marketplace.API/Marketplace.API.csproj"
WORKDIR "/src/Marketplace.API"
#
#RUN echo $PATH
#
#RUN dotnet tool install --version 7.0.15 --global dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"
#
#RUN echo $PATH
#
#RUN #dotnet-ef database update -p /src/Marketplace.Infrastructure/Marketplace.Infrastructure.csproj -s /src/Marketplace.API/Marketplace.API.csproj  -- --environment Development

RUN dotnet build "Marketplace.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Marketplace.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Marketplace.API.dll"]
