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
using Android.Support.V4.Content;
using Firebase.Xamarin.Auth;
using Firebase.Auth;

namespace Lawnmower
{
    [Activity(Label = "@string/app_name", RoundIcon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class LoginActivity : Activity
    {
        private LoginViewHolder holder;
        private List<Objects.User> list_users = new List<Objects.User>();
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
            holder.NewUserText = FindViewById<TextView>(Resource.Id.NewUserText);
            holder.AlertBox = FragmentManager.FindFragmentById<AlertBoxActivity>(Resource.Id.AlertBoxFragment);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.LoginButton.Click += LoginClick;
            holder.NewUserText.Click += NewUserClick;
        }

        private void UnassignClickEvents()
        {
            holder.LoginButton.Click -= LoginClick;
            holder.NewUserText.Click -= NewUserClick;
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(holder.UsernameEdit.Text, holder.PasswordEdit.Text);
                Shared.CheckIfAdmin();
                
                StartActivity(typeof(JobListActivity));

                Finish();
            }
            catch (Exception ex)
            {
                // Sign-in failed, display a message to the user
                // If sign in succeeds, the AuthState event handler will
                //  be notified and logic to handle the signed in user can happen there
                Toast.MakeText(this, "Sign In failed", ToastLength.Short).Show();
            }
            //CreateUser();
            
        }

        private void NewUserClick(object sender, EventArgs e)
        {
            StartActivity(typeof(NewUserActivity));
        }
        
#endregion
        
    }

}