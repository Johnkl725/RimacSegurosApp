# Utilidades Docker para RimacSegurosApp

Este directorio contiene scripts y utilidades para facilitar el trabajo con Docker.

## 📁 Contenido

### Scripts de Inicio

Scripts útiles para iniciar y gestionar los contenedores.

## 🚀 Comandos Rápidos

### Iniciar la aplicación
```bash
docker-compose up -d
```

### Ver logs
```bash
docker-compose logs -f
```

### Detener la aplicación
```bash
docker-compose down
```

### Reiniciar con base de datos limpia
```bash
docker-compose down -v
docker-compose up --build
```

### Acceder a SQL Server
```bash
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd"
```

## 🔧 Troubleshooting

### Puerto ya en uso
Si el puerto 8080 o 1433 están en uso:

1. Editar `docker-compose.yml`
2. Cambiar el mapeo de puertos, ejemplo: `"8081:8080"`

### Error de conexión a base de datos
Esperar a que SQL Server termine de inicializar:

```bash
docker-compose logs sqlserver
```

Buscar el mensaje: "SQL Server is now ready for client connections"

### Reconstruir imagen
Si hay cambios en el código:

```bash
docker-compose up --build
```
