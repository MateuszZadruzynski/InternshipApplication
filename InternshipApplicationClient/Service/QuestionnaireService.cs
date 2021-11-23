using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class QuestionnaireService : QuestionnaireInterface
    {
        private readonly HttpClient _httpClient;

        public QuestionnaireService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<QuestionnaireModelDTO> CreateQuestionnaire(QuestionnaireModelDTO questionnaireModelDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(questionnaireModelDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/questionnaire/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<QuestionnaireModelDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteQuestionnaire(int QuestionnaireId)
        {
            var response = await _httpClient.DeleteAsync($"api/questionnaire/delete/{QuestionnaireId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<QuestionnaireModelDTO> GetQuestionnaireByKeeperIdAndStudentId(int StudentId, int KeeperId)
        {
            var response = await _httpClient.GetAsync($"api/questionnaire/student&keeper/{StudentId}/{KeeperId}");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<QuestionnaireModelDTO>(content);
            return data;
        }

        public async Task<IEnumerable<QuestionnaireModelDTO>> GetQuestionnaireByStudentId(int StudentId)
        { 
            var response = await _httpClient.GetAsync($"api/questionnaire/student/{StudentId}");
            var content = await response.Content.ReadAsStringAsync();
            var internships = JsonConvert.DeserializeObject<IEnumerable<QuestionnaireModelDTO>>(content);
            return internships;
        }

        public async Task<QuestionnaireModelDTO> UpdateQuestionnaire(int QuestionnaireId, QuestionnaireModelDTO questionnaireModelDTO)
        {
            try
            {
                var diaryData = JsonConvert.SerializeObject(questionnaireModelDTO);
                var data = new StringContent(diaryData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/questionnaire/update/{QuestionnaireId}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject< QuestionnaireModelDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
