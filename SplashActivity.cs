using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lawnmower
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startup = new Task(() => { SimulateStartup(); });
            startup.Start();
        }

        async void SimulateStartup()
        {
            // Simulate waiting time so splash screen does not look awkward
            await Task.Delay(1000);

            // Start login activity
            StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
        }
    }
}