using System.Collections.Generic;
using System.Linq;
using WebAppScarpe_CoSabrinaCinque.Entities;

namespace WebAppScarpe_CoSabrinaCinque.Services
{
    public class ArticleService : IArticleService
    {
        private static List<Article> articles = new List<Article>();
        private static int lastId = 0;

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
            
            article.Id = ++lastId;
            articles.Add(article);
        }

        public void Delete(int id)//ho voluto incrementare anche con un metodo per cancellare il prodotto,disponibile il button sulla card
        {
            var article = GetById(id);
            if (article != null)
            {
                articles.Remove(article);
            }
        }
    }
}
