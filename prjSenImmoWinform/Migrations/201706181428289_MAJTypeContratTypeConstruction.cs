namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeContratTypeConstruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeContrats", "TypeConstruction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeContrats", "TypeConstruction");
        }
    }
}
