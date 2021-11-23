using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class StudentService : StudentInterface
    {
        private readonly HttpClient _httpClient;
        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StudentDTO> CheckIndex(string index)
        {
            var response = await _httpClient.GetAsync($"api/student/index/{index}");
            var content = await response.Content.ReadAsStringAsync();
            var student = JsonConvert.DeserializeObject<StudentDTO>(content); ;
            return student;
        }

        public async Task<StudentDTO> GetStudent(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/student/{studentId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var student = JsonConvert.DeserializeObject<StudentDTO>(content); ;
            return student;
        }

        public async Task<StudentDTO> GetStudentByEmail(string email)
        {
            var response = await _httpClient.GetAsync($"api/student/email/{email}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var student = JsonConvert.DeserializeObject<StudentDTO>(content); ;
            return student;
        }

        public async Task<StudentDTO> UpdateStudent(int studentId, StudentDTO studentDTO)
        {
            try
            {
                var studentyData = JsonConvert.SerializeObject(studentDTO);
                var data = new StringContent(studentyData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/student/update/{studentId}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<StudentDTO>(content);
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
