namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDClientsResa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientResas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        Prenom = c.String(),
                        Nom = c.String(),
                        CompteTiers = c.String(),
                        Type = c.String(),
                        Ilot = c.String(),
                        Lot = c.String(),
                        Surface = c.String(),
                        PrixDeVente = c.String(),
                        Importe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClientResas");
        }
    }
}
