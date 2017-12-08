namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJIlotCommentaire : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ilots", "Commentaires", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ilots", "Commentaires");
        }
    }
}
