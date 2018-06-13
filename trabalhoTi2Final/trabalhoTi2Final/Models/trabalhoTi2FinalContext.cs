using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace trabalhoTi2Final.Models
{
    public class trabalhoTi2FinalContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public trabalhoTi2FinalContext() : base("name=trabalhoTi2FinalContext")
        {
        }

        public System.Data.Entity.DbSet<trabalhoTi2Final.Models.Ementas> Ementas { get; set; }

        public System.Data.Entity.DbSet<trabalhoTi2Final.Models.Refeicao> Refeicaos { get; set; }

        public System.Data.Entity.DbSet<trabalhoTi2Final.Models.Pratos> Pratos { get; set; }

        public System.Data.Entity.DbSet<trabalhoTi2Final.Models.Utilizador> Utilizadors { get; set; }
    }
}
