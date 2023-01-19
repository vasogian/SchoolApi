using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Subjects
    {
        [Key]
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int HoursToComplete { get; set; }
               
    }
}
