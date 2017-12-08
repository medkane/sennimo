namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJAgentRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "UserLogin", c => c.String());
            AddColumn("dbo.Agents", "MotDePasse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agents", "MotDePasse");
            DropColumn("dbo.Agents", "UserLogin");
        }
    }
}
