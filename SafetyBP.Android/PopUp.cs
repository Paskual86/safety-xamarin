using SafetyBP.Droid;
using SafetyBP.Interfaces;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(PopUp))]
namespace SafetyBP.Droid
{
    public class PopUp : IPopUp
    {
        public async Task Show(string message)
        {
            //await DisplayAlert("Alert", "You have been alerted", "OK");
        }
    }
}