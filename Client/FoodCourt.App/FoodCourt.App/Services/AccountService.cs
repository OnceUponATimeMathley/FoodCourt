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
        public Task<ApiResponse<object>> RegisterAsync(User user, string password)
        {
            var url = Configuration.ID_HOST + "/api/account";

            return HttpService.PostAsync<object>(url, new
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
