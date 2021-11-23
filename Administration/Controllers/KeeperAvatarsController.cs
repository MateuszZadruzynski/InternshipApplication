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
    public class KeeperAvatarsController : KeeperAvatarsInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public KeeperAvatarsController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }

        public async Task<KeeperAvatarsDTO> CreateAvatar(KeeperAvatarsDTO AvatarDTO)
        {
            KeeperAvatars avatar = _imapper.Map<KeeperAvatarsDTO, KeeperAvatars>(AvatarDTO);
            var add = await _database.keeperAvatars.AddAsync(avatar);
            await _database.SaveChangesAsync();
            return _imapper.Map<KeeperAvatars, KeeperAvatarsDTO>(add.Entity);
        }

        public async Task<int> DeleteAvatarByAvatarId(int AvatarId)
        {
            var avatar = await _database.keeperAvatars.FindAsync(AvatarId);
            _database.keeperAvatars.Remove(avatar);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteAvatarByAvatarUrl(string AvatarUrl)
        {
            var avatars = await _database.keeperAvatars.FirstOrDefaultAsync(x => x.URL.ToLower() == AvatarUrl.ToLower());
            _database.keeperAvatars.Remove(avatars);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteAvatarByKeeperId(int KeeperId)
        {
            var avatarList = await _database.keeperAvatars.Where(x => x.KeeperId == KeeperId).ToListAsync();
            _database.keeperAvatars.RemoveRange(avatarList);
            return await _database.SaveChangesAsync();
        }

        public async Task<KeeperAvatarsDTO> GetKeeperAvatar(int KeeperId)
        {
            return _imapper.Map<KeeperAvatars, KeeperAvatarsDTO>(
            await _database.keeperAvatars.FirstOrDefaultAsync(x => x.KeeperId == KeeperId));
        }
    }
}
