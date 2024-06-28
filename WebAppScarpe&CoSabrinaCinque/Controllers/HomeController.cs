using Microsoft.AspNetCore.Mvc;
using WebAppScarpe_CoSabrinaCinque.Entities;
using WebAppScarpe_CoSabrinaCinque.Models;
using WebAppScarpe_CoSabrinaCinque.Services;


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

                _articleService.Create(article); 

                // upload delle immagini come abbiamo fatto in classe 
                string uploads = Path.Combine(_env.WebRootPath, "images");

                if (model.Cover != null && model.Cover.Length > 0)
                {
                    string coverPath = Path.Combine(uploads, $"{article.Id}_cover.jpg");
                    using (var fileStream = new FileStream(coverPath, FileMode.Create))
                    {
                        model.Cover.CopyTo(fileStream);
                    }
                    article.CoverImagePath = $"/images/{article.Id}_cover.jpg";
                }
                if (model.AdditionalImage1 != null && model.AdditionalImage1.Length > 0)
                {
                    string additionalImagePath1 = Path.Combine(uploads, $"{article.Id}_additional1.jpg");
                    using (var fileStream = new FileStream(additionalImagePath1, FileMode.Create))
                    {
                        model.AdditionalImage1.CopyTo(fileStream);
                    }
                    article.AdditionalImagePath1 = $"/images/{article.Id}_additional1.jpg";
                }
                if (model.AdditionalImage2 != null && model.AdditionalImage2.Length > 0)
                {
                    string additionalImagePath2 = Path.Combine(uploads, $"{article.Id}_additional2.jpg");
                    using (var fileStream = new FileStream(additionalImagePath2, FileMode.Create))
                    {
                        model.AdditionalImage2.CopyTo(fileStream);
                    }
                    article.AdditionalImagePath2 = $"/images/{article.Id}_additional2.jpg";
                }

                
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

            if (System.IO.File.Exists(coverPath))//è vero che sono obbligatori come file, ma è sempre bene mettere dei controlli perchè avremmo anche potuto cancellarli a mano i file in vs 
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

