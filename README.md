# Gestión de Tienda

Aplicación de consola desarrollada en .NET para gestionar productos, inventario, descuentos y el total de un carrito.

## Participantes

- Rodrigo Exequiel Mendoza
- Alvaro Estanislao Figueroa
- Ivan Gerardo Risso Patron

> **Aclaración:** los commits del repositorio fueron realizados únicamente por Alvaro Estanislao Figueroa. El trabajo se desarrolló de manera conjunta entre los participantes mediante Discord.

## Requisitos

- .NET SDK 10.0 o superior.

Para comprobar la versión instalada:

```bash
dotnet --version
```

## Ejecutar el proyecto

Desde la carpeta raíz del repositorio:

```bash
dotnet run --project GestionTienda.csproj
```

## Probar los tests

1. Restaurar las dependencias:

   ```bash
   dotnet restore GestionTienda.slnx
   ```

2. Ejecutar todos los tests:

   ```bash
   dotnet test GestionTienda.slnx
   ```

También se pueden ejecutar directamente desde la carpeta del proyecto:

```bash
cd GestionTienda.Tests
dotnet test
```

Para obtener una salida más detallada:

```bash
dotnet test GestionTienda.Tests/GestionTienda.Tests.csproj --verbosity normal
```

El proyecto de tests utiliza xUnit y Moq. Los casos cubren el agregado, búsqueda y eliminación de productos, la aplicación de descuentos y el cálculo del total del carrito.
