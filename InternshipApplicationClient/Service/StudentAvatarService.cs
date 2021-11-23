using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class StudentAvatarService : StudentAvatarInterface
    {
        private readonly HttpClient _httpClient;

        public StudentAvatarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<StudentAvatarsDTO> CreateStudentAvatar(StudentAvatarsDTO studentAvatarsDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(studentAvatarsDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/studentavatar/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<StudentAvatarsDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteStudentAvatarByStudentId(int studentId)
        {
            var response = await _httpClient.DeleteAsync($"api/studentavatar/delete/{studentId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<StudentAvatarsDTO> GetStudentAvatar(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/studentavatar/avatar/{studentId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var studentAvatarsData = JsonConvert.DeserializeObject<StudentAvatarsDTO>(content);
            return studentAvatarsData;
        }
    }
}
