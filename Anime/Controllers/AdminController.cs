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
            return View();
        }
        public ActionResult AdicionarAnime()
        {
            ViewBag.Temporadas = new SelectList(TemporadasDAO.ListaTemporadas(), "IDTemporada", "Estacao");
            return View();
        }
        [HttpPost]
        public ActionResult AdicionarAnime(int? Temp, Animes Anime, HttpPostedFileBase AnimeImagem)
        {
            ViewBag.Temporadas = new SelectList(TemporadasDAO.ListaTemporadas(), "IDTemporada", "Estacao");

           //Adicionar o IF ModelStateIsValid depois que resolver o negócio da categoria e das temporadas

                Anime.Temporadas.Add(TemporadasDAO.BuscarTempPorId(Temp));
                if (AnimeImagem == null)
                {
                    Anime.Imagem = "SemImagem.jpeg";
                }
                else
                {
                    string c = System.IO.Path.Combine(Server.MapPath("~/Imagem/"), AnimeImagem.FileName);
                    AnimeImagem.SaveAs(c);
                    Anime.Imagem = AnimeImagem.FileName;
                }
                if (AnimeDAO.AdicionarAnime(Anime))
                {
                    
                 return RedirectToAction("Index", "Admin");
                }


            
      
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarTemp(Temporada temp)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }
    }
}