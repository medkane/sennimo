namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeVillaTypeConstruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeVillas", "TypeConstruction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeVillas", "TypeConstruction");
        }
    }
}
