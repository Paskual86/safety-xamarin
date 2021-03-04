using Android.Content;
using SafetyBP.Controls;
using SafetyBP.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImagePasswordEntry), typeof(ImagePasswordEntryRenderer))]
namespace SafetyBP.Droid
{
    public class ImagePasswordEntryRenderer : EntryRenderer
    {
        public ImagePasswordEntryRenderer(Context context) : base(context)
        {

        }

    }
}