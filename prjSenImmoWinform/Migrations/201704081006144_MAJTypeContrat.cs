namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJTypeContrat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeContrats", "DateCreation", c => c.DateTime());
            AddColumn("dbo.TypeContrats", "Actif", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeContrats", "DateDesactivation", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeContrats", "DateDesactivation");
            DropColumn("dbo.TypeContrats", "Actif");
            DropColumn("dbo.TypeContrats", "DateCreation");
        }
    }
}
