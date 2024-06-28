using WebAppScarpe_CoSabrinaCinque.Entities;
using System.Collections.Generic;

namespace WebAppScarpe_CoSabrinaCinque.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAll();
        Article GetById(int id);
        void Create(Article article);
        void Delete(int id);
    }
}
