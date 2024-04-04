using Android.App;
using Android.Runtime;
using ServerRemote.Platforms.Android;
using ServerRemote.Platforms.Android.Services;

namespace ServerRemote;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
        DependencyService.Register<IVolumeControl, MainActivity>();
    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
