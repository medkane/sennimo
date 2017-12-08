namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDImportEtatAvancement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportEtatAvancements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroLot = c.String(),
                        Avancement = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportEtatAvancements");
        }
    }
}
