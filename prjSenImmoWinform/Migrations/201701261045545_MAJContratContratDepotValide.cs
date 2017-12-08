namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratContratDepotValide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "ContratDepotValide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contrats", "ContratDepotValide");
        }
    }
}
