using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.DAO.Anime
{
    public class BibliotecaDAO
    {

        private static BibliotecaContext ctx = SingletonContext.GetInstance();

        public static List<Animes> ListaAnimes()
        {

            return ctx.Animes.ToList();

        }

        public static List<Categoria> RetornarCategoria()
        {
            return ctx.Categorias.ToList();
        }
    }

}