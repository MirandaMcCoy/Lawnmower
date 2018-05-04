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
    public class NotesActivity : Fragment
    {
        View view;
        NotesViewHolder holder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.NotesFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new NotesViewHolder();
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

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);

            //holder.NotesEdit.Text = Shared.jobList[Shared.selectedJob].Notes;
        }

        private void SetHolderViews()
        {
            holder.NotesEdit = view.FindViewById<EditText>(Resource.Id.NotesEdit);
            holder.CloseButton = view.FindViewById<TextView>(Resource.Id.CloseButton);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.NotesEdit.TextChanged += NotesChanged;
            holder.CloseButton.Click += CloseClick;
        }

        private void UnassignClickEvents()
        {
            holder.NotesEdit.TextChanged -= NotesChanged;
            holder.CloseButton.Click -= CloseClick;
        }

        private void NotesChanged(object sender, EventArgs e)
        {
            var notes = (EditText)sender;
            Shared.jobList[Shared.selectedJob].Notes = notes.Text;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Hide(this).Commit();
        }

        #endregion
    }
}