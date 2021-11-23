using Models;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface KeeperAvatarInterface
    {
        public Task<KeeperAvatarsDTO> CreateKeeperAvatar(KeeperAvatarsDTO keeperAvatarsDTO);
        public Task<int> DeleteKeeperAvatarByKeeperId(int keeperId);
        public Task<KeeperAvatarsDTO> GetKeeperAvatar(int keeperId);
    }
}
