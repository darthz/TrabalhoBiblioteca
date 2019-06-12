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
        //Get Adicionar anime
        public ActionResult AdicionarAnime()
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");
            return View();
        }
        //Get Adicionar Temporada
        public ActionResult AdicionarTemp()
        {
            ViewBag.Temporadas = new SelectList(TemporadasDAO.ListaTemporadas(), "IDTemporada", "Estacao");
            ViewBag.Msgs = "Cadastrado com sucesso, adicione as temporadas";
            var a = TempData["AdtempAnime"];
            ViewBag.Anime = TempData["AdtempAnime"];
            return View(a);
        }
        //Get Pesquisar Anime
        public ActionResult PesquisarAnime()
        {
            var a = TempData["PesqAnime"];
            ViewBag.Msg = TempData["Msgs"];
            if (a != null)
            {
                return View(a);
            }
            return View();
        }
        //Post Adicionar Anime
        [HttpPost]
        public ActionResult AdicionarAnime(int? Categorias, Animes a, HttpPostedFileBase AnimeImagem)
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");

            if (AnimeDAO.BuscarPorNome(a) == null)
            {
                a.Categoria = CategoriaDAO.BuscarCategoriaPorID(Categorias);
                if (AnimeImagem == null)
                {
                    a.Imagem = "SemImagem.jpg";
                }
                else
                {
                    string c = System.IO.Path.Combine(Server.MapPath("~/Imagem/"), AnimeImagem.FileName);
                    AnimeImagem.SaveAs(c);
                    a.Imagem = AnimeImagem.FileName;
                }
                if (AnimeDAO.AdicionarAnime(a))
                {

                    TempData["AdtempAnime"] = a;
                    Session["ObjetoFull"] = a;
                    return RedirectToAction("AdicionarTemp", "Admin");
                }

                return View(a);
            }
            ModelState.AddModelError("", "Esse anime já está cadastrado!");
            return View(a);
        }

        //Post Adicionar Temporada
        [HttpPost]
        public ActionResult AdicionarTemp(int? Temporadas, string txtAno, int QtdeEps)
        {
            //Busca as temporadas
            ViewBag.Temporadas = new SelectList(TemporadasDAO.ListaTemporadas(), "IDTemporada", "Estacao");
            //Adiciona o ano ao objeto
            Animes a = new Animes();
            Temporada temp = new Temporada { Ano = txtAno };
            if (Temporadas != null && txtAno != null)
            {
                a = Session["ObjetoFull"] as Animes;
                a = AnimeDAO.BuscarPorNome(a);

                var back = TemporadasDAO.BuscarTempPorId(Temporadas);
                temp.Estacao = back.Estacao;
                Temporada t = TemporadasDAO.AddTemporada(temp, QtdeEps);
                if (a.Temporadas == null)
                {
                    a.Temporadas = new List<Temporada>();
                }

                a.Temporadas.Add(t);
                AnimeDAO.AtualizarAnime(a);
                Session["ObjetoFull"] = null;
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Não deixe valores nulos! ");
            return View(a);
        }

        //GET Remover Anime
        public ActionResult RemoverAnime(int? id)
        {
            Animes a = AnimeDAO.BuscarPorID(id);
            if (a != null)
            {
                AnimeDAO.RemoverAnime(a);
                TempData["Msgs"] = "Anime Deletado com sucesso! ";
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Esse anime não está cadastrado na base!");
            return RedirectToAction("Index", "Admin");

        }

        //Post Pesquisar Anime
        [HttpPost]
        public ActionResult PesquisarAnime(string NomeAnime)
        {
            Animes a = new Animes();
            a.NomeAnime = NomeAnime;
            var b = AnimeDAO.BuscarPorNomeInclude(a);
            if (b != null)
            {
                TempData["PesqAnime"] = b;
                return RedirectToAction("PesquisarAnime", "Admin");
            }

            ModelState.AddModelError("", "Esse anime não está cadastrado!");

            TempData["Msgs"] = "Esse anime não está cadastrado na base!";
            return RedirectToAction("Index", "Admin");
        }
        //Get Alterar Anime
        public ActionResult AlterarAnime(int? id)
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");

            return View(AnimeDAO.BuscarPorID(id));
        }
        //Post Alterar Anime
        [HttpPost]
        public ActionResult AlterarAnime(int? Categorias, Animes anime, HttpPostedFileBase AnimeImagem)
        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "IDCategoria", "DescCategoria");
            Animes a = new Animes();
            a = AnimeDAO.BuscarPorID(anime.IDAnime);
            var temp = AnimeDAO.BuscarPorNome(anime);
            //Coloca um nome para a img primeiro
            if (temp == null || temp.NomeAnime.Equals(anime.NomeAnime))
            {
                if (AnimeImagem != null)
                {
                    if (AnimeImagem.FileName != anime.Imagem)
                    {

                        string c = System.IO.Path.Combine(Server.MapPath("~/Imagem/"), AnimeImagem.FileName);
                        AnimeImagem.SaveAs(c);
                        a.Imagem = AnimeImagem.FileName;
                    }
                }
                else
                {
                    a.Imagem = anime.Imagem;
                }
                //Depois compara
                if (a != anime)
                {

                    a.Categoria = CategoriaDAO.BuscarCategoriaPorID(Categorias);
                    a.NomeAnime = anime.NomeAnime;
                    a.Descricao = anime.Descricao;
                    a.Duracao = anime.Duracao;
                    a.Estudio = anime.Estudio;

                    AnimeDAO.AtualizarAnime(a);
                    TempData["Msgs"] = "Alterado com sucesso";
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Esse nome de anime já está cadastrado!");
                return View(anime);
            }
            ModelState.AddModelError("", "Não há alterações! ");
            return View(anime);

        }


    }
}