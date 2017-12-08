namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentRecouvrementResaDepot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "RecouvrementResa", c => c.Boolean(nullable: false));
            AddColumn("dbo.Agents", "RecouvrementDepot", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agents", "RecouvrementDepot");
            DropColumn("dbo.Agents", "RecouvrementResa");
        }
    }
}
