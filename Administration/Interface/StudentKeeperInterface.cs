using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface StudentKeeperInterface
    {
        public Task<StudentKeeperDTO> CreateStudentKeeper(StudentKeeperDTO studentKeeperDTO);
        public Task<int> DeleteStudentKeeperByEmail(string Email);
        public Task<int> DeleteStudentKeeperId(int KeeperId);
        public Task<int> DeleteStudentByStudentId(int StudentId);
        public Task<int> DeleteKeeperByKeeperId(int KeeperId);
        public Task<IEnumerable<StudentKeeperDTO>> GetAllKeeperIdByStudentId(int StudentId);
        public Task<IEnumerable<StudentKeeperDTO>> GetAllStudentIdByKeeperId(int KeeperId);
    }
}
