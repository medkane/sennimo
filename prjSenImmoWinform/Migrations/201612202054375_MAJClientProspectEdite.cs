namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientProspectEdite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ProspectEdite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "ProspectEdite");
        }
    }
}
