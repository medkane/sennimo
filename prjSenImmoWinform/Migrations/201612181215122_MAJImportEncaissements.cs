namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJImportEncaissements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportEncaissements", "iDClient", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportEncaissements", "iDClient", c => c.Int(nullable: false));
        }
    }
}
