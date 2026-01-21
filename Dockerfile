# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["AplicaciónWeb/AplicaciónWeb.csproj", "AplicaciónWeb/"]
COPY ["AccesoDatos/AccesoDatos.csproj", "AccesoDatos/"]
COPY ["LogicaNegocio/LogicaNegocio.csproj", "LogicaNegocio/"]
COPY ["DbContext/MiAplicacion.Data.csproj", "DbContext/"]
COPY ["EntidadesProyecto/EntidadesProyecto.csproj", "EntidadesProyecto/"]

RUN dotnet restore "AplicaciónWeb/AplicaciónWeb.csproj"

# Copiar el resto de los archivos y compilar
COPY . .
WORKDIR "/src/AplicaciónWeb"
RUN dotnet build "AplicaciónWeb.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "AplicaciónWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Crear usuario sin privilegios
RUN useradd -m -u 1000 appuser && chown -R appuser:appuser /app
USER appuser

# Copiar archivos publicados
COPY --from=publish /app/publish .

# Exponer puerto
EXPOSE 8080

# Variables de entorno
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Entrypoint
ENTRYPOINT ["dotnet", "AplicaciónWeb.dll"]
