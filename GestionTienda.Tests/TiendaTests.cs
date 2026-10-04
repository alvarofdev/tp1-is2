using GestionTienda.Entities;
using Moq;

namespace GestionTienda.Tests;

public class TiendaTests
{
    private static Mock<Producto> CrearProductoMock(string nombre, decimal precio)
    {
        var productoMock = new Mock<Producto>(nombre, precio, "categoria");
        productoMock.SetupGet(producto => producto.Nombre).Returns(nombre);
        productoMock.SetupGet(producto => producto.Precio).Returns(precio);

        return productoMock;
    }

    [Fact]
    public void agregarProducto()
    {
        Tienda tienda = new Tienda();
        var productoMock = CrearProductoMock("Pan", 1000m);

        var inventario = tienda.agregarProducto(productoMock.Object);

        Assert.Single(inventario);
        Assert.Same(productoMock.Object, inventario[0]);
    }

    [Fact]
    public void buscarProductoCorrecto()
    {
        Tienda tienda = new Tienda();

        var producto1Mock = CrearProductoMock("Pan", 1000m);
        var producto2Mock = CrearProductoMock("Carne", 20000m);

        tienda.agregarProducto(producto1Mock.Object);
        tienda.agregarProducto(producto2Mock.Object);

        var buscar = tienda.buscarProducto(producto2Mock.Object.Nombre);

        Assert.Same(producto2Mock.Object, buscar);
    }

    [Fact]
    public void buscarProductoInexistente()
    {
        Tienda tienda = new Tienda();

        var producto1Mock = CrearProductoMock("Pan", 1000m);
        var producto2Mock = CrearProductoMock("Carne", 20000m);

        tienda.agregarProducto(producto1Mock.Object);
        tienda.agregarProducto(producto2Mock.Object);

        Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pizza"));
    }

    [Fact]
    public void eliminarProducto()
    {
        Tienda tienda = new Tienda();

        var producto1Mock = CrearProductoMock("Pan", 1000m);
        var producto2Mock = CrearProductoMock("Carne", 20000m);

        tienda.agregarProducto(producto1Mock.Object);
        tienda.agregarProducto(producto2Mock.Object);


        var eliminado = tienda.eliminarProducto("Pan");

        Assert.True(eliminado);
        Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pan"));
    }

    [Fact]
    public void eliminarProductoInexistente()
    {
        Tienda tienda = new Tienda();

        Assert.Throws<KeyNotFoundException>(() => tienda.eliminarProducto("Pizza"));
    }

    [Fact]
    public void aplicarDescuentoCalculaNuevoPrecioYActualizaProducto()
    {
        Tienda tienda = new Tienda();
        var productoMock = CrearProductoMock("Pan", 1000m);
        decimal precioActualizado = 0m;
        productoMock
            .Setup(producto => producto.actualizarPrecio(It.IsAny<decimal>()))
            .Callback<decimal>(precio => precioActualizado = precio);

        tienda.agregarProducto(productoMock.Object);

        var productoConDescuento = tienda.aplicarDescuento("Pan", 0.20m);

        Assert.Same(productoMock.Object, productoConDescuento);
        Assert.Equal(800m, precioActualizado);
        productoMock.Verify(producto => producto.actualizarPrecio(800m), Times.Once);
    }
}
