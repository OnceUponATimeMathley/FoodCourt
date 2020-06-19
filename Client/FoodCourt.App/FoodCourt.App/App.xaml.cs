﻿using System;
using Autofac.Core;
using FoodCourt.Services.Navigation;
using FoodCourt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BuildDependencies();

            InitNavigation();
        }

        private void InitNavigation()
        {
            ServiceLocator.Instance.Resolve<INavigationService>().NavigateToAsync<LoginViewModel>();
        }

        private void BuildDependencies()
        {
            if (ServiceLocator.Instance.Built)
                return;
            //Register Dependencies
            ServiceLocator.Instance.RegisterInstance<Services.Navigation.INavigationService, Services.Navigation.NavigationService>();

            ServiceLocator.Instance.RegisterViewModels();

            ServiceLocator.Instance.Build();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
