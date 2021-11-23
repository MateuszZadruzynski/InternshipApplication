using Models;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface StudentAvatarInterface
    {
        public Task<StudentAvatarsDTO> CreateStudentAvatar(StudentAvatarsDTO studentAvatarsDTO);
        public Task<int> DeleteStudentAvatarByStudentId(int studentId);
        public Task<StudentAvatarsDTO> GetStudentAvatar(int studentId);
    }
}
