namespace GestionTienda.Entities;

public class Tienda
{
    private List<Producto> Inventario;

    public Tienda()
    {
        this.Inventario = new List<Producto>();
    }

    public List<Producto> agregarProducto(Producto nuevoProducto)
    {
        this.Inventario.Add(nuevoProducto);

        return this.Inventario;
    }

    public Producto buscarProducto(string nombre)
    {
        var result = this.Inventario.FirstOrDefault(producto =>
                producto.Nombre == nombre);

        if (result == null)
        {
            throw new ArgumentNullException($"no se encuentra producto con nombre {nombre}");
        }

        return result;
    }

    public bool eliminarProducto(string nombre)
    {
        Producto? productoAEliminar = this.Inventario.Find(producto =>
                producto.Nombre == nombre);

        if (productoAEliminar == null)
        {
            throw new KeyNotFoundException($"No se encuentra el producto {nombre}");
        }

        this.Inventario.Remove(productoAEliminar);
        return true;
    }

    public Producto aplicarDescuento(string nombre, decimal descuento)
    {
        if (descuento < 0 || descuento > 1)
        {
            throw new ArgumentOutOfRangeException("descuento", $"El descuento no puede ser negativo o mayor a 1");
        }

        var productoConDescuento = this.buscarProducto(nombre);

        decimal nuevoPrecio = productoConDescuento.Precio * (1 - descuento);
        productoConDescuento.actualizarPrecio(nuevoPrecio);

        return productoConDescuento;
    }

    public decimal calcular_total_carrito(List<string> carrito)
    {
        return carrito.Sum(nombre => this.buscarProducto(nombre).Precio);
    }
}
