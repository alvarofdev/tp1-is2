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


## Pruebas con Excepciones

Preguntas conceptuales 

Podría haber escrito las pruebas primero antes de modificar el código de la aplicación?

Sí, es totalmente posible. Tal como se establece en la teoría sobre el Procedimiento de prueba de Unidad, el diseño de las pruebas se puede realizar antes o después de la codificación de la unidad. Además, en los tableros de metodologías ágiles (como se observa en las tareas "Write failing test" previas a la implementación), esta práctica se formaliza como TDD (Test-Driven Development o Desarrollo Guiado por Pruebas).

¿Cómo sería el proceso de escribir primero los tests? Describe el proceso con tus palabras.

1. Diseño y escritura de la prueba fallida (Fase Roja / Write failing test): Sabiendo que toda prueba requiere definir los resultados esperados, primero escribimos los métodos de prueba con el atributo [Fact] de xUnit en TiendaTests.cs (y ProductoTests.cs). Definimos los tests buscarProductoInexistente() esperando un Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pizza")), eliminarProductoInexistente() esperando un Assert.Throws<KeyNotFoundException>(() => tienda.eliminarProducto("Pizza")), y la prueba de actualizarPrecio() con un valor negativo esperando un ArgumentOutOfRangeException. Al ejecutar dotnet test en este momento, las pruebas fallan (o ni siquiera compilan si actualizarPrecio aún no fue declarado), ya que el código base aún no lanza dichas excepciones.   
2. Codificación mínima de la unidad (Fase Verde / Implementación): Vamos al espacio de nombres GestionTienda.Entities (Producto.cs y Tienda.cs), que representan nuestras unidades bajo prueba (UUT), y escribimos el código necesario para satisfacer los tests:   
- En Producto.cs, implementamos actualizarPrecio(decimal precio) apoyado en el método privado validarPrecio(precio), el cual evalúa la condición if (precio < 0) y lanza ArgumentOutOfRangeException.   - En Tienda.cs, modificamos buscarProducto para que si FirstOrDefault devuelve null, lance ArgumentNullException, y modificamos eliminarProducto para que si Find devuelve null, lance KeyNotFoundException. Al volver a ejecutar xUnit, todas las pruebas pasan (quedan en verde).
3. Refactorización y Pruebas de Regresión: Con las pruebas pasando, podemos mejorar el diseño interno del código (reestructuración de código) —por ejemplo, centralizando la validación del precio en el método privado validarPrecio(precio) dentro de Producto.cs para reutilizarlo tanto en el constructor como en actualizarPrecio, o haciendo que aplicarDescuento reutilice buscarProducto—. Luego, volvemos a correr toda la suite de TiendaTests.cs como pruebas de regresión para asegurarnos de que los cambios no introdujeron efectos colaterales no deseados (side effects) en las pruebas previas.

## Uso de dobles para aislar unidades bajo prueba

En lo que va del trabajo práctico, ¿puedes identificar 'Controladores' y 'Resguardos'?

En nuestro código: La clase TiendaTests (TiendaTests.cs) junto con el motor de ejecución de xUnit actúan como el Controlador. Cada método con [Fact] (como agregarProducto(), buscarProductoCorrecto() o aplicarDescuentoCalculaNuevoPrecioYActualizaProducto()) instancia o solicita la Tienda, invoca sus métodos pasándole parámetros y ejecuta las aserciones (Assert.Equal, Assert.Same, Assert.Throws) para evaluar los resultados.

Los objetos Mock<Producto> creados mediante la librería Moq (tanto en TiendaFixture.crearProductoMock como en TiendaTests.cs) actúan como Resguardos. 

¿Qué es un “test double”? 

Es el término general que engloba a cualquier objeto simulado que reemplaza a un componente real (una dependencia o clase colaboradora) durante el testing.

¿Hay otros nombres para los objetos/funciones simulados?

Sí. Aunque en la teoría tradicional se los agrupa bajo el término Resguardos (Stubs) y librerías como Moq usan la clase genérica Mock<T> para crearlos, conceptualmente reciben distintos nombres según cómo se utilicen en la prueba:
-Stub: Es un objeto simulado que devuelve respuestas preprogramadas ("enlatadas") ante llamadas de la unidad bajo prueba.
-Mock: Es un doble de prueba sobre el cual se definen expectativas de interacción y comportamiento.
-Spy: Es un doble que registra o captura los valores con los que fue llamado un método para inspeccionarlos luego.
-Dummy: Un objeto que solo se pasa como parámetro para cumplir con la firma de un método, pero que no se utiliza.
Fake: Un objeto con una implementación funcional real pero simplificada y liviana pensada solo para el entorno de pruebas.