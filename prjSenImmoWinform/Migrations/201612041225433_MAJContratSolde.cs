namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratSolde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "ContratSolde", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contrats", "ContratSolde");
        }
    }
}
