using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Patrimonio.Domains
{
    public partial class Comentario
    {
        [Key]
        public int Id { get; set; }
        public int IdEquipamentos { get; set; }
        public int IdPerfils { get; set; }
        public string Comentario1 { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Equipamento IdEquipamentosNavigation { get; set; }
        public virtual Perfil IdPerfilsNavigation { get; set; }
    }
}
