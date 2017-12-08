namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJCooperativeAgent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cooperatives", "AgentID", c => c.Int());
            CreateIndex("dbo.Cooperatives", "AgentID");
            AddForeignKey("dbo.Cooperatives", "AgentID", "dbo.Agents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cooperatives", "AgentID", "dbo.Agents");
            DropIndex("dbo.Cooperatives", new[] { "AgentID" });
            DropColumn("dbo.Cooperatives", "AgentID");
        }
    }
}
