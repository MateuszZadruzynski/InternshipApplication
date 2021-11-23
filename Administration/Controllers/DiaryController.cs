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
    public class DiaryController : DiaryInterface
    {
        private readonly AppDbContext _database;
        private readonly IMapper _imapper;
        public DiaryController(AppDbContext database, IMapper mapper)
        {
            _imapper = mapper;
            _database = database;
        }
        public async Task<DiaryDTO> CreateDiary(DiaryDTO diaryDTO)
        {
            Diary diary = _imapper.Map<DiaryDTO, Diary>(diaryDTO);
            var addedDiary = await _database.diaries.AddAsync(diary);
            await _database.SaveChangesAsync();
            return _imapper.Map<Diary, DiaryDTO>(addedDiary.Entity);
        }

        public async Task<int> DeleteDiary(int DiaryId)
        {
            var diaryData = await _database.diaries.FindAsync(DiaryId);
            if (diaryData != null)
            {
                _database.diaries.Remove(diaryData);
                return await _database.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteDiaryByStudentId(int StudentId)
        {
            var diaryList = await _database.diaries.Where(i => i.StudentId == StudentId).ToListAsync();
            _database.diaries.RemoveRange(diaryList);
            return await _database.SaveChangesAsync();
        }

        public async Task<DiaryDTO> FindDiaryByDate(DateTime date)
        {
            try
            {
                DiaryDTO diaryDTO = _imapper.Map<Diary, DiaryDTO>(
                    await _database.diaries.FirstOrDefaultAsync(x => x.Date == date));

                return diaryDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<DiaryDTO> FindDiaryByStudentId(int StudentId)
        {
            try
            {
                DiaryDTO diaryDTO = _imapper.Map<Diary, DiaryDTO>(
                    await _database.diaries.FirstOrDefaultAsync(x => x.StudentId == StudentId));

                return diaryDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DiaryDTO>> GetAllDiaries()
        {
            try
            {
                IEnumerable<DiaryDTO> diaryDTOs =
                    _imapper.Map<IEnumerable<Diary>, IEnumerable<DiaryDTO>>(_database.diaries);
                return diaryDTOs;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DiaryDTO>> GetAllDiariesByStudent(int StudentId)
        {
            return _imapper.Map<IEnumerable<Diary>, IEnumerable<DiaryDTO>>(
            await _database.diaries.Where(x => x.StudentId == StudentId).ToListAsync());
        }

        public async Task<DiaryDTO> GetDiary(int DiaryId)
        {
            try
            {
                DiaryDTO diaryDTO = _imapper.Map<Diary, DiaryDTO>(
                    await _database.diaries.FirstOrDefaultAsync(x => x.DiaryId == DiaryId));

                return diaryDTO;
            }
            catch (Exception eror)
            {
                return null;
            }
        }

        public async Task<DiaryDTO> UpdateDiary(int DiaryId, DiaryDTO diaryDTO)
        {
            try
            {
                if (DiaryId == diaryDTO.DiaryId)
                {
                    Diary diaryData = await _database.diaries.FindAsync(DiaryId);
                    Diary diary = _imapper.Map<DiaryDTO, Diary>(diaryDTO, diaryData);
                    var updatedDiary = _database.diaries.Update(diary);
                    await _database.SaveChangesAsync();
                    return _imapper.Map<Diary, DiaryDTO>(updatedDiary.Entity);
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
