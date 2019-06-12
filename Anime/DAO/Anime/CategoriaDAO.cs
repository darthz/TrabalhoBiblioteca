using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Anime.Models;

namespace Anime.DAO.Anime
{
    public class CategoriaDAO
    {
        private static BibliotecaContext ctx = SingletonContext.GetInstance();

        public static void AddCategoria(Categoria c)
        {
            ctx.Categorias.Add(c);
            ctx.SaveChanges();
        }
        public static List<Categoria> RetornarCategoria() => ctx.Categorias.ToList();
        public static Categoria BuscarCategoriaPorID(int? id) => ctx.Categorias.Find(id);
    }
}