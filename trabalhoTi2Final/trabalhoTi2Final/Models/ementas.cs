using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class Ementas
    {
        [Key]
        public int IDEmentas { get; set; }


        public DateTime Dia { get; set; }
        public string Periodo { get; set; }


        public Ementas()
        {
            ListaDePratos= new HashSet<Pratos>();
        }

        public virtual ICollection<Pratos> ListaDePratos { get; set; }

    }
}