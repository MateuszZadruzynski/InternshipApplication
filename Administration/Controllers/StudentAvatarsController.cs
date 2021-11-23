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
    public class StudentAvatarsController : StudentAvatarsInterface
    {

        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public StudentAvatarsController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }

        public async Task<StudentAvatarsDTO> CreateAvatar(StudentAvatarsDTO AvatarDTO)
        {
            StudentAvatars avatar = _imapper.Map<StudentAvatarsDTO, StudentAvatars>(AvatarDTO);
            var add = await _database.studentAvatars.AddAsync(avatar);
            await _database.SaveChangesAsync();
            return _imapper.Map<StudentAvatars, StudentAvatarsDTO>(add.Entity);
        }

        public async Task<int> DeleteAvatarByAvatarId(int AvatarId)
        {
            var avatar = await _database.studentAvatars.FindAsync(AvatarId);
            _database.studentAvatars.Remove(avatar);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteAvatarByAvatarUrl(string AvatarUrl)
        {
            var avatars = await _database.studentAvatars.FirstOrDefaultAsync(x => x.URL.ToLower() == AvatarUrl.ToLower());
            _database.studentAvatars.Remove(avatars);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteAvatarByStudentId(int StudentId)
        {
            var avatarList = await _database.studentAvatars.Where(x => x.StudentId == StudentId).ToListAsync();
            _database.studentAvatars.RemoveRange(avatarList);
            return await _database.SaveChangesAsync();
        }

        public async Task<StudentAvatarsDTO> GetStudentAvatar(int StudentId)
        {
            return _imapper.Map<StudentAvatars, StudentAvatarsDTO>(
            await _database.studentAvatars.FirstOrDefaultAsync(x => x.StudentId == StudentId));
        }
    }
}
