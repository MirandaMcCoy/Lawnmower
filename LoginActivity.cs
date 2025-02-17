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
            holder.ForgotPasswordText = FindViewById<TextView>(Resource.Id.ForgotPasswordText);
            holder.ForgotPasswordFragment = FragmentManager.FindFragmentById<ForgotPassword>(Resource.Id.ForgotPasswordFragment);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.LoginButton.Click += LoginClick;
            holder.NewUserText.Click += NewUserClick;
            holder.ForgotPasswordText.Click += ForgotPasswordClick;
        }

        private void UnassignClickEvents()
        {
            holder.LoginButton.Click -= LoginClick;
            holder.NewUserText.Click -= NewUserClick;
            holder.ForgotPasswordText.Click -= ForgotPasswordClick;
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            ProgressDialog dialog = new ProgressDialog(this);
            dialog.SetMessage("Signing in...");
            dialog.Indeterminate = true;
            dialog.SetCancelable(false);
            dialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            try
            {
                dialog.Show();

                UnassignClickEvents(); // So users can't spam the login button

                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(holder.UsernameEdit.Text, holder.PasswordEdit.Text);
                await Shared.CheckIfAdmin();

                if (Shared.showAdmin)
                {
                    StartActivity(new Intent(Application.Context, typeof(JobListActivityAdmin)));
                }
                else
                {
                    StartActivity(new Intent(Application.Context, typeof(JobListActivity)));
                }

                Finish();
            }
            catch (Exception ex)
            {
                // Sign-in failed, display a message to the user
                // If sign in succeeds, the AuthState event handler will
                //  be notified and logic to handle the signed in user can happen there
                holder.AlertBox.SetAlert(Resources.GetString(Resource.String.sign_in_failed));
                FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();

                AssignClickEvents();
            }
            finally
            {
                dialog.Hide();
            }   
        }

        private void NewUserClick(object sender, EventArgs e)
        {
            StartActivity(typeof(NewUserActivity));
        }

        private void ForgotPasswordClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Show(holder.ForgotPasswordFragment).Commit();
        }
        
#endregion
        
    }

}