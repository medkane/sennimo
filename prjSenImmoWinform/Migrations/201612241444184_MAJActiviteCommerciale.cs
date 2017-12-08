namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJActiviteCommerciale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiviteCommerciales", "Priorite", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiviteCommerciales", "Priorite");
        }
    }
}
