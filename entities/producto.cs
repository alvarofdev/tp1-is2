namespace GestionTienda.Entities;

class Producto
{
    private string nombre;
    private float precio;
    private string categoria;

    public Producto(string nombre, float precio, string categoria)
    {
        this.nombre = nombre;
        this.precio = precio;
        this.categoria = categoria;
    }

    public string obtenerNombre()
    {
        return this.nombre;
    }
}
