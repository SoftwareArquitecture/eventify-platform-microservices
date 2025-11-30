# ---------------------------------------
# CAMBIA ESTA VARIABLE POR EL NOMBRE DE TU CARPETA (IAM, Planning, etc.)
# Ejemplo: Eventify.Services.IAM
ARG SERVICE_NAME=Eventify.Services.IAM
# ---------------------------------------

# Etapa de Construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG SERVICE_NAME
WORKDIR /src

# 1. Copiar los csproj (El del servicio y el Shared)
COPY ["${SERVICE_NAME}/${SERVICE_NAME}.csproj", "${SERVICE_NAME}/"]
COPY ["Eventify.Shared/Eventify.Shared.csproj", "Eventify.Shared/"]

# 2. Restaurar dependencias
RUN dotnet restore "${SERVICE_NAME}/${SERVICE_NAME}.csproj"

# 3. Copiar el resto del código
COPY . .

# 4. Compilar
WORKDIR "/src/${SERVICE_NAME}"
RUN dotnet build "${SERVICE_NAME}.csproj" -c Release -o /app/build

# 5. Publicar
FROM build AS publish
ARG SERVICE_NAME
RUN dotnet publish "${SERVICE_NAME}.csproj" -c Release -o /app/publish

# Etapa Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
ARG SERVICE_NAME
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Truco para que el ENTRYPOINT sea dinámico según el nombre del servicio
ENV DLL_NAME="${SERVICE_NAME}.dll"
ENTRYPOINT dotnet $DLL_NAME
