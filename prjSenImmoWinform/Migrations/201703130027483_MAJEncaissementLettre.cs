namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJEncaissementLettre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EncaissementGlobals", "EncaissementLettre", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EncaissementGlobals", "EncaissementLettre");
        }
    }
}
