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
    public class StudentKeeperController : StudentKeeperInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public StudentKeeperController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<StudentKeeperDTO> CreateStudentKeeper(StudentKeeperDTO studentKeeperDTO)
        {
            StudentKeeper studentKeeper = _imapper.Map<StudentKeeperDTO, StudentKeeper>(studentKeeperDTO);
            var addedStudentKeeper = await _database.studentKeeper.AddAsync(studentKeeper);
            await _database.SaveChangesAsync();

            return _imapper.Map<StudentKeeper, StudentKeeperDTO>(addedStudentKeeper.Entity);
        }

        public async Task<int> DeleteStudentKeeperByEmail(string email)
        {
            var data = await _database.studentKeeper.FirstOrDefaultAsync(x => x.Email == email);
            _database.studentKeeper.Remove(data);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentKeeperId(int KeeperId)
        {
            var student = await _database.studentKeeper.FirstOrDefaultAsync(x => x.KeeperId == KeeperId);
            _database.studentKeeper.Remove(student);
            return await _database.SaveChangesAsync();
        }

        public async Task<int> DeleteStudentByStudentId(int StudentId)
        {
            var student = await _database.studentKeeper.FirstOrDefaultAsync(x => x.StudentId == StudentId);
            _database.studentKeeper.Remove(student);
            return await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentKeeperDTO>> GetAllKeeperIdByStudentId(int StudentId)
        {
            return _imapper.Map<IEnumerable<StudentKeeper>, IEnumerable<StudentKeeperDTO>>(
             await _database.studentKeeper.Where(x => x.StudentId == StudentId).ToListAsync());
        }

        public async Task<IEnumerable<StudentKeeperDTO>> GetAllStudentIdByKeeperId(int KeeperId)
        {
            return _imapper.Map<IEnumerable<StudentKeeper>, IEnumerable<StudentKeeperDTO>>(
            await _database.studentKeeper.Where(x => x.KeeperId == KeeperId).ToListAsync());
        }

        public async Task<int> DeleteKeeperByKeeperId(int KeeperId)
        {
            var keeper = await _database.studentKeeper.FirstOrDefaultAsync(x => x.KeeperId == KeeperId);
            _database.studentKeeper.Remove(keeper);
            return await _database.SaveChangesAsync();
        }
    }
}
