namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentIsChefEquipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "IsChefEquipe", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agents", "IsChefEquipe");
        }
    }
}
