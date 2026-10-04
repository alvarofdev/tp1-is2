using GestionTienda.Entities;

namespace GestionTienda.Tests;

public class TiendaTests
{
    [Fact]
    public void agregarProducto()
    {
        Tienda tienda = new Tienda();
        Producto producto = new Producto("Pan", 1000, "panaderia");

        var inventario = tienda.agregarProducto(producto);

        Assert.Single(inventario);
        Assert.Same(producto, inventario[0]);
    }

    [Fact]
    public void buscarProductoCorrecto()
    {
        Tienda tienda = new Tienda();

        Producto producto1 = new Producto("Pan", 1000, "panaderia");
        Producto producto2 = new Producto("Carne", 20000, "carniceria");

        tienda.agregarProducto(producto1);
        tienda.agregarProducto(producto2);

        var buscar = tienda.buscarProducto(producto2.Nombre);

        Assert.Same(producto2, buscar);
    }

    [Fact]
    public void buscarProductoInexistente()
    {
        Tienda tienda = new Tienda();

        Producto producto1 = new Producto("Pan", 1000, "panaderia");
        Producto producto2 = new Producto("Carne", 20000, "carniceria");

        tienda.agregarProducto(producto1);
        tienda.agregarProducto(producto2);

        Assert.Throws<ArgumentNullException>(() => tienda.buscarProducto("Pizza"));
    }

    [Fact]
    public void eliminarProducto()
    {
        Tienda tienda = new Tienda();

        Producto producto1 = new Producto("Pan", 1000, "panaderia");
        Producto producto2 = new Producto("Carne", 20000, "carniceria");

        tienda.agregarProducto(producto1);
        tienda.agregarProducto(producto2);


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
}
