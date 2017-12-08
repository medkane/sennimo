namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDTauxAtteinte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TauxAtteintes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TauxMinimun = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TauxMaximun = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Taux = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TauxAtteintes");
        }
    }
}
