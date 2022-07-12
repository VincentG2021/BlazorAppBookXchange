using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    public class LoginMembreModel
    {
        [DisplayName("Pseudo")]
        [Required]
        public string Pseudo { get; set; }

        [DisplayName("Mot de passe")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }

    public class ConnectedMember
    {
        public int IdMembre { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string? Prenom { get; set; }
        public string? Nom { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}