using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface KeeperInterface
    {
        public Task<KeeperDTO> GetKeeper(int keeperId);
        public Task<KeeperDTO> UpdateKeeper(int keeperId, KeeperDTO keeperDTO);
        public Task<KeeperDTO> GetKeeperByEmail(string email);
        public Task<IEnumerable<KeeperDTO>> GetAllCompanyKeepers(int companyId);
        public Task<int> DeleteKeeper(int keeperId);
    }
}
