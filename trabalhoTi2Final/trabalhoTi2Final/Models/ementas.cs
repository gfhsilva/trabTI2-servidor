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
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // impede que uma nova ementa tenha um ID automático
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