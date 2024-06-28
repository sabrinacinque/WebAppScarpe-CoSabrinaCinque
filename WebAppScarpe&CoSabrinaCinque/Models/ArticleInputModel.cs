using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebAppScarpe_CoSabrinaCinque.Models
{
    public class ArticleInputModel
    {
        [Required(ErrorMessage = "Il nome dell'articolo è obbligatorio")]
        [Display(Name = "Nome dell'articolo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il prezzo è obbligatorio")]
        [Display(Name = "Prezzo")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'immagine di copertina è obbligatoria")]
        [Display(Name = "Immagine di copertina")]
        public IFormFile Cover { get; set; }

        [Required(ErrorMessage = "L'immagine aggiuntiva 1 è obbligatoria")]
        [Display(Name = "Immagine aggiuntiva 1")]
        public IFormFile AdditionalImage1 { get; set; }

        [Required(ErrorMessage = "L'immagine aggiuntiva 2 è obbligatoria")]
        [Display(Name = "Immagine aggiuntiva 2")]
        public IFormFile AdditionalImage2 { get; set; }
    }
}
