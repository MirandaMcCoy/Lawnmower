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
    [Activity(Label = "LoginActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
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
            
        }

        #region Click Events
        private void AssignClickEvents()
        {
            
        }

        private void UnassignClickEvents()
        {
            
        }
#endregion
    }
}