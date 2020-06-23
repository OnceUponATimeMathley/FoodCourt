using System.Globalization;

namespace FoodCourt.Strings
{
    internal class Localization
    {
        public static string CurrentLanguage { get; set; } = "vi";

        public static string GetString(string resourceName, string defaultValue = null)
        {
            var value = FoodCourt.Strings.Resources.ResourceManager
                .GetString(resourceName, new CultureInfo(CurrentLanguage));

            return !string.IsNullOrEmpty(value) ? value : defaultValue;
        }
    }
}
