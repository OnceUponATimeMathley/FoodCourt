using System;
using System.Collections.Generic;
using System.Text;
using FoodCourt.Mvvm.Commands;
using FoodCourt.Services;
using FoodCourt.ViewModels.Base;
using FoodCourt.Services.Dialog;

namespace FoodCourt.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        string _email;
        string _password;

        public LoginViewModel(AccountService accountService)
        {
            AccountService = accountService;

            LoginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => IsBusy)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Password);

            RegisterCommand = new DelegateCommand(Register);

            Title = "Login View"; //Strings.Localization.GetString("Login_Title");
        }

        public DelegateCommand RegisterCommand { get; }

        public DelegateCommand LoginCommand { get; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        void Register()
        {
            NavigationService.NavigateToAsync<RegisterViewModel>();
        }

        bool CanLogin()
        {
            return IsNotBusy && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        async void Login()
        {
            IsBusy = true;
            if (await AccountService.LoginAsync(Email, Password))
                await NavigationService.NavigateToAsync<DashboardViewModel>();
            else
               // await DialogService.AlertAsync("Login fail", "Login fail" );
            IsBusy = false;
        }

        AccountService AccountService { get; }
    }
}
