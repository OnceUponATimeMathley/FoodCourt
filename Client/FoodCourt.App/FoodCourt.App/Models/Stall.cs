using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCourt.Models
{
    public class Stall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public string Location { get; set; }
        public IEnumerable<FoodItem> FoodList
        {
            get
            {
                return new List<FoodItem>
                {
                    new FoodItem() {Name="Food placeholder 1", PhotoUrl="food1.jpg", Price=12000f},
                    new FoodItem() {Name="Food placeholder 2", PhotoUrl="food2.jpg", Price=292000f},
                    new FoodItem() {Name="Food placeholder 3", PhotoUrl="food3.jpg", Price=22000f},
                    new FoodItem() {Name="Food placeholder 4", PhotoUrl="food4.jpg", Price=32000f},
                    new FoodItem() {Name="Food placeholder 1", PhotoUrl="food5.jpg", Price=45000f},
                    new FoodItem() {Name="Food placeholder 2", PhotoUrl="food1.jpg", Price=45000f},
                    new FoodItem() {Name="Food placeholder 3", PhotoUrl="food2.jpg", Price=5000f},
                    new FoodItem() {Name="Food placeholder 4", PhotoUrl="food3.jpg", Price=4000f}
                };
            }
        }
    }
}
