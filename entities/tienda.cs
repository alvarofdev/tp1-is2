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

    public Producto? buscarProducto(string nombre)
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
}
