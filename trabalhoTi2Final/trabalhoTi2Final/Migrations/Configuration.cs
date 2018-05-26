namespace trabalhoTi2Final.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using trabalhoTi2Final.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var tipoPratos = new List<TipoPrato>{
    new TipoPrato {IDTipoPrato=1, Designacao="Carne", Image="carne.jpg"  },
    new TipoPrato {IDTipoPrato=2, Designacao="Peixe", Image="peixe.jpg"  },
    new TipoPrato {IDTipoPrato=3, Designacao="Vegetariano", Image="peixe.jpg"  }

};
            tipoPratos.ForEach(tt => context.TipoPratos.AddOrUpdate(t => t.Designacao, tt));
            context.SaveChanges();


            var utilizador = new List<Utilizador>{
    new Utilizador{IDUtilizador=1,Nome="Guilherme Silva",Tipo="Aluno"},
    new Utilizador{IDUtilizador=2,Nome="Pedro Silva",Tipo="Aluno"},
    new Utilizador{IDUtilizador=3,Nome="João Silva",Tipo="Professor"},
    new Utilizador{IDUtilizador=4,Nome="Ana Costa",Tipo="Aluno"},
    new Utilizador{IDUtilizador=5,Nome="Sofia Sousa",Tipo="Funcionário"},
    new Utilizador{IDUtilizador=6,Nome="Pedro Costa",Tipo="Aluno"}
};
            utilizador.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.Nome, uu));
            context.SaveChanges();


            var ementas = new List<Ementas>{
    new Ementas{IDEmentas=1,Dia=new DateTime(2018,02,13),Periodo="Almoço"},
    new Ementas{IDEmentas=2,Dia=new DateTime(2018,02,13),Periodo="Almoço"},
    new Ementas{IDEmentas=3,Dia=new DateTime(2018,02,3),Periodo="Jantar"},
    new Ementas{IDEmentas=4,Dia=new DateTime(2018,02,13),Periodo="Almoço"},


};
            ementas.ForEach(ee => context.Ementas.AddOrUpdate(e => e.Dia, ee));
            context.SaveChanges();


            var pratos = new List<Pratos> {
    new Pratos {IDPratos=1, Descricao="Esparguete à bolonhesa (carne e soja) com salada de alface",Image="carne.jpg",TipoPratoFK=1},
    new Pratos {IDPratos=2, Descricao="Pescada frita c/ arroz de tomate e salada de alface e cenoura", Image="peixe.jpg" ,TipoPratoFK=2},
    new Pratos {IDPratos=3, Descricao="Jardineira de peru ", Image="carne.jpg",  TipoPratoFK=1},
    new Pratos {IDPratos=4, Descricao="Douradinhos no forno com arroz de cenoura e salada mista", Image="peixe.jpg",TipoPratoFK=2},
    new Pratos {IDPratos=5, Descricao="Ranchinho (frango e porco)", Image="carne.jpg",  TipoPratoFK=2},
    new Pratos {IDPratos=6, Descricao="Almôndegas de aves c/ arroz e salada mista", Image="carne.jpg",TipoPratoFK=1    }

};
            pratos.ForEach(pp => context.Pratos.AddOrUpdate(p => p.Descricao, pp));
            context.SaveChanges();





            var refeicao = new List<Refeicao>{
    new Refeicao{IDRefeicao=1, Periodo="Almoço",Dia=new DateTime(2018,02,13),DataReserva=new DateTime(2018,02,13),Fornecido=true,  UtilizadorFK=1},
    new Refeicao{IDRefeicao=2, Periodo="Almoço",Dia=new DateTime(2018,02,13),DataReserva=new DateTime(2018,02,13),Fornecido=false,  UtilizadorFK=1},
    new Refeicao{IDRefeicao=3, Periodo="Jantar",Dia=new DateTime(2018,02,20),DataReserva=new DateTime(2018,02,13),Fornecido=false,  UtilizadorFK=1},
    new Refeicao{IDRefeicao=4, Periodo="Jantar",Dia=new DateTime(2018,02,13),DataReserva=new DateTime(2018,02,13),Fornecido=true,  UtilizadorFK=4},
    new Refeicao{IDRefeicao=5, Periodo="Almoço",Dia=new DateTime(2018,02,22),DataReserva=new DateTime(2018,02,21),Fornecido=false,  UtilizadorFK=3},
    new Refeicao{IDRefeicao=6, Periodo="Almoço",Dia=new DateTime(2018,02,23),DataReserva=new DateTime(2018,02,22),Fornecido=true,  UtilizadorFK=2},
    new Refeicao{IDRefeicao=7, Periodo="Jantar",Dia=new DateTime(2018,02,24),DataReserva=new DateTime(2018,02,23),Fornecido=false,  UtilizadorFK=1},
    new Refeicao{IDRefeicao=8, Periodo="Jantar",Dia=new DateTime(2018,02,25),DataReserva=new DateTime(2018,02,24),Fornecido=true,  UtilizadorFK=6}


};
            refeicao.ForEach(rr => context.Refeicoes.AddOrUpdate(r => r.Periodo, rr));
            context.SaveChanges();










        }
    }
}
