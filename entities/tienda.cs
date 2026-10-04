namespace GestionTienda.Entities;

class Tienda
{
    private List<Producto> inventario;

    public Tienda(List<Producto> inventario)
    {
        this.inventario = new List<Producto>();
    }

    public List<Producto> agregarProducto(Producto nuevoProducto)
    {
        this.inventario.Add(nuevoProducto);

        return this.inventario;
    }

    public Producto? buscarProducto(string nombre)
    {
        return this.inventario.FirstOrDefault(p => p.obtenerNombre() == nombre);
    }

    public bool eliminarProducto(string nombre)
    {
        foreach (Producto producto in this.inventario)
        {
            if (producto.obtenerNombre() == nombre)
            {
                this.inventario.Remove(producto);
                return true;
            }
        }

        return false;
    }
}
