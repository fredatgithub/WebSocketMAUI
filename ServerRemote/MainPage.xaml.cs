using Microsoft.Maui.Controls;
using ServerRemote.Platforms.Android;
using ServerRemote.Platforms.Android.Services;

namespace ServerRemote;

public partial class MainPage : ContentPage
{
    private Server server;
    int count = 0;
    public MainPage()
    {
		InitializeComponent();
        server = new Server(this);
        server.Start(1024);

    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
        //var volumeControl = DependencyService.Get<IVolumeControl>();
        //volumeControl?.IncreaseVolume();

        //server = new Server(this);
        //server.Start(1024);
        //      count++;

        //if (count == 1)
        //	CounterBtn.Text = $"Clicked {count} time";
        //else
        //	CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

    public void Up()
    {
        // Execute UI-related operations on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var volumeControl = DependencyService.Get<IVolumeControl>();
            volumeControl?.IncreaseVolume();
        });
      
    }

    public void Down()
    {
        // Execute UI-related operations on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var volumeControl = DependencyService.Get<IVolumeControl>();
            volumeControl?.DecreaseVolume();
        });
      
    }
}

