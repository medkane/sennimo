namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDEncaissementResa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportEncaissementSaaris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateEncaissement = c.String(),
                        Libelle = c.String(),
                        CompteTiers = c.String(),
                        MontantDebit = c.String(),
                        MontantCredit = c.String(),
                        Importe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportEncaissementSaaris");
        }
    }
}
