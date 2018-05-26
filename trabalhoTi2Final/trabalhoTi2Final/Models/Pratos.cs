using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class Pratos
    {
        [Key]
        public int IDPratos { get; set; }
        public string Image { get; set; }
        public string Descricao { get; set; }
     
  

        [ForeignKey("TipoPrato")]
        public int TipoPratoFK { get; set; }
        public virtual TipoPrato TipoPrato { get; set; }

        public Pratos()
        {
            ListaDeRefeicoes = new HashSet<Refeicao>();
            ListaDeEmentas = new HashSet<Ementas>();
        }

        public virtual ICollection<Refeicao> ListaDeRefeicoes { get; set; }
        public virtual ICollection<Ementas> ListaDeEmentas { get; set; }



    }
}       