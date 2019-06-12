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
        public static Animes BuscarPorID(int? id) => ctx.Animes.Find(id);
        public IList<Animes> ListaAnimes() => ctx.Animes.ToList();
        public static void AtualizarAnime(Animes Animes)
        {
            ctx.Entry(Animes).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }
        public static void RemoverAnime(Animes anime)
        {
            ctx.Animes.Remove(anime);
            ctx.SaveChanges();

        }

        public static Animes BuscarAnimesPorId(int? id)
        {
            return ctx.Animes.Find(id);
        }


        public static Animes BuscarPorNomeInclude(Animes a) => ctx.Animes.
            Include("Temporadas").Include("Categoria")
            .SingleOrDefault(x => x.NomeAnime.Equals(a.NomeAnime));
    }
}
