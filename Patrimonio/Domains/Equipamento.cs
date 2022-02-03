using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Patrimonio.Domains
{
    public partial class Equipamento
    {
        [Key]
        public int Id { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public bool? Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomePatrimonio { get; set; }
    }
}
