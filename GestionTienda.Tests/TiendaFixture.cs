using GestionTienda.Entities;
using Moq;

namespace GestionTienda.Tests;

public class TiendaFixture
{
    public Tienda crearTiendaConProductos()
    {
        var tienda = new Tienda();

        var pan = crearProductoMock("Pan", 1000m);
        var carne = crearProductoMock("Carne", 20000m);

        tienda.agregarProducto(pan.Object);
        tienda.agregarProducto(carne.Object);

        return tienda;
    }

    public Mock<Producto> crearProductoMock(string nombre, decimal precio)
    {
        var productoMock = new Mock<Producto>(nombre, precio, "categoria");
        productoMock
            .SetupGet(producto => producto.Nombre)
            .Returns(nombre);
        productoMock
            .SetupGet(producto => producto.Precio)
            .Returns(precio);

        return productoMock;
    }
}
