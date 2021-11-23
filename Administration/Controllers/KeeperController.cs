using Administration.Interface;
using AutoMapper;
using DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Controllers
{
    public class KeeperController : KeeperInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public KeeperController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<KeeperDTO> CreateKeeper(KeeperDTO keeperDTO)
        {
            Keeper keeper = _imapper.Map<KeeperDTO, Keeper>(keeperDTO);
            var addedKeepers = await _database.keepers.AddAsync(keeper);
            await _database.SaveChangesAsync();
            return _imapper.Map<Keeper, KeeperDTO>(addedKeepers.Entity);
        }

        public async Task<int> DeleteKeeper(int KeeperId)
        {
            var keeperData = await _database.keepers.FindAsync(KeeperId);
            if (keeperData != null)
            {
                var avatars = await _database.keeperAvatars.Where(a => a.KeeperId == KeeperId).ToListAsync();

                _database.keeperAvatars.RemoveRange();
                _database.keepers.Remove(keeperData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<KeeperDTO> FindKeeperByLastName(string lastName)
        {
            try
            {
                KeeperDTO keeperDTO = _imapper.Map<Keeper, KeeperDTO>(
                    await _database.keepers.FirstOrDefaultAsync(x => x.lastName.ToLower() == lastName));

                return keeperDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<KeeperDTO> FindKeeperByName(string name)
        {
            try
            {
                KeeperDTO keeperDTO = _imapper.Map<Keeper, KeeperDTO>(
                    await _database.keepers.FirstOrDefaultAsync(x => x.name.ToLower() == name));

                return keeperDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<KeeperDTO>> GetAllCompanyKeepers(int CompanyId)
        {
            return _imapper.Map<IEnumerable<Keeper>, IEnumerable<KeeperDTO>>(
             await _database.keepers.Where(x => x.CompanyId == CompanyId).ToListAsync());
        }

        public async Task<IEnumerable<KeeperDTO>> GetAllKeepers()
        {
            try
            {
                IEnumerable<KeeperDTO> keeperDTOs =
                    _imapper.Map<IEnumerable<Keeper>, IEnumerable<KeeperDTO>>(_database.keepers.Include( a => a.KeeperAvatars).Include(s => s.Students));
                return keeperDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<KeeperDTO> GetKeeper(int KeeperId)
        {
            try
            {
                KeeperDTO keeperDTO = _imapper.Map<Keeper, KeeperDTO>(
                    await _database.keepers.Include(a => a.KeeperAvatars).Include(s =>s.Students).FirstOrDefaultAsync(x => x.KeeperId == KeeperId));

                return keeperDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<KeeperDTO> GetKeeperByEmail(string email)
        {
            try
            {
                KeeperDTO keeperDTO = _imapper.Map<Keeper, KeeperDTO>(
                    await _database.keepers.FirstOrDefaultAsync(x => x.email.ToLower() == email));

                return keeperDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<KeeperDTO> UpdateKeeper(int KeeperId, KeeperDTO keeperDTO)
        {
            try
            {
                if (KeeperId == keeperDTO.KeeperId)
                {
                    Keeper keeperData = await _database.keepers.FindAsync(KeeperId);
                    Keeper keeper = _imapper.Map<KeeperDTO, Keeper>(keeperDTO, keeperData);
                    var updatedKeeper = _database.keepers.Update(keeper);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<Keeper, KeeperDTO>(updatedKeeper.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception eror)
            {
                return null;
            }
        }
    }
}
