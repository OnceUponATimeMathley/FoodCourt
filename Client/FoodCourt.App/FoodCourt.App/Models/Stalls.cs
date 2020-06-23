using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCourt.Models
{
    public class Stalls
    {
        public static IEnumerable<Stall> Get()
        {
            return new List<Stall>
            {
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"},
                new Stall() {Name="Fast food", PhotoUrl="stall1.jpg", Description="Sell fastfood"}
            };
        }
    }
}
