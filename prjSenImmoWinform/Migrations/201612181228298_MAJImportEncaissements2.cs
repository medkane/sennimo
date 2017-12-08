namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJImportEncaissements2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImportEncaissements", "DateEncaissement", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ImportEncaissements", "Montant", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImportEncaissements", "Montant", c => c.String());
            AlterColumn("dbo.ImportEncaissements", "DateEncaissement", c => c.String());
        }
    }
}
