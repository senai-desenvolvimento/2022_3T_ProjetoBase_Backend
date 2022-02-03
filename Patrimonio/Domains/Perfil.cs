using System;
using System.Collections.Generic;

#nullable disable

namespace Patrimonio.Domains
{
    public partial class Perfil
    {
        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Perfil1 { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
