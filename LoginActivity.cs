using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lawnmower.ViewHolders;
using Android.Gms.Common;

namespace Lawnmower
{
    [Activity(Label = "LoginActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private LoginViewHolder holder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            if (holder == null)
            {
                holder = new LoginViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();
        }

        protected override void OnDestroy()
        {
            UnassignClickEvents();

            base.OnDestroy();
        }

        private void SetHolderViews()
        {
            holder.mainButton = FindViewById<Button>(Resource.Id.MainButton);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.mainButton.Click += MainClick;
        }

        private void UnassignClickEvents()
        {
            holder.mainButton.Click -= MainClick;
        }

        private void MainClick(object sender, EventArgs e)
        {
            StartActivity(typeof(JobListActivity));
        }
#endregion
    }
}