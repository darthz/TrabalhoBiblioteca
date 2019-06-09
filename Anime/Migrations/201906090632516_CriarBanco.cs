namespace Anime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animes",
                c => new
                    {
                        IDAnime = c.Int(nullable: false, identity: true),
                        NomeAnime = c.String(),
                        Estudio = c.String(),
                        Duracao = c.String(),
                        Usuario_UsuarioId = c.Int(),
                        Usuario_UsuarioId1 = c.Int(),
                        Usuario_UsuarioId2 = c.Int(),
                    })
                .PrimaryKey(t => t.IDAnime)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId1)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId2)
                .Index(t => t.Usuario_UsuarioId)
                .Index(t => t.Usuario_UsuarioId1)
                .Index(t => t.Usuario_UsuarioId2);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        IDCategoria = c.Int(nullable: false, identity: true),
                        DescCategoria = c.String(),
                        Animes_IDAnime = c.Int(),
                    })
                .PrimaryKey(t => t.IDCategoria)
                .ForeignKey("dbo.Animes", t => t.Animes_IDAnime)
                .Index(t => t.Animes_IDAnime);
            
            CreateTable(
                "dbo.Temporadas",
                c => new
                    {
                        IDTemporada = c.Int(nullable: false, identity: true),
                        Estacao = c.String(),
                        Animes_IDAnime = c.Int(),
                    })
                .PrimaryKey(t => t.IDTemporada)
                .ForeignKey("dbo.Animes", t => t.Animes_IDAnime)
                .Index(t => t.Animes_IDAnime);
            
            CreateTable(
                "dbo.Episodios",
                c => new
                    {
                        IDEpisodio = c.Int(nullable: false, identity: true),
                        NumEpisodio = c.Int(nullable: false),
                        NomeEpisodio = c.String(),
                        Temporada_IDTemporada = c.Int(),
                    })
                .PrimaryKey(t => t.IDEpisodio)
                .ForeignKey("dbo.Temporadas", t => t.Temporada_IDTemporada)
                .Index(t => t.Temporada_IDTemporada);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Sobrenome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        ConfirmarSenha = c.String(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animes", "Usuario_UsuarioId2", "dbo.Usuarios");
            DropForeignKey("dbo.Animes", "Usuario_UsuarioId1", "dbo.Usuarios");
            DropForeignKey("dbo.Animes", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Temporadas", "Animes_IDAnime", "dbo.Animes");
            DropForeignKey("dbo.Episodios", "Temporada_IDTemporada", "dbo.Temporadas");
            DropForeignKey("dbo.Categoria", "Animes_IDAnime", "dbo.Animes");
            DropIndex("dbo.Episodios", new[] { "Temporada_IDTemporada" });
            DropIndex("dbo.Temporadas", new[] { "Animes_IDAnime" });
            DropIndex("dbo.Categoria", new[] { "Animes_IDAnime" });
            DropIndex("dbo.Animes", new[] { "Usuario_UsuarioId2" });
            DropIndex("dbo.Animes", new[] { "Usuario_UsuarioId1" });
            DropIndex("dbo.Animes", new[] { "Usuario_UsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Episodios");
            DropTable("dbo.Temporadas");
            DropTable("dbo.Categoria");
            DropTable("dbo.Animes");
        }
    }
}
