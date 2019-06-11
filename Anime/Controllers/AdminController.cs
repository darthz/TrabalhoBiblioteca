using Anime.Models;
using Anime.DAO;
using Anime.DAO.Anime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anime.Controllers
{
    public class AdminController : Controller
    {
       
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Msg = TempData["AdtempAnime"];
            return View();
        }
        public ActionResult AdicionarAnime()
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");
            return View();
        }
        //Falta fazer
        public ActionResult AdicionarTemp()
        {
            ViewBag.Msgs = TempData["AdtempAnime"];
            var a = TempData["AdtempAnime"];
            return View(a);
        }
        [HttpPost]
        public ActionResult AdicionarAnime(int? Categorias, Animes a, HttpPostedFileBase AnimeImagem)
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");

            if (AnimeDAO.BuscarPorNome(a) == null)
            {
                a.Categoria = CategoriaDAO.BuscarCategoriaPorID(Categorias);
                if (AnimeImagem == null)
                {
                    a.Imagem = "SemImagem.jpeg";
                }
                else
                {
                    string c = System.IO.Path.Combine(Server.MapPath("~/Imagem/"), AnimeImagem.FileName);
                    AnimeImagem.SaveAs(c);
                    a.Imagem = AnimeImagem.FileName;
                }
                if (AnimeDAO.AdicionarAnime(a))
                {
                    TempData["AdtempAnime"] = "Cadastrado com sucesso";
                    return RedirectToAction("Index", "Admin");
                }
              
                return View(a);
            }
            ModelState.AddModelError("", "Esse anime já está cadastrado!");
            return View();
        }

        //falta fazer
        [HttpPost]
        public ActionResult AdicionarTemp(Animes a)
        {
            AnimeDAO.AlterarAnime(a);
            return RedirectToAction("Index", "Admin");
        }


    }
}