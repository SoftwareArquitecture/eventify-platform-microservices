# Etapa de Construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo el csproj del Gateway
COPY ["Eventify.Platform.API/Eventify.Platform.API.csproj", "Eventify.Platform.API/"]

# Restaurar
RUN dotnet restore "Eventify.Platform.API/Eventify.Platform.API.csproj"

# Copiar todo lo demás
COPY . .

# Compilar
WORKDIR "/src/Eventify.Platform.API"
RUN dotnet build "Eventify.Platform.API.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "Eventify.Platform.API.csproj" -c Release -o /app/publish

# Etapa Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eventify.Platform.API.dll"]
