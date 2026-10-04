namespace GestionTienda.Entities;

public class Producto
{
    public virtual string Nombre { get; private set; }
    public virtual decimal Precio { get; private set; }
    public string Categoria { get; private set; }

    public Producto(string nombre, decimal precio, string categoria)
    {
        this.Nombre = nombre;
        this.Precio = this.validarPrecio(precio);
        this.Categoria = categoria;
    }

    public virtual void actualizarPrecio(decimal precio)
    {
        this.Precio = this.validarPrecio(precio);
    }

    private decimal validarPrecio(decimal precio)
    {
        if (precio < 0)
        {
            throw new ArgumentOutOfRangeException(
                    nameof(precio),
                    precio,
                    "El precio no puede ser un valor negativo"
            );
        }

        return precio;
    }
}
