FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ship.API/Ship.API.csproj", "Ship.API/"]
COPY ["Ship.Core/Ship.Core.csproj", "Ship.Core/"]
COPY ["Ship.Infrastructure/Ship.Infrastructure.csproj", "Ship.Infrastructure/"]
RUN dotnet restore "Ship.API/Ship.API.csproj"
COPY . .
WORKDIR "/src/Ship.API"
RUN dotnet build "Ship.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ship.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ship.API.dll"]