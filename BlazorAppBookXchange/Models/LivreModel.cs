using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppBookXchange.Models
{
    public class LivreModel
    {
        public int IdLivre { get; set; }
        public string Titre { get; set; }

        public string Auteur { get; set; }

        public string Synopsis { get; set; }

    }

    public class AddLivreModel
    {
        [DisplayName("Titre de l'ouvrage")]
        [Required]
        public string Titre { get; set; }

        [DisplayName("Auteur l'ouvrage")]
        [Required]
        public string Auteur { get; set; }

        [DisplayName("Résumé, synopsis")]
        [Required]
        public string Synopsis { get; set; }
    }


    public class EditLivreModel
    {
        public int IdLivre { get; set; }

        [DisplayName("Titre de l'ouvrage")]
        [Required]
        public string Titre { get; set; }

        [DisplayName("Auteur l'ouvrage")]
        [Required]
        public string Auteur { get; set; }

        [DisplayName("Résumé, synopsis")]
        [Required]
        public string Synopsis { get; set; }

    }
}
