namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDImportCompteTiers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportCompteTiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Compte = c.String(),
                        InituleComptetiers = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportCompteTiers");
        }
    }
}
