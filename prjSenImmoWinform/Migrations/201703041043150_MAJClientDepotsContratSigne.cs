namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientDepotsContratSigne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientDepots", "ContratResaSigne", c => c.String());
            DropColumn("dbo.ClientDepots", "ContratSigne");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientDepots", "ContratSigne", c => c.String());
            DropColumn("dbo.ClientDepots", "ContratResaSigne");
        }
    }
}
