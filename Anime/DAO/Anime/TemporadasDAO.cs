using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.DAO.Anime
{
    public class TemporadasDAO
    {
        private static BibliotecaContext ctx = SingletonContext.GetInstance();
        public static List<Temporada> ListaTemporadas() =>  ctx.Temporadas.ToList();
        public static Temporada BuscarTempPorId(int? id) => ctx.Temporadas.Find(id);
 


    }
}