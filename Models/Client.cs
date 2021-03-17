using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Este campo deve conter no maximo 50 caracteres")]
        [EmailAddress]
        public string Email { get; set; }
    }
}