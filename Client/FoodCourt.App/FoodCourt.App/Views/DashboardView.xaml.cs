using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.Services.Navigation;
using FoodCourt.ViewModels;
using FoodCourt.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView : TabbedPage
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (CurrentPage != null)
            {
                SetViewModelByView(CurrentPage);
            }
        }

        private void SetViewModelByView(Page view)
        {
            try
            {
                if (view.BindingContext!=null && view.BindingContext?.GetType()!=typeof(DashboardViewModel) && view.BindingContext is ViewModelBase viewModel)
                {
                    viewModel.OnNavigationAsync(new NavigationParameters(), NavigationType.Back);
                    return;
                }
                

                var viewType = view.GetType();
                if (viewType.FullName == null)
                    return;

                var viewModelType = Type.GetType(viewType.FullName.Replace("View", "ViewModel"));

                if(viewModelType == null)
                    throw new Exception($"Mapping type for {view}  is  not a exist");

                viewModel = ServiceLocator.Instance.Resolve(viewModelType) as ViewModelBase;

                if (viewModel != null)
                {
                    view.BindingContext = viewModel;
                    viewModel.OnNavigationAsync(new NavigationParameters(), NavigationType.New);
                }
            }
            catch(Exception ex)
            {
                Debugger.Break();

                throw;
            }
        }
    }
}