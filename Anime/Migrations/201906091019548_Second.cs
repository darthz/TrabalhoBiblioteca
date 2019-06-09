namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Temporadas", "Ano", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Temporadas", "Ano");
        }
    }
}
