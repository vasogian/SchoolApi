using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Professor
    {
        [Key]
        public int ProfId { get; set; } 
        public string? ProfName { get; set; }
        public string? ProfLastName { get; set; }
    }
}
