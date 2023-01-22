using System;
using System.Collections.Generic;

namespace thehatfactory.api.Models
{
    public partial class ProductoCompra
    {
        public Guid Id { get; set; }
        public Guid CompraId { get; set; }
        public Guid ProductoId { get; set; }
        public string? ProductTalla { get; set; }
        public decimal Price { get; set; }

        public virtual Compra Compra { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;
    }
}
