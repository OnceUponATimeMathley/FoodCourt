using System.Threading.Tasks;

namespace FoodCourt.Services.Dialog
{
    public interface IDialogService
    {
        Task AlertAsync(string message, string title);
        Task AlertAsync(string message, string title, string okText);
    }
}