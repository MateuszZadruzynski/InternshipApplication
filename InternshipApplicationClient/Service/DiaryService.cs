using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class DiaryService : DiaryInterface
    {
        private readonly HttpClient _httpClient;
        public DiaryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DiaryDTO> CreateDiary(DiaryDTO diaryDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(diaryDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/diary/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DiaryDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<IEnumerable<DiaryDTO>> GetDiaries(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/diary/student/{studentId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var diary = JsonConvert.DeserializeObject<IEnumerable<DiaryDTO>>(content);
            return diary;
        }

        public async Task<DiaryDTO> GetDiary(int diaryId)
        {
            var response = await _httpClient.GetAsync($"api/diary/{diaryId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var diary = JsonConvert.DeserializeObject<DiaryDTO>(content);
            return diary;
        }

        public async Task<DiaryDTO> UpdateDiary(int diaryID, DiaryDTO DiaryDTO)
        {
            try
            {
                var diaryData = JsonConvert.SerializeObject(DiaryDTO);
                var data = new StringContent(diaryData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/diary/update/{diaryID}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DiaryDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteDiary(int diaryId)
        {
            var response = await _httpClient.DeleteAsync($"api/diary/delete/{diaryId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

    }
}
