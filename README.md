# Gestión de Tienda

Proyecto desarrollado en .NET para modelar la gestión de productos, inventario, descuentos y el total de un carrito.

> **Nota:** actualmente `Program.cs` solo muestra `Hello, World!`. La lógica de gestión se encuentra implementada en las clases de dominio `Producto` y `Tienda`, y se verifica mediante tests automatizados.

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

## Pruebas de unidad e integración

### ¿Puedes identificar pruebas de unidad y de integración en la práctica realizada?

Sí. En la práctica se realizaron ambos tipos de pruebas:

- **Pruebas de unidad:** verifican una operación de manera aislada. Por ejemplo, `agregarProducto`, `buscarProductoCorrecto`, `buscarProductoInexistente`, `eliminarProducto`, `eliminarProductoInexistente` y `aplicarDescuentoCalculaNuevoPrecioYActualizaProducto` prueban comportamientos específicos de `Tienda`. En algunos casos se utilizan objetos `Mock<Producto>` creados con Moq para aislar la unidad bajo prueba de sus dependencias.
- **Prueba de integración:** `calcularTotalCarritoIntegraProductosYDescuentos` verifica la colaboración entre varias operaciones. El test agrega productos reales a la tienda, aplica un descuento mediante `Tienda.aplicarDescuento(string nombre, decimal descuento)` y luego calcula el total con `Tienda.calcular_total_carrito(List<string> carrito)`. Por eso comprueba el flujo integrado entre el inventario, los precios actualizados y el carrito.

La diferencia principal es que una prueba de unidad comprueba una responsabilidad puntual y aislada, mientras que una prueba de integración verifica que varias partes del sistema funcionen correctamente juntas.

## Pruebas con excepciones

### ¿Podría haber escrito las pruebas primero antes de modificar el código de la aplicación?

Sí, es totalmente posible. Tal como se establece en la teoría sobre el Procedimiento de prueba de Unidad, el diseño de las pruebas se puede realizar antes o después de la codificación de la unidad. Además, en los tableros de metodologías ágiles (como se observa en las tareas "Write failing test" previas a la implementación), esta práctica se formaliza como TDD (Test-Driven Development o Desarrollo Guiado por Pruebas).

### ¿Cómo sería el proceso de escribir primero los tests?

1. **Fase roja:** definir el comportamiento esperado y escribir los tests con `[Fact]` en `TiendaTests.cs`. En este proyecto, los casos existentes incluyen `buscarProductoInexistente`, que espera una `ArgumentNullException`, y `eliminarProductoInexistente`, que espera una `KeyNotFoundException`. También se puede agregar un caso para `Producto.actualizarPrecio(decimal precio)` con un precio negativo, esperando una `ArgumentOutOfRangeException`.
2. **Fase verde:** implementar el código mínimo en `entities/producto.cs` y `entities/tienda.cs` para satisfacer esos tests. El constructor de `Producto` y `Producto.actualizarPrecio(decimal precio)` validan que el precio no sea negativo. Por su parte, `Tienda.buscarProducto(string nombre)` y `Tienda.eliminarProducto(string nombre)` lanzan las excepciones correspondientes cuando el producto no existe.
3. **Refactorización y regresión:** con los tests en verde, mejorar la estructura interna sin cambiar el comportamiento. Por ejemplo, reutilizar la validación privada del precio en el constructor y en `actualizarPrecio`. Finalmente, ejecutar nuevamente toda la suite para comprobar que no se introdujeron regresiones.

## Uso de dobles para aislar unidades bajo prueba

### ¿Puedes identificar controladores y resguardos?

En nuestro código, `TiendaTests` junto con el motor de xUnit actúan como el **controlador**. Cada método con `[Fact]` instancia o prepara una `Tienda`, invoca sus métodos y evalúa el resultado mediante aserciones como `Assert.Equal`, `Assert.Same` y `Assert.Throws`.

Los objetos `Mock<Producto>` creados con Moq, especialmente mediante `TiendaFixture.crearProductoMock(string nombre, decimal precio)`, actúan como **resguardos** o dobles de prueba.

### ¿Qué es un test double?

Es el término general que engloba a cualquier objeto simulado que reemplaza a un componente real (una dependencia o clase colaboradora) durante el testing.

### ¿Hay otros nombres para los objetos simulados?

Sí. Aunque en la teoría tradicional se los agrupa bajo el término resguardos (stubs) y librerías como Moq usan la clase genérica `Mock<T>` para crearlos, conceptualmente reciben distintos nombres según cómo se utilicen en la prueba:

- **Stub:** objeto simulado que devuelve respuestas preprogramadas ante llamadas de la unidad bajo prueba.
- **Mock:** doble de prueba sobre el cual se definen expectativas de interacción y comportamiento.
- **Spy:** doble que registra o captura los valores con los que fue llamado un método para inspeccionarlos después.
- **Dummy:** objeto que se pasa como parámetro para cumplir con la firma de un método, pero que no se utiliza.
- **Fake:** objeto con una implementación funcional real, pero simplificada y liviana, pensada para el entorno de pruebas.

### ¿Qué es un fixture?

Un fixture es una preparación previa que permite dejar listo el entorno de una prueba. Por ejemplo, crear una tienda y cargar algunos productos antes de ejecutar un test.

### ¿Qué ventajas tiene usar fixtures? ¿Qué enfoque aplicamos: caja negra o caja blanca?

Los fixtures permiten reutilizar datos de prueba, evitar repetir código y mantener una preparación similar entre las pruebas. En nuestro caso, usamos principalmente un enfoque de caja negra, porque verificamos los resultados de las operaciones de la tienda sin tener que conocer cómo están implementadas internamente.

### ¿Qué son Setup y Teardown en testing?

- **Setup:** tareas realizadas antes de ejecutar una prueba, como preparar una tienda y cargar los productos necesarios.
- **Teardown:** tareas realizadas después de la prueba, como limpiar los datos utilizados o liberar recursos.

### ¿Realizó una prueba de cobertura completa? ¿Qué tipo de cobertura utilizó?

En nuestro caso, no podemos afirmar que realizamos una cobertura completa de todo el sistema, porque no medimos la cobertura con una herramienta específica. Sí realizamos pruebas que cubren los principales caminos de las clases Producto y Tienda, incluyendo casos correctos y casos donde se producen excepciones.

El tipo de cobertura que podemos identificar principalmente es la cobertura de sentencias, ya que nuestras pruebas ejecutan las diferentes instrucciones de los métodos, como agregar, buscar, eliminar, actualizar precios, aplicar descuentos y calcular el total.

### ¿Puede describir una situación de integración ascendente para este caso?

Un ejemplo sería comenzar probando la operación más simple, `Producto.actualizarPrecio(decimal precio)`, y luego probar `Tienda.aplicarDescuento(string nombre, decimal descuento)`, que busca el producto y actualiza su precio. Finalmente, se podría probar `Tienda.calcular_total_carrito(List<string> carrito)`, que utiliza la búsqueda de productos y sus precios.

De esta manera, se empieza probando los componentes de menor nivel y luego se integran con componentes de mayor nivel hasta comprobar el funcionamiento de la tienda. Esto representa una estrategia de integración ascendente.
