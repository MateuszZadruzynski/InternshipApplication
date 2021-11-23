using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface StudentInterface
    {
        public Task<StudentDTO> CreateStudent(StudentDTO studentDTO);
        public Task<StudentDTO> UpdateStudent(int Id, StudentDTO studentDTO);
        public Task<StudentDTO> GetStudent(int Id);
        public Task<int> DeleteStudent(int Id);
        public Task<IEnumerable<StudentDTO>> GetAllStudents();
        public Task<StudentDTO> FindStudentByLastName(string name);
        public Task<StudentDTO> FindStudentByName(string name);
        public Task<StudentDTO> GetStudentByEmail(string email);
        public Task<StudentDTO> GetStudentByIndex(string index);
        public Task<StudentDTO> IsIndexUnique(string index, int StudentId = 0);
    }
}
