using System;
using System.ComponentModel.DataAnnotations;

namespace Agendamento.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateSchedule { get; set; }

        [Required]
        public int StatusSchedule { get; set; }

        [Required]
        public int StatusPayment { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client Client { get; set; }

        [Required]
        public int ProcedureId { get; set; }

        public Procedure Procedure { get; set; }
    }
}