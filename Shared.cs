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
using Firebase.Xamarin.Database;
using Lawnmower.Objects;

namespace Lawnmower
{
    static class Shared
    {
        public static string FirebaseURL = "https://lawnmower-a4296.firebaseio.com/";
        public static FirebaseClient FirebaseClient = new FirebaseClient(FirebaseURL);
        public static Job[] jobList;
    }
}