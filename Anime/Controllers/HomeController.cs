using Anime.DAO;
using Anime.Filtro;
using Anime.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Anime.Controllers
{
    public class HomeController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();
        public static int ok = 0;

        public ActionResult Index()
        {
            if (ok == 0)
            {
                Dados.Inserir();
                ok = 1;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                if (UsuarioDAO.Adiciona(usuario))
                {

                    return RedirectToAction("Perfil", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nickname já existe!");
                    return View("Index");
                }
            }
            else
            {

                return View("Index");
            }

        }

        [AutorizacaoFilter]
        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult Biblioteca()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autentica(String login, String senha)
        {
            UsuarioDAO dao = new UsuarioDAO();
            Usuario usuario = dao.Busca(login, senha);

            if (usuario != null)
            {
                //if (User.IsInRole("Administrador"))
                //{
                if (usuario.Nivel.Equals(1))
                {
                    TempData["Nickname"] = usuario.Nickname;
                    Session["usuarioLogado"] = usuario;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    //}
                    //else
                    //{
                    TempData["Nickname"] = usuario.Nickname;
                    Session["usuarioLogado"] = usuario;
                    return RedirectToAction("Perfil", "Home");
                }
                //}
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Nick()
        {
            Usuario usuario = new Usuario();
            ViewBag.Nickname = usuario.Nickname;

            return View(UsuarioDAO.BuscaPorNickname(usuario));
        }
        public bool IsInRole(string role)
        {

            return true;
        }
        public ActionResult PerfilAdm()
        {
            return View();
        }
        public ActionResult ConfigSenha()
        {
            return View();
        }
        public ActionResult ConfigEmail()
        {
            return View();
        }
        public ActionResult ConfigNickname()
        {
            return View();
        }
        public ActionResult ConfigImagem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(Usuario usuario)
        {
            Usuario u = UsuarioDAO.BuscarPorId(usuario.UsuarioId);
            u.Senha = usuario.Senha;
            u.ConfirmarSenha = usuario.ConfirmarSenha;
            UsuarioDAO.Atualiza(u);

            if (usuario.Nivel.Equals(1))
            {
                return RedirectToAction("PerfilAdm", "Home");
            }
            else
            {
                return RedirectToAction("Perfil", "Home");
            }
        }
        [HttpPost]
        public ActionResult AlterarEmail(Usuario usuario)
        {
            Usuario u = UsuarioDAO.BuscarPorId(usuario.UsuarioId);
            u.Email = usuario.Email;
            UsuarioDAO.Atualiza(u);

            if (usuario.Nivel.Equals(1))
            {
                return RedirectToAction("PerfilAdm", "Home");
            }
            else
            {
                return RedirectToAction("Perfil", "Home");
            }
        }
        [HttpPost]
        public ActionResult AlterarImagem(Usuario usuario)
        {
            Usuario u = UsuarioDAO.BuscarPorId(usuario.UsuarioId);
            UsuarioDAO.Atualiza(u);

            if (usuario.Nivel.Equals(1))
            {
                return RedirectToAction("PerfilAdm", "Home");
            }
            else
            {
                return RedirectToAction("Perfil", "Home");
            }
        }
        [HttpPost]
        public ActionResult AlterarNickname(Usuario usuario)
        {
            Usuario u = UsuarioDAO.BuscarPorId(usuario.UsuarioId);

            u.Nickname = usuario.Nickname;
            UsuarioDAO.Atualiza(u);

            if (usuario.Nivel.Equals(1))
            {
                return RedirectToAction("PerfilAdm", "Home");
            }
            else
            {
                return RedirectToAction("Perfil", "Home");
            }
        }
    }
}