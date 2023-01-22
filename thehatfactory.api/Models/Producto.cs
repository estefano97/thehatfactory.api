using System;
using System.Collections.Generic;

namespace thehatfactory.api.Models
{
    public partial class Producto
    {
        public Producto()
        {
            FavoritosUsuarios = new HashSet<FavoritosUsuario>();
            ProductoCompras = new HashSet<ProductoCompra>();
        }

        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public string? Liga { get; set; }
        public int? Categoria { get; set; }
        public string? Equipo { get; set; }

        public virtual ICollection<FavoritosUsuario> FavoritosUsuarios { get; set; }
        public virtual ICollection<ProductoCompra> ProductoCompras { get; set; }
    }
}
