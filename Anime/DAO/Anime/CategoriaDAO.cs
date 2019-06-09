using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anime.Models;

namespace Anime.DAO
{
    public class CategoriaDAO
    {
        private static BibliotecaContext ctx = SingletonContext.GetInstance();
        public static List<Categoria> ListaCategorias()
        {

            return ctx.Categorias.ToList();

        }
    }
}
