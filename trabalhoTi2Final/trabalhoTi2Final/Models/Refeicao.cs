using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class Refeicao
    {
        [Key]
        public int IDRefeicao { get; set; }
        public String Periodo { get; set; }
        public DateTime Dia { get; set; }
        public DateTime DataReserva { get; set; }
        public Boolean Fornecido { get; set; }


        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public Refeicao()
        {
            ListaDePratos = new HashSet<Pratos>();
        }

        public virtual ICollection<Pratos> ListaDePratos { get; set; }

    }
}