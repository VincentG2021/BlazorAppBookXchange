namespace BlazorAppBookXchange.Models
{
    public class EditionModel : LivreModel
    {
        public int IdEdition { get; set; }
        public string Isbn { get; set; }
        public DateTime Parution { get; set; }
        public string Format { get; set; }
        public int IdLivre { get; set; }
        public bool IsRowExpanded { get; set; } = false;

    }
}
