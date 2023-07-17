using System;
using System.Collections.Generic;

namespace ApiTextilCorp.Data.Models;

public partial class Producto
{
    public int ProductosId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int Precio { get; set; }

    public int Cantidad { get; set; }

    public bool Stock { get; set; }

    public string? Imagen { get; set; }

    public int MarcasId { get; set; }

    public int CategoriasId { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual Categoria Categorias { get; set; } = null!;

    public virtual Marca Marcas { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
