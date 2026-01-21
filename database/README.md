# Scripts de Base de Datos

## 📁 Estructura

- **init.sql** - Script principal para inicializar la base de datos `BDSeguros`
- **backups/** - Carpeta con versiones anteriores de scripts SQL

## 🚀 Uso

### Con Docker

El archivo `init.sql` se carga automáticamente cuando ejecutas:

```bash
docker-compose up
```

El contenedor de SQL Server ejecutará este script al iniciar por primera vez.

### Manual

Si prefieres configurar la base de datos manualmente:

1. Abrir SQL Server Management Studio (SSMS)
2. Conectar a tu instancia de SQL Server
3. Ejecutar el script `init.sql`

## 📝 Notas

- El script `init.sql` es una copia de `7diciembre.sql` (versión más reciente)
- Los scripts en `backups/` son versiones anteriores guardadas por referencia
- La base de datos se llama `BDSeguros`

## 🔄 Actualizar Script

Si necesitas actualizar el script de inicialización:

1. Editar `init.sql`
2. Reconstruir los contenedores Docker:

```bash
docker-compose down -v
docker-compose up --build
```

⚠️ **Advertencia**: `docker-compose down -v` eliminará todos los datos existentes en la base de datos.
