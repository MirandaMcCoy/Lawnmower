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
using Firebase.Firestore;
using Firebase.Xamarin.Database;
using Lawnmower.Objects;

namespace Lawnmower
{
    static class Shared
    {
        public static string FirebaseURL = "https://lawnmower-a4296.firebaseio.com/";
        public static FirebaseClient FirebaseClient = new FirebaseClient(FirebaseURL);
        public static FirebaseFirestore fs = FirebaseFirestore.Instance;
        public static Job[] jobList;

        public static void testDB()
        {
            JavaDictionary<string, object> newUser = new JavaDictionary<string, object>();

            newUser.Add("userName", "NathanDunn");
            newUser.Add("passWord", "password");

            fs.Collection("Users").Document("0").Set(newUser);
        }
    }
}