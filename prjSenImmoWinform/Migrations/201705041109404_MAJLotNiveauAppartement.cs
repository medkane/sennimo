namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJLotNiveauAppartement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "NiveauAppartement", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "NiveauAppartement");
        }
    }
}
