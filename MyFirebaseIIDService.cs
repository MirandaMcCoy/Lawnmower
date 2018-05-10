using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Iid;
using Firebase.Xamarin.Database.Query;

namespace Lawnmower
{
    [Service]
    [IntentFilter (new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public async override void OnTokenRefresh()
        {
            try
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;

                var users = await Shared.firebaseClient.Child(Shared.fbUser).OnceAsync<Objects.User>();

                for (int i = 0; i < users.Count; i++)
                {
                    if (users.ElementAt(i).Object.Uid == FirebaseAuth.Instance.CurrentUser.Uid)
                    {
                        users.ElementAt(i).Object.Token = refreshedToken;
                        await Shared.firebaseClient.Child(Shared.fbUser).Child(users.ElementAt(i).Key).Child("Token").PutAsync<string>(refreshedToken);
                    }
                }
            } catch (Exception ex)
            {
                var t = "failed";
            }
        }
    }
}