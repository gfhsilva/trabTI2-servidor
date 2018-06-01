using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class TipoPrato
    {
        public TipoPrato()
        {
            Pratos = new HashSet<Pratos>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // impede que um novo tipoPrato tenha um ID automático
   
        public int IDTipoPrato { get; set; }

        public string Designacao { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Pratos> Pratos { get; set; }
    }
}