using Models;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface StudentInterface
    {
        public Task<StudentDTO> GetStudent(int studentId);
        public Task<StudentDTO> CheckIndex(string index);
        public Task<StudentDTO> GetStudentByEmail(string email);
        public Task<StudentDTO> UpdateStudent(int studentId, StudentDTO studentDTO);
    }
}
