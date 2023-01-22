using System;
using System.Collections.Generic;

namespace thehatfactory.api.Models
{
    public partial class Compra
    {
        public Compra()
        {
            ProductoCompras = new HashSet<ProductoCompra>();
        }

        public Guid Id { get; set; }
        public string IdPaypal { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
        public decimal TotalValue { get; set; }
        public Guid IdUsuarioCompra { get; set; }

        public virtual Usuario IdUsuarioCompraNavigation { get; set; } = null!;
        public virtual ICollection<ProductoCompra> ProductoCompras { get; set; }
    }
}
