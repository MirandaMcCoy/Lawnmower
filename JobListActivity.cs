using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Gms.Common;
using Lawnmower;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace Lawnmower
{
    [Activity(Label = "Lawnmower", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.JobList);

            msgText = FindViewById<TextView>(Resource.Id.msgText);
            IsPlayServicesAvailable();

        }

        /// <testGooglePlay>
        /// testting abilities to connect to google play services 
        /// for firebase connection
        /// </testGooglePlay>
        TextView msgText;
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                
                return true;
            }
        }
    }
}

