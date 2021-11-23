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
    public class CompanyImageService : CompanyImageInterface
    {
        private readonly HttpClient _httpClient;

        public CompanyImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompanyImageDTO> CreateCompanyImage(CompanyImageDTO companyImageDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(companyImageDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/companyimage/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CompanyImageDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteCompanyImageByCompanyId(int companyId)
        {
            var response = await _httpClient.DeleteAsync($"api/companyimage/delete/{companyId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<IEnumerable<CompanyImageDTO>> GetAllCompanyImages()
        {
            var response = await _httpClient.GetAsync($"api/companyimage/images/all");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var company = JsonConvert.DeserializeObject<IEnumerable<CompanyImageDTO>> (content);
            return company;
        }

        public async Task<CompanyImageDTO> GetCompanyImages(int companyId)
        {
            var response = await _httpClient.GetAsync($"api/companyimage/image/{companyId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var companyImageData = JsonConvert.DeserializeObject<CompanyImageDTO>(content);
            return companyImageData;
        }

    }
}
