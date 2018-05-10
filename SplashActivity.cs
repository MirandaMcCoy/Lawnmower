﻿using System;
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
using Firebase;
using Firebase.Xamarin.Auth;
using Firebase.Auth;
using Firebase.Iid;
using Firebase.Xamarin.Database.Query;

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
            FirebaseApp.InitializeApp(this);

            StartApplication();

            Task startup = new Task(() => { Task.Delay(2000); });
            startup.Start();
        }

        private async void StartApplication()
        {
            Firebase.Auth.FirebaseAuth.Instance.AuthState += async (sender, e) =>
            {
                var user = e?.Auth?.CurrentUser;

                if (user != null)
                {
                    //user is signed in
                    var adminTask = await Shared.CheckIfAdmin();

                    var users = await Shared.firebaseClient.Child(Shared.fbUser).OnceAsync<Objects.User>();

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users.ElementAt(i).Object.Uid == Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid)
                        {
                            users.ElementAt(i).Object.Token = FirebaseInstanceId.Instance.Token;
                            await Shared.firebaseClient.Child(Shared.fbUser).Child(users.ElementAt(i).Key).Child("Token").PutAsync<string>(FirebaseInstanceId.Instance.Token);
                        }
                    }

                    if (Shared.showAdmin)
                    {
                        StartActivity(new Intent(Application.Context, typeof(JobListActivityAdmin)));
                    } else
                    {
                        StartActivity(new Intent(Application.Context, typeof(JobListActivity)));
                    }
                }
                else
                {
                    //user is signed out
                    StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
                }
            };
        }
    }
}