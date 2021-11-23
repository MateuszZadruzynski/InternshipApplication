using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface KeeperAvatarsInterface
    {
        public Task<KeeperAvatarsDTO> CreateAvatar(KeeperAvatarsDTO AvatarDTO);
        public Task<int> DeleteAvatarByAvatarId(int AvatarId);

        public Task<int> DeleteAvatarByAvatarUrl(string AvatarUrl);
        public Task<int> DeleteAvatarByKeeperId(int KeeperId);
        public Task<KeeperAvatarsDTO> GetKeeperAvatar(int KeeperId);
    }
}
