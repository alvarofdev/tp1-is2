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


        Assert.Same(producto2, tienda.buscarProducto(producto2.obtenerNombre()));
    }

    [Fact]
    public void buscarProductoIncorrecto()
    {
        Tienda tienda = new Tienda();

        Producto producto1 = new Producto("Pan", 1000, "panaderia");
        Producto producto2 = new Producto("Carne", 20000, "carniceria");


        tienda.agregarProducto(producto1);
        tienda.agregarProducto(producto2);


        Assert.Null(tienda.buscarProducto("Pizza"));
    }

}
