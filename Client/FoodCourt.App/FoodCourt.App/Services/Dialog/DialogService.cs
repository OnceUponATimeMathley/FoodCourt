using System.Threading.Tasks;

namespace FoodCourt.Services.Dialog
{
    internal class DialogService : IDialogService
    {
        public Task AlertAsync(string message, string title)
        {
            return AlertAsync(message, title, "Ok");
        }

        public Task AlertAsync(string message, string title, string okText)
        {
            return Acr.UserDialogs.UserDialogs.Instance.AlertAsync(message, title, okText);
        }
    }
}
