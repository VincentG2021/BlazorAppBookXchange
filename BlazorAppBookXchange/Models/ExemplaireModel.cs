using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppBookXchange.Models
{
    public class ExemplaireModel : EditionModel
    {
        public int IdExemplaire { get; set; }
        public int IdMembre { get; set; }
        public int IdEdition { get; set; }
        public bool IsRowExpanded { get; set; } = false;

    }

    public class CreateExemplaireModel : EditionModel
    {
        public int IdMembre { get; set; }

        [DisplayName("Titre de l'ouvrage")]
        [Required]
        public string Titre { get; set; }

        [DisplayName("Auteur l'ouvrage")]
        [Required]
        public string Auteur { get; set; }

        [DisplayName("Résumé, synopsis")]
        [Required]
        public string Synopsis { get; set; }

        [DisplayName("Isbn")]
        [Required]
        public string Isbn { get; set; }

        [DisplayName("Date de parution")]
        [Required]
        public DateTime Parution { get; set; } = DateTime.UtcNow;

        [DisplayName("Format")]
        [Required]
        public string Format { get; set; }

        //[DisplayName("Disponibilité")]
        //[Required]
        //public bool? Disponible { get; set; }

    }


    public class EditExemplaireLivreModel : EditionModel
    {
        public int IdExemplaire { get; set; }

        [DisplayName("Titre de l'ouvrage")]
        [Required]
        public string Titre { get; set; }

        [DisplayName("Auteur l'ouvrage")]
        [Required]
        public string Auteur { get; set; }

        [DisplayName("Résumé, synopsis")]
        [Required]
        public string Synopsis { get; set; }

        [DisplayName("Isbn")]
        [Required]
        public string Isbn { get; set; }

        [DisplayName("Date de parution")]
        [Required]
        public DateTime Parution { get; set; }

        [DisplayName("Format")]
        [Required]
        public string Format { get; set; }

        //[DisplayName("Disponibilité")]
        //[Required]
        //public bool? Disponible { get; set; }

    }

}
