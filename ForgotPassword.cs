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
using Lawnmower.Objects;
using Lawnmower.ViewHolders;
using Firebase.Xamarin.Database.Query;
using Firebase.Auth;

namespace Lawnmower
{
    public class ForgotPassword : Fragment
    {
        View view;
        ForgotPasswordViewHolder holder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.ForgotPasswordFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new ForgotPasswordViewHolder();
            }

            SetHolderViews();

            holder.EmailEdit.Text = String.Empty;

            AssignClickEvents();

            return view;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            UnassignClickEvents();
        }

        private void SetHolderViews()
        {
            holder.EmailEdit = view.FindViewById<EditText>(Resource.Id.EmailEdit);
            holder.SendButton = view.FindViewById<TextView>(Resource.Id.SendButton);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.SendButton.Click += SendClick;
        }

        private void UnassignClickEvents()
        {
            holder.SendButton.Click -= SendClick;
        }

        private async void SendClick(object sender, EventArgs e)
        {
            if (holder.EmailEdit.Text != String.Empty)
            {
                FirebaseAuth.Instance.SendPasswordResetEmail(holder.EmailEdit.Text);

                FragmentManager.BeginTransaction().Hide(this).Commit();

                Toast.MakeText(this.Context, "Email sent!", ToastLength.Long).Show();
            } else
            {
                Toast.MakeText(this.Context, "Please enter your email.", ToastLength.Short).Show();
            }
        }
        #endregion
    }
}