using FoodCourt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public Stalls stalls;
        public HomeView()
        {
            InitializeComponent();
            stalls = new Stalls
            {
                ListStalls = Stalls.Get()
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionViewListHorizontal.ItemsSource = stalls.ListStalls.ToList();
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int number;
            string id = ((Frame)sender).ClassId;
            if (Int32.TryParse(id, out number))
            {
                await Navigation.PushAsync(new StallPage(number, stalls));
            }
        }
    }
}