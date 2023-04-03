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
                .Include(x => x.Professor)
                .Include(x => x.Subjects)
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
                .Include(x => x.Subjects)
                .Include(x => x.Professor)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (selectedStudent is null)
            {
                return new Student();
            }
            return selectedStudent;
        }
        public async Task<Student> GetStudentByName(string name)
        {
            var studentByName = await _studentContext.Students
                .Include(x => x.Subjects)
                .Include(x => x.Professor)
                .FirstOrDefaultAsync(x => x.Name == name);
            if (studentByName is null)
            {
                return new Student();
            }
            return studentByName;
        }


        public async Task<Student>AddStudent(Student student)
        {
            if (student != null)
            {
                _studentContext.Students.Add(student);
                await _studentContext.SaveChangesAsync();
            }
            return new Student();

        }
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            var studentToUpdate = await GetStudentById(id);
            if (studentToUpdate is not null)
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

        public async Task<Professor> GetProfessorById(int id)
        {
            var selectedProfessor = await _studentContext.Professors
                .FirstOrDefaultAsync(x => x.ProfId == id);
            if (selectedProfessor is null)
            {
                return new Professor();
            }
            return selectedProfessor;
        }
        public async Task CreateProfessor(Professor professor)
        {
            this._studentContext.Professors.Add(professor);
            await _studentContext.SaveChangesAsync();
        }

        public async Task<Professor> UpdateProfessor(int id, Professor professor)
        {
            var professorToBeUpdated = await GetProfessorById(id);
            if (professorToBeUpdated is not null)
            {
                professorToBeUpdated.ProfName = professor.ProfName;
                professorToBeUpdated.ProfLastName = professor.ProfLastName;
                await _studentContext.SaveChangesAsync();
            }
            return new Professor();
        }
        public async Task<Professor> DeleteProfessor(int id)
        {
            var professorToBeDeleted = await GetProfessorById(id);
            if (professorToBeDeleted is not null)
            {
                _studentContext.Professors.Remove(professorToBeDeleted);
                await _studentContext.SaveChangesAsync();
            }
            return professorToBeDeleted;

        }
        public async Task<Subjects> GetSubjectById(int id)
        {
            var selectedSubject = await _studentContext.Subjects
                .FirstOrDefaultAsync(x => x.SubjectId == id);
            if(selectedSubject is null)
            {
                return new Subjects();
            }
            return selectedSubject;
        }


        public async Task CreateSubject(Subjects subject)
        {
            this._studentContext.Subjects.Add(subject);
            await _studentContext.SaveChangesAsync();
        }

        public async Task<Subjects> UpdateSubject(int id, Subjects subject)
        {
            var subjectToBeUpdated = await GetSubjectById(id);
            if(subjectToBeUpdated is not null)
            {
                subjectToBeUpdated.SubjectName = subject.SubjectName;
                subjectToBeUpdated.HoursToComplete = subject.HoursToComplete;
                await _studentContext.SaveChangesAsync();
            }
            return new Subjects();
        }
        public async Task<Subjects> DeleteSubject(int id)
        {
            var subjectToBeDeleted = await GetSubjectById(id);
            if(subjectToBeDeleted is not null)
            {
               _studentContext.Subjects.Remove(subjectToBeDeleted);
                await _studentContext.SaveChangesAsync();
            }
            return subjectToBeDeleted;
        }

    }
}
