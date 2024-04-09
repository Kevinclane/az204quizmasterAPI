FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["az204quizmasterAPI/az204quizmasterAPI.csproj", "az204quizmasterAPI/"]
RUN dotnet restore "az204quizmasterAPI/az204quizmasterAPI.csproj"
COPY . .
WORKDIR "/src/az204quizmasterAPI"
RUN dotnet build "az204quizmasterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "az204quizmasterAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "az204quizmasterAPI.dll"]