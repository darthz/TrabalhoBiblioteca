using Anime.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Anime.DAO
{
    public class BibliotecaContext : DbContext
    {
        //Criei a context de anime, episodio, temporada, categoria. Assim, vai ser criado uma tabela para todos. 
        public BibliotecaContext() : base("BancoBiblioteca") { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Animes> Animes { get; set; }
    }
}