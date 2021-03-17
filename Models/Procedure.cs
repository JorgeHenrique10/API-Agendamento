using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir de 3 a 50 caracteres")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

    }
}