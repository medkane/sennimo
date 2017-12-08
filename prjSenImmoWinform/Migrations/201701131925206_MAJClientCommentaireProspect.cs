namespace prjSenImmoWinform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAJClientCommentaireProspect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "CommentaireProspect", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "CommentaireProspect");
        }
    }
}
