using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using ServerRemote;
using ServerRemote.Platforms.Android;
using ServerRemote.Platforms.Android.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.Res.Resources;

[assembly: Dependency(typeof(Test))]
namespace ServerRemote;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class Test : MauiAppCompatActivity, IVolumeControl
{
    private AudioManager _audioManager;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if(Singleton1.Instance._audioManager == null)
        {
            // Retrieve AudioManager service
            Singleton1.Instance._audioManager = (AudioManager)GetSystemService(Context.AudioService);
            Singleton1.Instance._audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
        }
       
    }

    public Test()
    {
        //_audioManager = (AudioManager)GetSystemService(Context.AudioService);
        //_audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
    }
    public void DecreaseVolume()
    {
        throw new NotImplementedException();
    }

    public void IncreaseVolume()
    {
        try
        {
            if (Singleton1.Instance._audioManager != null)
            {
                    _audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
            }
            else
            {
                Singleton1.Instance._audioManager = (AudioManager)GetSystemService(Context.AudioService);
                Singleton1.Instance._audioManager.AdjustStreamVolume(Android.Media.Stream.Music, Adjust.Raise, VolumeNotificationFlags.ShowUi);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}
