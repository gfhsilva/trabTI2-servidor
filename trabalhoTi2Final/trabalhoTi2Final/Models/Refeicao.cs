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
        //    [DatabaseGenerated(DatabaseGeneratedOption.None)] // impede que um novo refeição tenha um ID automático
        public int IDRefeicao { get; set; }
        public String Periodo { get; set; }
        public DateTime Dia { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Fornecido { get; set; }


        [ForeignKey("Utilizador")]
        public int utilizadorFK { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        [ForeignKey("Prato")]
        public int PratosFk { get; set; }
        public virtual Pratos Prato { get; set; }
     

    }
}