using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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


        [ForeignKey("Prato")]
        public int pratosFk { get; set; }
        public virtual Pratos Prato { get; set; }

        public virtual TipoPrato TipoPrato { get; set; }

    }
}