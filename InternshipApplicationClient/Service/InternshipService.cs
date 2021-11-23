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
    public class InternshipService : InternshipInterface
    {
        private readonly HttpClient _httpClient;
        public InternshipService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InternshipDTO> CreateInternship(InternshipDTO internshipDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(internshipDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/internship/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InternshipDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteInternship(int internshipId)
        {
            var response = await _httpClient.DeleteAsync($"api/internship/delete/{internshipId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<InternshipDTO> GetInternship(int internshipId)
        {
            var response = await _httpClient.GetAsync($"api/internship/{internshipId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var internship = JsonConvert.DeserializeObject<InternshipDTO>(content);
            return internship;
        }

        public async Task<IEnumerable<InternshipDTO>> GetInternships()
        {
            var response = await _httpClient.GetAsync($"api/internship");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var internships = JsonConvert.DeserializeObject<IEnumerable<InternshipDTO>>(content);
            return internships;
        }

        public async Task<IEnumerable<InternshipDTO>> GetInternshipsByCompanyId(int CompanyId)
        {
            var response = await _httpClient.GetAsync($"api/internship/company/{CompanyId}");
            var content = await response.Content.ReadAsStringAsync();
            var internships = JsonConvert.DeserializeObject<IEnumerable<InternshipDTO>>(content);
            return internships;
        }

        public async Task<InternshipDTO> UpdateInternship(int internshipId, InternshipDTO internshipDTO)
        {
            try
            {
                var diaryData = JsonConvert.SerializeObject(internshipDTO);
                var data = new StringContent(diaryData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/internship/update/{internshipId}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InternshipDTO>(content);
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
