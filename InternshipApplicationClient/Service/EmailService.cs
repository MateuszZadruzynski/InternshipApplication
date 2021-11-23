using InternshipApplicationClient.Interface;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Utils;

namespace InternshipApplicationClient.Service
{
    public class EmailService : EmailInterface
    {
        private readonly HttpClient _httpClient;
        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Send(Email emailData)
        {
            var newData = JsonConvert.SerializeObject(emailData);
            var data = new StringContent(newData, Encoding.UTF8, "application/json");
            _httpClient.PostAsync($"api/email/send", data);
        }
    }
}