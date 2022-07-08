using System;
using System.ComponentModel;

namespace habahabamall.Models
{
    public class HomePageMasterMenuItem
    {
        public HomePageMasterMenuItem()
        {
            TargetType = typeof(HomePageMasterMenuItem);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string TitleIcon { get; set; }
    }

    public enum MenuPage
    {
        [Description("home")] Home = 1,
        [Description("categories")] Categories = 2,
        [Description("about us")] About = 3,
        [Description("contact us")] Contact = 4


    }
}