using System;
using System.Collections.Generic;
using System.Text;
using FoodCourt.Mvvm.Commands;
using FoodCourt.ViewModels.Base;

namespace FoodCourt.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login);

            Title = "Login View";
        }

        private void Login()
        {
            NavigationService.NavigateToAsync<DashboardViewModel>();
        }

        public DelegateCommand LoginCommand { get; }
    }
}
