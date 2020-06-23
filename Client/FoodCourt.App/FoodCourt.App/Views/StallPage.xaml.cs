using FoodCourt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StallPage : ContentPage
    {
        Stall stall;
        public static string Name;
        public static string Rating;
        public static string Description;
        public static string PhotoUrl;
        List<FoodItem> FoodList;
        public StallPage(int id, Stalls stalls)
        {
            stall = Stalls.FindStallById(id, stalls);
            if (stall != null)
            {
                FoodList = stall.FoodList.ToList();
                Name = stall.Name;
                Rating = "Rating: " + stall.Rating.ToString() + "/5";
                Description = stall.Description;
                PhotoUrl = stall.PhotoUrl;
            }
            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionViewListHorizontal.ItemsSource = FoodList;
        }
    }
}