using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anime.DAO
{
    public class Dados
    {
        public static void Inserir()
        {
            Usuario usuario = new Usuario { 
            UsuarioId = 10,
            Nome = "Adm",
            Sobrenome = "Adm",
            Email = "biel_s.carvalho2@hotmail.com",
            Nickname = "Administrador",
            Login = "Admin",
            Senha = "Admin",
            ConfirmarSenha = "Admin",
            Nivel = 1,
            Role = "Administrador"
        };

        UsuarioDAO.CadastrarAdm(usuario);

        }
    }
}