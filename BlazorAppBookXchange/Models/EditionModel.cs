using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppBookXchange.Models
{
    public class EditionModel : LivreModel
    {
        public int IdEdition { get; set; }
        
        [DisplayName("Isbn")]
        [Required]
        public string Isbn { get; set; }

        [DisplayName("Date de parution")]
        [Required]
        public DateTime Parution { get; set; } = DateTime.UtcNow;

        [DisplayName("Format")]
        [Required]
        public string Format { get; set; }

        //public int IdLivre { get; set; }
        public bool IsRowExpanded { get; set; } = false;

    }
}
