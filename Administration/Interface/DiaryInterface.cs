using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Interface
{
    public interface DiaryInterface
    {
        public Task<DiaryDTO> CreateDiary(DiaryDTO diaryDTO);
        public Task<DiaryDTO> UpdateDiary(int DiaryId, DiaryDTO diaryDTO);
        public Task<DiaryDTO> GetDiary(int DiaryId);
        public Task<int> DeleteDiary(int DiaryId);
        public Task<int> DeleteDiaryByStudentId(int StudentId);
        public Task<IEnumerable<DiaryDTO>> GetAllDiaries();
        public Task<DiaryDTO> FindDiaryByDate(DateTime date);
        public Task<DiaryDTO> FindDiaryByStudentId(int StudentId);
        public Task<IEnumerable<DiaryDTO>> GetAllDiariesByStudent(int StudentId);
    }
}
