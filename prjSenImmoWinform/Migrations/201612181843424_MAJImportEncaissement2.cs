namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJImportEncaissement2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportEncaissements", "Importe", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportEncaissements", "Importe", c => c.Boolean(nullable: false));
        }
    }
}
