using System;
using System.Collections.Generic;

namespace thehatfactory.api.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
            FavoritosUsuarios = new HashSet<FavoritosUsuario>();
        }

        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPassword { get; set; } = null!;

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<FavoritosUsuario> FavoritosUsuarios { get; set; }
    }
}
