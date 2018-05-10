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

namespace Lawnmower
{
    public class MenuFragment : Fragment
    {
        View view;
        MenuViewHolder holder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.MenuFragment, container, false);

            if (holder == null)
            {
                holder = new MenuViewHolder();
            }

            FragmentManager.BeginTransaction().Hide(this).Commit();

            SetHolderViews();

            AssignClickEvents();

            return view;
        }

        public override void OnDestroy()
        {
            UnassignClickEvents();

            base.OnDestroy();
        }

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);
        }

        private void SetHolderViews()
        {
            holder.SignOutText = view.FindViewById<TextView>(Resource.Id.SignOutText);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.SignOutText.Click += SignOutClick;
        }

        private void UnassignClickEvents()
        {
            holder.SignOutText.Click -= SignOutClick;
        }

        private void SignOutClick(object sender, EventArgs e)
        {
            Firebase.Auth.FirebaseAuth.Instance.SignOut();
            StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
        }

        #endregion
    }
}