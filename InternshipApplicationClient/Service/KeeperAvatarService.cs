using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class KeeperAvatarService : KeeperAvatarInterface
    {
        private readonly HttpClient _httpClient;

        public KeeperAvatarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KeeperAvatarsDTO> CreateKeeperAvatar(KeeperAvatarsDTO keeperAvatarsDTO)
        {
            try
            {
                var newData = JsonConvert.SerializeObject(keeperAvatarsDTO);
                var data = new StringContent(newData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/keeperavatar/create", data);
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<KeeperAvatarsDTO>(content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<int> DeleteKeeperAvatarByKeeperId(int keeperId)
        {
            var response = await _httpClient.DeleteAsync($"api/keeperavatar/delete/{keeperId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<KeeperAvatarsDTO> GetKeeperAvatar(int keeperId)
        {
            var response = await _httpClient.GetAsync($"api/keeperavatar/avatar/{keeperId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var keeperAvatarsData = JsonConvert.DeserializeObject<KeeperAvatarsDTO>(content);
            return keeperAvatarsData;
        }
    }
}
