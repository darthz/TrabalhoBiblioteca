namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "Imagem", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Animes", "Imagem");
        }
    }
}
