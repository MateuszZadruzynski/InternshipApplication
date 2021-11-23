using InternshipApplicationClient.Interface;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
{
    public class UploadService : UploadInterface
    {
        private readonly HttpClient _httpClient;
        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> DeleteImage(string name)
        {
            var response = await _httpClient.DeleteAsync($"api/upload/delete/{name}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            return 0;
        }

        public async Task<string> UploadImage(MultipartFormDataContent multipartFormDataContent)
        {
            var response = await _httpClient.PostAsync($"https://localhost:44344/api/upload/file", multipartFormDataContent);
            var postResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(postResponse);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:44344/", postResponse);
                return imgUrl;
            }
        }
    }
}
