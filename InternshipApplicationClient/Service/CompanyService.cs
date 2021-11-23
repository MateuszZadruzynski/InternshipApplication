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
    public class CompanyService : CompanyInterface
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompanyDTO> CheckNIP(string nip)
        {
            var response = await _httpClient.GetAsync($"api/company/nip/{nip}");
            var content = await response.Content.ReadAsStringAsync();
            var student = JsonConvert.DeserializeObject<CompanyDTO>(content); ;
            return student;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            var response = await _httpClient.GetAsync($"api/company/all");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var company = JsonConvert.DeserializeObject<IEnumerable<CompanyDTO>>(content);
            return company;
        }

        public async Task<CompanyDTO> GetCompany(int CompanyId)
        {
            var response = await _httpClient.GetAsync($"api/company/{CompanyId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var company = JsonConvert.DeserializeObject<CompanyDTO>(content);
            return company;
        }

        public async Task<CompanyDTO> UpdateCompany(int companyId, CompanyDTO studentDTO)
        {
            try
            {
                var studentyData = JsonConvert.SerializeObject(studentDTO);
                var data = new StringContent(studentyData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/company/update/{companyId}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CompanyDTO>(content);
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
