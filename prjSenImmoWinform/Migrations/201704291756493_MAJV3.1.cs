namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJV31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ilots", "TypeConstruction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ilots", "TypeConstruction");
        }
    }
}
