using WebAppScarpe_CoSabrinaCinque.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WebAppScarpe_CoSabrinaCinque.Services
{
    public class ArticleService : IArticleService
    {
        private static List<Article> articles = new List<Article>();

        public IEnumerable<Article> GetAll()
        {
            return articles;
        }

        public Article GetById(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Article article)
        {
            // Generare un nuovo ID in modo sicuro
            article.Id = articles.Any() ? articles.Max(a => a.Id) + 1 : 1;
            articles.Add(article);
        }

        public void Delete(int id)
        {
            var article = GetById(id);
            if (article != null)
            {
                articles.Remove(article);
            }
        }
    }
}
