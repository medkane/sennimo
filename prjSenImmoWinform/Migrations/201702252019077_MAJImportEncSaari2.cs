namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJImportEncSaari2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportEncaissementSaaris", "APrendre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImportEncaissementSaaris", "APrendre");
        }
    }
}
