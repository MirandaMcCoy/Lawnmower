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

using Android.Support.V7.App;
using Lawnmower.Objects;
using Lawnmower.Adapters;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace Lawnmower
{
    [Activity(Label = "NewUserActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class NewUserActivity : Activity
    {
        private NewUserViewHolder holder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewUser);

            if (holder == null)
            {
                holder = new NewUserViewHolder();
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
            holder.FirstNameEdit = FindViewById<EditText>(Resource.Id.FirstNameEdit);
            holder.LastNameEdit = FindViewById<EditText>(Resource.Id.LastNameEdit);
            holder.UsernameEdit = FindViewById<EditText>(Resource.Id.UsernameEdit);
            holder.PasswordEdit = FindViewById<EditText>(Resource.Id.PasswordEdit);
            holder.VerifyPasswordEdit = FindViewById<EditText>(Resource.Id.VerifyPasswordEdit);
            holder.CreateAccountButton = FindViewById<TextView>(Resource.Id.CreateUserButton);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.CreateAccountButton.Click += CreateAccountClick;
        }

        private void UnassignClickEvents()
        {
            holder.CreateAccountButton.Click -= CreateAccountClick;
        }

        private void CreateAccountClick(object sender, EventArgs e)
        {
            // Verify that there is not an account with that username in the db
            // If there is, alert the user
            // Else, check that the two password fields match
            //      If they don't, alert the user
            //      If they do, create user and log them in

            StartActivity(typeof(JobListActivity));

            Finish();
        }
#endregion
    }
}