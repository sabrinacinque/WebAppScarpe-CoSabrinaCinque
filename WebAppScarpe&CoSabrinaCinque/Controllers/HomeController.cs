using Microsoft.AspNetCore.Mvc;
using WebAppScarpe_CoSabrinaCinque.Entities;
using WebAppScarpe_CoSabrinaCinque.Models;
using WebAppScarpe_CoSabrinaCinque.Services;
using System.IO;

namespace WebAppScarpe_CoSabrinaCinque.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IWebHostEnvironment _env;

        public HomeController(IArticleService articleService, IWebHostEnvironment env)
        {
            _articleService = articleService;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Articles()
        {
            var articles = _articleService.GetAll();
            return View(articles);
        }

        public IActionResult Create()
        {
            return View(new ArticleInputModel());
        }

        [HttpPost]
        public IActionResult Create(ArticleInputModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description
                };

                // upload delle immagini (come fatto vedere in classe) 
                string uploads = Path.Combine(_env.WebRootPath, "images");

                if (model.Cover != null)
                {
                    string coverPath = Path.Combine(uploads, $"{article.Id}_cover.jpg");
                    using (var fileStream = new FileStream(coverPath, FileMode.Create))
                    {
                        model.Cover.CopyTo(fileStream);
                    }
                    article.CoverImagePath = $"/images/{article.Id}_cover.jpg";
                }
                if (model.AdditionalImage1 != null)
                {
                    string additionalImagePath1 = Path.Combine(uploads, $"{article.Id}_additional1.jpg");
                    using (var fileStream = new FileStream(additionalImagePath1, FileMode.Create))
                    {
                        model.AdditionalImage1.CopyTo(fileStream);
                    }
                    article.AdditionalImagePath1 = $"/images/{article.Id}_additional1.jpg";
                }
                if (model.AdditionalImage2 != null)
                {
                    string additionalImagePath2 = Path.Combine(uploads, $"{article.Id}_additional2.jpg");
                    using (var fileStream = new FileStream(additionalImagePath2, FileMode.Create))
                    {
                        model.AdditionalImage2.CopyTo(fileStream);
                    }
                    article.AdditionalImagePath2 = $"/images/{article.Id}_additional2.jpg";
                }

                _articleService.Create(article);
                return RedirectToAction("Articles");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var article = _articleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var article = _articleService.GetById(id);
            if (article == null)
            {
                return NotFound();
            }

            _articleService.Delete(id);

            // dobbiamo eliminare anche i file associati,quando pensiamo di cancellare un articolo
            string uploads = Path.Combine(_env.WebRootPath, "images");
            string coverPath = Path.Combine(uploads, $"{article.Id}_cover.jpg");
            string additionalImagePath1 = Path.Combine(uploads, $"{article.Id}_additional1.jpg");
            string additionalImagePath2 = Path.Combine(uploads, $"{article.Id}_additional2.jpg");

            if (System.IO.File.Exists(coverPath))
            {
                System.IO.File.Delete(coverPath);
            }
            if (System.IO.File.Exists(additionalImagePath1))
            {
                System.IO.File.Delete(additionalImagePath1);
            }
            if (System.IO.File.Exists(additionalImagePath2))
            {
                System.IO.File.Delete(additionalImagePath2);
            }

            return RedirectToAction("Articles");
        }
    }
}

