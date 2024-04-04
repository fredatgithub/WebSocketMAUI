using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using ServerRemote.Platforms.Android;

namespace ServerRemote;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
//[assembly: Microsoft.Maui.Controls.Dependency(typeof(VolumeControlApp.Droid.VolumeControlImplementation))]
//[assembly: Dependency(typeof(VolumeControlImplementation))]
public class MainActivity : MauiAppCompatActivity, IVolumeControl
{
    //private AudioManager _audioManager;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Singleton1.Instance._audioManager == null)
        {
            // Retrieve AudioManager service
            Singleton1.Instance._audioManager = (AudioManager)GetSystemService(Context.AudioService);
            //Singleton1.Instance._audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
        }
    }

    public void DecreaseVolume()
    {
        try
        {
            // Execute UI-related operations on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Singleton1.Instance._audioManager != null)
                {
                    Singleton1.Instance._audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Lower, VolumeNotificationFlags.ShowUi);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve AudioManager service.");
                }
            });
        }
        catch (Exception)
        {
        }
    }

    public void IncreaseVolume()
    {
        try
        {
            // Execute UI-related operations on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Singleton1.Instance._audioManager != null)
                {
                    Singleton1.Instance._audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
                }
            });

           
        }
        catch (Exception ex)
        {
        }
    }
}


