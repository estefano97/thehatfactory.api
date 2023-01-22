using System;
using System.Collections.Generic;

namespace thehatfactory.api.Models
{
    public partial class FavoritosUsuario
    {
        public Guid Id { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdUsuario { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
