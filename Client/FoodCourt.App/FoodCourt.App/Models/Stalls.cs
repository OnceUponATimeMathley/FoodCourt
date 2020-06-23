using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCourt.Models
{
    public class Stalls
    {
        public IEnumerable<Stall> ListStalls;
        public static IEnumerable<Stall> Get()
        {
            return new List<Stall>
            {
                new Stall() {Id=1, Name="Fast Food", PhotoUrl="stall1.jpg", Description="Sell fastfood", Rating=4f},
                new Stall() {Id=2, Name="Flavorsome", PhotoUrl="stall2.jpg", Description="Flavorsome indicates good tasting, full of flavor, specifically pleasant flavor", Rating=4.2f},
                new Stall() {Id=3, Name="Fruity", PhotoUrl="stall3.jpg", Description="Fruity food will be having a taste, smell or flavor of fruit", Rating=3.9f},
                new Stall() {Id=4, Name="Gamy Refers", PhotoUrl="stall4.jpg", Description="Gamy refers to the flavor or strong odor of game", Rating=4.7f},
                new Stall() {Id=5, Name="Gustatory", PhotoUrl="stall3.jpg", Description="Gustatory, relating to the sense of taste", Rating=3f},
                new Stall() {Id=6, Name="Harsh", PhotoUrl="stall1.jpg", Description="Harsh, unpleasant to the taste, abrasive, coarse", Rating=4f},
                new Stall() {Id=7, Name="Heavenly", PhotoUrl="stall2.jpg", Description="Heavenly, considered divine, wonderful, blissful", Rating=2.4f},
                new Stall() {Id=8, Name="Honey", PhotoUrl="stall3.jpg", Description="Honey, honeyed and let us say sweet, sugar, sweetened", Rating=4.2f}
            };
        }
        public static Stall FindStallById(int id, Stalls stalls)
        {
            foreach (Stall stall in stalls.ListStalls)
            {
                if (stall.Id == id) return stall;
            }
            return null;
        }
    }
}
