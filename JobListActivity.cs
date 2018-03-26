using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace Lawnmower
{
    [Activity(Label = "Lawnmower", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.JobList);
        }
    }
}

