namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratAvenant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "AvecAvenant", c => c.Boolean());
            AddColumn("dbo.Contrats", "ContratPrecedentID", c => c.Int());
            CreateIndex("dbo.Contrats", "ContratPrecedentID");
            AddForeignKey("dbo.Contrats", "ContratPrecedentID", "dbo.Contrats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contrats", "ContratPrecedentID", "dbo.Contrats");
            DropIndex("dbo.Contrats", new[] { "ContratPrecedentID" });
            DropColumn("dbo.Contrats", "ContratPrecedentID");
            DropColumn("dbo.Contrats", "AvecAvenant");
        }
    }
}
