FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PolizaExpress.Web.API/PolizaExpress.Web.API.csproj", "PolizaExpress.Web.API/"]
RUN dotnet restore "PolizaExpress.Web.API/PolizaExpress.Web.API.csproj"
COPY . .
WORKDIR "/src/PolizaExpress.Web.API"
RUN dotnet build "PolizaExpress.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PolizaExpress.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PolizaExpress.Web.API.dll"]