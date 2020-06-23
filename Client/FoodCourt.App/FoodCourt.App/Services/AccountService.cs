using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FoodCourt.Models;

namespace FoodCourt.Services
{
    public class AccountService
    {
        private HttpService HttpService { get; }

        public AccountService(HttpService httpService)
        {
            HttpService = httpService;
        }

        //Task<ApiResponse<object>> --> Task<bool>
        public async Task<bool> LoginAsync(string email, string password)
        {
            var url = Configuration.ID_HOST + "/connect/token";

            var response = await HttpService.PostAsync<TokenResponse>(url, new Dictionary<string, string>()
            {
                {"client_id", "client"},
                {"client_secret", "secret"},
                {"grant_type","password"},
                {"username", email},
                {"password", password}
            });

            if (!string.IsNullOrEmpty(response.Error))
            {
                switch (response.Error)
                {
                    case "invalid_grant":
                        return false;
                    default:
                        return false;
                }
            }
            return !string.IsNullOrEmpty(response.AccessToken);

        }
        public Task<ApiResponse<object>> RegisterAsync(User user, string password)
        {
            var url = Configuration.ID_HOST + "/api/account";

            return HttpService.PostApiAsync<object>(url, new
            {
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Gender,
                user.BirthDate,
                Password = password
            });

        }
    }
}
