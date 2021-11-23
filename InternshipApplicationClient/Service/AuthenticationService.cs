using Blazored.LocalStorage;
using InternshipApplicationClient.Interface;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace InternshipApplicationClient.Service
{
    public class AuthenticationService : AuthenticationInterface
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _browserStorage;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService browserStorage)
        {
            _httpClient = httpClient;
            _browserStorage = browserStorage;

        }
        public async Task<Registration> SignUpStudent(StudentDTO student)
        {
            var studentData = JsonConvert.SerializeObject(student);
            var data = new StringContent(studentData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/student/signup", data);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Registration>(content);
            if (response.IsSuccessStatusCode)
            {
                return new Registration { isSuccessful = true };
            }
            else
            {
                return new Registration { isSuccessful = false };
            }
        }
        public async Task<Registration> SignUpCompany(CompanyDTO company)
        {
            var companyData = JsonConvert.SerializeObject(company);
            var data = new StringContent(companyData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/company/signup", data);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Registration>(content);
            if (response.IsSuccessStatusCode)
            {
                return new Registration { isSuccessful = true };
            }
            else
            {
                return new Registration { isSuccessful = false };
            }
        }

        public async Task<Registration> SignUpKeeper(KeeperDTO keeper)
        {
            var keeperData = JsonConvert.SerializeObject(keeper);
            var data = new StringContent(keeperData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/keeper/signup", data);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Registration>(content);
            if (response.IsSuccessStatusCode)
            {
                return new Registration { isSuccessful = true };
            }
            else
            {
                return new Registration { isSuccessful = false };
            }
        }

        public async Task<SignInAuthentication> LoginStudent(AuthenticationUser authenticationUser)
        {
            var content = JsonConvert.SerializeObject(authenticationUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/student/singin", data);
            var contentTMP = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInAuthentication>(contentTMP);
            if (!response.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                await _browserStorage.SetItemAsync(AuthenticationData.Token, result.Token);
                await _browserStorage.SetItemAsync(AuthenticationData.UserData, result.studentDTO);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInAuthentication { isAuthenticationSuccessful = true };
            }
        }

        public async Task<SignInAuthentication> LoginKeeper(AuthenticationUser authenticationUser)
        {
            var content = JsonConvert.SerializeObject(authenticationUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/keeper/singin", data);
            var contentTMP = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInAuthentication>(contentTMP);
            if (!response.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                await _browserStorage.SetItemAsync(AuthenticationData.Token, result.Token);
                await _browserStorage.SetItemAsync(AuthenticationData.UserData, result.keeperDTO);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInAuthentication { isAuthenticationSuccessful = true };
            }
        }

        public async Task<SignInAuthentication> LoginCompany(AuthenticationUser authenticationUser)
        {
            var content = JsonConvert.SerializeObject(authenticationUser);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/company/singin", data);
            var contentTMP = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInAuthentication>(contentTMP);
            if (!response.IsSuccessStatusCode)
            {
                return result;
            }
            else
            {
                await _browserStorage.SetItemAsync(AuthenticationData.Token, result.Token);
                await _browserStorage.SetItemAsync(AuthenticationData.UserData, result.companyDTO);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInAuthentication { isAuthenticationSuccessful = true };
            }
        }

        public async Task Logout()
        {
            await _browserStorage.RemoveItemAsync(AuthenticationData.Token);
            await _browserStorage.RemoveItemAsync(AuthenticationData.UserData);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
