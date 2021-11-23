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
    public class KeeperService : KeeperInterface
    {
        private readonly HttpClient _httpClient;
        public KeeperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KeeperDTO> GetKeeper(int keeperId)
        {
            var response = await _httpClient.GetAsync($"api/keeper/{keeperId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var keeper = JsonConvert.DeserializeObject<KeeperDTO>(content); ;
            return keeper;
        }

        public async Task<IEnumerable<KeeperDTO>> GetAllCompanyKeepers(int CompanyId)
        {
            var response = await _httpClient.GetAsync($"api/keeper/company/{CompanyId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var keepers = JsonConvert.DeserializeObject<IEnumerable<KeeperDTO>>(content);
            return keepers;
        }
        public async Task<KeeperDTO> UpdateKeeper(int keeperId, KeeperDTO keeperDTO)
        {
            try
            {
                var keeperData = JsonConvert.SerializeObject(keeperDTO);
                var data = new StringContent(keeperData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/keeper/update/{keeperId}", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<KeeperDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<KeeperDTO> GetKeeperByEmail(string email)
        {
            var response = await _httpClient.GetAsync($"api/keeper/email/{email}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var keeper = JsonConvert.DeserializeObject<KeeperDTO>(content); ;
            return keeper;
        }

        public async Task<int> DeleteKeeper(int keeperId)
        {
            var response = await _httpClient.DeleteAsync($"api/keeper/delete/{keeperId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }
    }
}
