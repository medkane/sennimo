namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratValide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "ContratValide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contrats", "ContratValide");
        }
    }
}
