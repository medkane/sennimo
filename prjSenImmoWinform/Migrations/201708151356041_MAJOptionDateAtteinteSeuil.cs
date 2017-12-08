namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJOptionDateAtteinteSeuil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "DateAtteinteSeuil", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Options", "DateAtteinteSeuil");
        }
    }
}
