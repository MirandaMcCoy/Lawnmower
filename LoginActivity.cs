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
            holder.UsernameEdit = FindViewById<EditText>(Resource.Id.UsernameEdit);
            holder.PasswordEdit = FindViewById<EditText>(Resource.Id.PasswordEdit);
            holder.LoginButton = FindViewById<Button>(Resource.Id.LoginButton);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.LoginButton.Click += LoginClick;
        }

        private void UnassignClickEvents()
        {
            holder.LoginButton.Click -= LoginClick;
        }

        private void LoginClick(object sender, EventArgs e)
        {
            // Only worry about passing the username and password data and returning
            //     if the user is a valid employee or not.
            //     Job List will handle what to show/not to show to different employees

            // To get the entered username, use:
            // holder.UsernameEdit.Text

            // To get the entered password, use:
            // holder.PasswordEdit.Text
            
            // Open Up Job List if valid employee (Currently open regardless for testing)
            StartActivity(typeof(JobListActivity));
        }
#endregion
    }
}