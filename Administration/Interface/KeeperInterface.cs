using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface KeeperInterface
    {
        public Task<KeeperDTO> CreateKeeper(KeeperDTO keeperDTO);
        public Task<KeeperDTO> UpdateKeeper(int KeeperId, KeeperDTO keeperDTO);
        public Task<KeeperDTO> GetKeeper(int KeeperId);
        public Task<int> DeleteKeeper(int KeeperId);
        public Task<KeeperDTO> GetKeeperByEmail(string email);
        public Task<IEnumerable<KeeperDTO>> GetAllKeepers();
        public Task<KeeperDTO> FindKeeperByLastName(string name);
        public Task<KeeperDTO> FindKeeperByName(string name);
        public Task<IEnumerable<KeeperDTO>> GetAllCompanyKeepers(int CompanyId);
    }
}
