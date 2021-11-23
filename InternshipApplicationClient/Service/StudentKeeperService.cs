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
    public class StudentKeeperService : StudentKeeperInterface
    {
        private readonly HttpClient _httpClient;
        public StudentKeeperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> CreateStudentKeeper(StudentKeeperDTO studentKeeperDTO)
        {
            var newData = JsonConvert.SerializeObject(studentKeeperDTO);
            var data = new StringContent(newData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/studentkeeper/create", data);
            if (response != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }

        public async Task<int> DeleteKeeperByKeeperId(int KeeperId)
        {
            var response = await _httpClient.DeleteAsync($"/api/studentkeeper/delete/{KeeperId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<int> DeleteStudentByStudentId(int StudentId)
        {

            var response = await _httpClient.DeleteAsync($"/api/studentkeeper/delete/{StudentId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<IEnumerable<StudentKeeperDTO>> GetAllKeeperIdByStudentId(int StudentId)
        {
            var response = await _httpClient.GetAsync($"/api/studentkeeper/keepers/{StudentId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var StudentKeeper = JsonConvert.DeserializeObject<IEnumerable<StudentKeeperDTO>>(content);
            return StudentKeeper;
        }

        public async Task<IEnumerable<StudentKeeperDTO>> GetAllStudentIdByKeeperId(int KeeperId)
        {
            var response = await _httpClient.GetAsync($"/api/studentkeeper/students/{KeeperId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var StudentKeeper = JsonConvert.DeserializeObject<IEnumerable<StudentKeeperDTO>>(content);
            return StudentKeeper;
        }
    }
}
