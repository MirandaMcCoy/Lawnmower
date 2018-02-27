using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Lawnmower.Holders;

namespace Lawnmower
{
    [Activity(Label = "Lawnmower", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private LoginViewHolder holder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (holder == null)
            {
                holder = new LoginViewHolder();
            }

            holder.testText = FindViewById<TextView>(Resource.Id.testText);

            holder.testText.Click += TestClick;
        }

        private void TestClick(object sender, EventArgs e)
        {
            using (Toast alert = Toast.MakeText(this, "Click!", ToastLength.Long))
            {
                alert.Show();
            }
        }
    }
}

