using Blazored.LocalStorage;
using DataModel.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MotorReparationTicketWASM.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Common;

namespace MotorReparationTicketWASM.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<RegisterResponseDTO> RegisterUser(RegisterDTO registerDTO)
        {
            var content = JsonConvert.SerializeObject(registerDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {

                return new RegisterResponseDTO() { IsRegisterationSuccessful = true };
            }
            return result;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
        {
            var content = JsonConvert.SerializeObject(loginDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(Auth.AUTH_TOKEN, result.Token);
                await _localStorage.SetItemAsync(Auth.USER_DETAIL, result.UserInfoDTO);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                //((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                return new LoginResponseDTO { IsAuthSuccessful = true };
            }
            return result;
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync(Auth.AUTH_TOKEN);
            await _localStorage.RemoveItemAsync(Auth.USER_DETAIL);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
