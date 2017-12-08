namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJImportEncSaari : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportEncaissementSaaris", "Commentaire", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImportEncaissementSaaris", "Commentaire");
        }
    }
}
