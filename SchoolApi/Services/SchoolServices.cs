using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Services
{
    public class SchoolServices
    {
        private readonly StudentContext _studentContext;
        public SchoolServices(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            var allStudents = await _studentContext.Students              
                .Include(x => x.Subjects)
                .Include(x=>x.Professor)                 
                .ToListAsync();
            if (allStudents.Any())
            {
                return allStudents;
            }
            return new List<Student>();
        }
        public async Task<Student> GetStudentById(int id)
        {
            var selectedStudent = await _studentContext.Students
                .Include(x=>x.Subjects)
                .Include(x => x.Professor)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (selectedStudent is null)
            {
                return new Student();
            }
            return selectedStudent;
        }
        public async Task AddStudent(Student student)
        {
             _studentContext.Students.Add(student);
            await _studentContext.SaveChangesAsync();

        }
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            var studentToUpdate = await GetStudentById(id);
            if (studentToUpdate != null)
            {
                studentToUpdate.Name = student.Name;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Age = student.Age;
                studentToUpdate.Professor = student.Professor;
                studentToUpdate.Subjects = student.Subjects;
                await _studentContext.SaveChangesAsync();
            }
            return studentToUpdate;
        }
        public async Task<Student> DeleteStudent(int id)
        {
            var studentToBeDeleted = await GetStudentById(id);
            if (studentToBeDeleted != null)
            {
                _studentContext.Students
                    .Remove(studentToBeDeleted);
                await _studentContext.SaveChangesAsync();   
            }
            return studentToBeDeleted;

        }
    }
}
