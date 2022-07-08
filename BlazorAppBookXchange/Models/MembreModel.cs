namespace BlazorAppBookXchange.Models
{
    public class MembreModel
    {
        public int IdMembre { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string? Prenom { get; set; }
        public string? Nom { get; set; }
        public string Pwd { get; set; }
        public int Role { get; set; }
    }
}
