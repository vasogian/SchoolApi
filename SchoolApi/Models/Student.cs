using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Student
    {      
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public Professor? Professor { get; set; } 
        public List<Subjects>? Subjects { get; set; } = new List<Subjects>();        
    }
}
