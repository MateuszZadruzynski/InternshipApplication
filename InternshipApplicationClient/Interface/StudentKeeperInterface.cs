using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    interface StudentKeeperInterface
    {
        public Task<string> CreateStudentKeeper(StudentKeeperDTO studentKeeperDTO);
        public Task<int> DeleteStudentByStudentId(int StudentId);
        public Task<int> DeleteKeeperByKeeperId(int KeeperId);
        public Task<IEnumerable<StudentKeeperDTO>> GetAllKeeperIdByStudentId(int StudentId);
        public Task<IEnumerable<StudentKeeperDTO>> GetAllStudentIdByKeeperId(int KeeperId);
    }
}
