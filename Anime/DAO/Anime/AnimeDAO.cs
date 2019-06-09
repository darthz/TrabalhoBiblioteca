using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.DAO
{
    public class AnimeDAO
    {
        private static BibliotecaContext ctx = SingletonContext.GetInstance();
        public static bool AdicionarAnime(Animes Animes)
        {
            if (BuscarPorNome(Animes) == null)
            {
                ctx.Animes.Add(Animes);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        public static Animes BuscarPorNome(Animes a) => ctx.Animes.SingleOrDefault(x => x.NomeAnime.Equals(a.NomeAnime));
        public IList<Animes> ListaAnimes()
        {

            return ctx.Animes.ToList();

        }
        public static void AtualizarAnime(Animes Animes)
        {

            ctx.Entry(Animes).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();

        }

       
    }
}
