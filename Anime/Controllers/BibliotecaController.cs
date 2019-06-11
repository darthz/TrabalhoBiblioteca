using Anime.DAO;
using Anime.DAO.Anime;
using Anime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anime.Controllers
{
    public class BibliotecaController : Controller
    {
        // GET: Biblioteca
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BibliotecaAnimes()
        {
            ViewBag.Categorias = BibliotecaDAO.RetornarCategoria();

            return View(BibliotecaDAO.ListaAnimes());
        }

        public ActionResult Detalhes(int? id)
        {            
            return View(AnimeDAO.BuscarAnimesPorId(id));
        }
    }
}