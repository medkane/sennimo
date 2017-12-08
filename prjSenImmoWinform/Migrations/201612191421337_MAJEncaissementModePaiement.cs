namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJEncaissementModePaiement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EncaissementGlobals", "ModePaiement", c => c.Int());
            AlterColumn("dbo.Encaissements", "ModePaiement", c => c.Int());
            AlterColumn("dbo.EncaissementProspects", "ModePaiement", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EncaissementProspects", "ModePaiement", c => c.Int(nullable: false));
            AlterColumn("dbo.Encaissements", "ModePaiement", c => c.Int(nullable: false));
            AlterColumn("dbo.EncaissementGlobals", "ModePaiement", c => c.Int(nullable: false));
        }
    }
}
