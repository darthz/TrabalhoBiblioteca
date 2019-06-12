using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.DAO
{
    public class UsuarioDAO
    {

        // Criei o Singleton e to pegando a instância dele. 
        // Tirei todos os "using" e troquei pelo já instânciado Context
        // Ah, os outros métodos estavam como "contexto" eu só deixei como context

        private static BibliotecaContext ctx = SingletonContext.GetInstance();
        public static bool Adiciona(Usuario usuario)
        {
            
                if (BuscaPorNickname(usuario) == null)
                {
                    ctx.Usuarios.Add(usuario);
                    ctx.SaveChanges();
                    return true;
                }
            return false;

        }

        public IList<Usuario> Lista()
        {

            return ctx.Usuarios.ToList();

        }

        public static Usuario BuscarPorId(int? id)
        {

            return ctx.Usuarios.Find(id);

        }

        public static void Atualiza(Usuario usuario)
        {

            ctx.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();

        }

        public Usuario Busca(string login, string senha)
        {

            return ctx.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);

        }
        public static Usuario BuscaPorNickname(Usuario usuario)
        {


            return ctx.Usuarios.FirstOrDefault(u => u.Nickname == usuario.Nickname);

        }
        public static bool CadastrarAdm(Usuario usuario)
        {
            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
            return true;

        }
        public static bool VerificaAdm(Usuario usuario)
        {

            if (BuscaPorNickname(usuario) == null)
            {
                return true;
            }

            return false;
        }
        public Usuario BuscaNivel(Usuario usuario)
        {

            return ctx.Usuarios.Find(usuario.Nivel);

        }
    }
}