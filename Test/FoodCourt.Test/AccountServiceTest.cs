using System;
using FoodCourt.Models;
using FoodCourt.Services;
using Xunit;

namespace FoodCourt.Test
{
    public class AccountServiceTest
    {
        [Fact]
        public async void Register()
        {
            var accountService = new AccountService(new HttpService());

            var result = await accountService.RegisterAsync(new User()
            {
                Firstname = "Le",
                Lastname = "Long",
                Gender = "Male",
                Email = "longromsay2@gmail.com",
            },"123456");
            Assert.True(result.Successful);
            
        }
    }
}
