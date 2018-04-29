﻿using System;
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
    [Activity(Label = "LoginActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private LoginViewHolder holder;
        private List<User> list_users = new List<User>();
        private const string firebaseURL = "https://lawnmower-a4296.firebaseio.com/";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            if (holder == null)
            {
                holder = new LoginViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            await LoadData();
        }

        private async Task LoadData()
        {
            var firebase = new FirebaseClient(firebaseURL);
            var items = await firebase.Child("Users").OnceAsync<User>();
            foreach (var item in items)
            {
                User account = new User();
                account.uid = item.Key;
                account.name = item.Object.name;
                account.email = item.Object.email;
                list_users.Add(account);
            }
        }
        private async void CreateUser()
        {
            User user = new User();
            user.uid = String.Empty;
            user.name = holder.UsernameEdit.Text;
            user.email = holder.PasswordEdit.Text;
            var firebase = new FirebaseClient(firebaseURL);
            //Add Item  
            var item = await firebase.Child("login").PostAsync<User>(user);
            await LoadData();
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
            CreateUser();

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