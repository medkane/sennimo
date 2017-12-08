namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeEtatAvancementLibelleTechnique : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEtatAvancements", "LibelleTechnique", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeEtatAvancements", "LibelleTechnique");
        }
    }
}
