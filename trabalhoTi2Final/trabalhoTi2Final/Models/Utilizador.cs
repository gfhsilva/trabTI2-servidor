using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class Utilizador
    {
        [Key]
        public int IDUtilizador { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

        [ForeignKey("Refeicao")]
        public virtual ICollection<Refeicao> Refeicao { get; set; }
    }
}