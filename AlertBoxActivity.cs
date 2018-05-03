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
    public class AlertBoxActivity : Fragment
    {
        View view;
        AlertBoxViewHolder holder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.AlertBoxFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new AlertBoxViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            return view;
        }

        public override void OnDestroyView()
        {
            UnassignClickEvents();

            base.OnDestroyView();
        }

        private void SetHolderViews()
        {
            holder.CloseButton = view.FindViewById<ImageView>(Resource.Id.CloseButton);
            holder.AlertText = view.FindViewById<TextView>(Resource.Id.AlertText);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.CloseButton.Click += ExitClick;
        }

        private void UnassignClickEvents()
        {
            holder.CloseButton.Click -= ExitClick;
        }

        private void ExitClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Hide(this).Commit();
        }

        #endregion

        public void SetAlert(string alert)
        {
            holder.AlertText.Text = alert;
        }
    }
}