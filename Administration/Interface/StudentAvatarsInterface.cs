using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface StudentAvatarsInterface
    {
        public Task<StudentAvatarsDTO> CreateAvatar(StudentAvatarsDTO AvatarDTO);
        public Task<int> DeleteAvatarByAvatarId(int AvatarId);

        public Task<int> DeleteAvatarByAvatarUrl(string AvatarUrl);
        public Task<int> DeleteAvatarByStudentId(int StudentId);
        public Task<StudentAvatarsDTO> GetStudentAvatar(int StudentId);
    }
}
