using GestionTienda.Entities;
using Moq;

namespace GestionTienda.Tests;

public class TiendaTests : IClassFixture<TiendaFixture>
{
    private readonly TiendaFixture fixture;

    public TiendaTests(TiendaFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void agregarProducto()
    {
        Tienda tienda = new Tienda();
        var productoMock = this.fixture.crearProductoMock("Pan", 1000m);

        var inventario = tienda.agregarProducto(productoMock.Object);

        Assert.Single(inventario);
        Assert.Same(productoMock.Object, inventario[0]);
    }

    [Fact]
    public void buscarProductoCorrecto()
    {
        Tienda tienda = this.fixture.crearTiendaConProductos();

        var buscar = tienda.buscarProducto("Carne");

        Assert.Equal("Carne", buscar.Nombre);
    }

    [Fact]
    public void buscarProductoInexistente()
    {
        Tienda tienda = this.fixture.crearTiendaConProductos();

        Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pizza"));
    }

    [Fact]
    public void eliminarProducto()
    {
        Tienda tienda = this.fixture.crearTiendaConProductos();

        var eliminado = tienda.eliminarProducto("Pan");

        Assert.True(eliminado);
        Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pan"));
    }

    [Fact]
    public void eliminarProductoInexistente()
    {
        Tienda tienda = this.fixture.crearTiendaConProductos();

        Assert.Throws<KeyNotFoundException>(() => tienda.eliminarProducto("Pizza"));
    }

    [Fact]
    public void aplicarDescuentoCalculaNuevoPrecioYActualizaProducto()
    {
        Tienda tienda = new Tienda();
        var productoMock = this.fixture.crearProductoMock("Pan", 1000m);
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

    [Fact]
    public void calcularTotalCarritoIntegraProductosYDescuentos()
    {
        Tienda tienda = this.fixture.crearTiendaConProductosReales();

        tienda.aplicarDescuento("Pan", 0.20m);
        tienda.aplicarDescuento("Carne", 0.10m);

        var total = tienda.calcular_total_carrito(new List<string> { "Pan", "Carne", "Leche" });

        Assert.Equal(20300m, total);
    }
}
