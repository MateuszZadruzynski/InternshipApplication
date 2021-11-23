using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Interface
{
    public interface DiaryInterface
    {
        public Task<IEnumerable<DiaryDTO>> GetDiaries(int studentId);
        public Task<DiaryDTO> GetDiary(int studentId);
        public Task<DiaryDTO> CreateDiary(DiaryDTO diaryDTO);
        public Task<DiaryDTO> UpdateDiary(int DiaryID, DiaryDTO diaryDTO);
        public Task<int> DeleteDiary(int DiaryId);
    }
}