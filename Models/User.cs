using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Este campo deve possuir de 4 a 8 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir de 4 a 8 caracteres")]
        public string Password { get; set; }

        [MaxLength(20, ErrorMessage = "Este campo deve possuir no máximo 20 caracteres")]
        public string Role { get; set; }
    }
}