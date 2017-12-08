namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJContratAjoutDureeDepot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrats", "DureeDepot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contrats", "DureeDepot");
        }
    }
}
