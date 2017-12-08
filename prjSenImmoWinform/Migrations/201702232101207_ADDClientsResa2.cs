namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDClientsResa2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientResas", "MatriculeCommercial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientResas", "MatriculeCommercial");
        }
    }
}
