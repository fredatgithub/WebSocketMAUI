using ServerRemote.Platforms.Android.Services;
using Android.App;
using Android.Content;
using Microsoft.Maui;
using Android.Content.PM;
using Android.OS;
using ServerRemote.Platforms.Android;
using Android.Media;


[assembly: Dependency(typeof(VolumeControlImplementation))]
namespace ServerRemote.Platforms.Android.Services
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class VolumeControlImplementation : MauiAppCompatActivity, IVolumeControl
    {
        private AudioManager _audioManager;

        public VolumeControlImplementation()
        {
            _audioManager = (AudioManager)GetSystemService(Context.AudioService);
            //_audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
        }
        public void DecreaseVolume()
        {
            throw new NotImplementedException();
        }

        public void IncreaseVolume()
        {
            throw new NotImplementedException();
        }
    }
}
