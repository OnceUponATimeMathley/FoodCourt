using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.Services.Navigation;

namespace FoodCourt.ViewModels.Base
{
    public class ViewModelBase : Mvvm.BindableBase
    {
        protected INavigationService NavigationService { get; }
        public ViewModelBase()
        {
            NavigationService = ServiceLocator.Instance.Resolve<INavigationService>();
        }
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value , ()=> RaisePropertyChanged(nameof(IsNotBusy)));
        }

        public bool IsNotBusy => !IsBusy;

        //Lấy parameter truyền xuống ViewModel khi Navigation, và loại  NavgationType
        public virtual Task OnNavigationAsync(NavigationParameters parameter, NavigationType navigationType)
        {
            return Task.CompletedTask;
        }
    }
}
