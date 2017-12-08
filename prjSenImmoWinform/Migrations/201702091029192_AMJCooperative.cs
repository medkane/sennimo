namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AMJCooperative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cooperatives", "Denomination", c => c.String());
            AddColumn("dbo.Cooperatives", "DateSouscription", c => c.DateTime());
            AddColumn("dbo.Cooperatives", "Adresse", c => c.String());
            AddColumn("dbo.Cooperatives", "Email", c => c.String());
            AddColumn("dbo.Cooperatives", "Mobile", c => c.String());
            AddColumn("dbo.Cooperatives", "Fixe", c => c.String());
            AlterColumn("dbo.Cooperatives", "TauxRemise", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Cooperatives", "NomCooperative");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cooperatives", "NomCooperative", c => c.String());
            AlterColumn("dbo.Cooperatives", "TauxRemise", c => c.Int(nullable: false));
            DropColumn("dbo.Cooperatives", "Fixe");
            DropColumn("dbo.Cooperatives", "Mobile");
            DropColumn("dbo.Cooperatives", "Email");
            DropColumn("dbo.Cooperatives", "Adresse");
            DropColumn("dbo.Cooperatives", "DateSouscription");
            DropColumn("dbo.Cooperatives", "Denomination");
        }
    }
}
