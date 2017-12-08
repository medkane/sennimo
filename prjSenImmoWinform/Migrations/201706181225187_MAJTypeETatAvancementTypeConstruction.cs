namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeETatAvancementTypeConstruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "TypeConstruction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeEtatAvancements", "TypeConstruction");
        }
    }
}
