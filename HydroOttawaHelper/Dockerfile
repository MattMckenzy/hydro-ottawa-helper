FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["HydroOttawaHelper/HydroOttawaHelper.csproj", "HydroOttawaHelper/"]
RUN dotnet restore "HydroOttawaHelper/HydroOttawaHelper.csproj"
COPY . .
WORKDIR "/src/HydroOttawaHelper"
RUN dotnet build "HydroOttawaHelper.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "HydroOttawaHelper.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HydroOttawaHelper.dll"]
