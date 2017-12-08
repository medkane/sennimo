namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJObjectifsDateLimite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObjectifAnnuels", "DateDebut", c => c.DateTime(nullable: false));
            AddColumn("dbo.ObjectifAnnuels", "DateFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.ObjectifMensuels", "DateDebut", c => c.DateTime(nullable: false));
            AddColumn("dbo.ObjectifMensuels", "DateFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.ObjectifTrimestriels", "DateDebut", c => c.DateTime(nullable: false));
            AddColumn("dbo.ObjectifTrimestriels", "DateFin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ObjectifTrimestriels", "DateFin");
            DropColumn("dbo.ObjectifTrimestriels", "DateDebut");
            DropColumn("dbo.ObjectifMensuels", "DateFin");
            DropColumn("dbo.ObjectifMensuels", "DateDebut");
            DropColumn("dbo.ObjectifAnnuels", "DateFin");
            DropColumn("dbo.ObjectifAnnuels", "DateDebut");
        }
    }
}
