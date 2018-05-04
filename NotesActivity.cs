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
using Firebase.Xamarin.Database.Query;
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

            if (holder == null)
            {
                holder = new NotesViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            FragmentManager.BeginTransaction().Hide(this).Commit();

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

            try
            {
                holder.NotesEdit.Text = Shared.jobList[Shared.selectedJob].Notes;
            } catch (Exception ex)
            {
                // Will crash during first loading because Shared.selectedJob hasn't been set yet
            }
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

        private async void NotesChanged(object sender, EventArgs e)
        {
            var notes = (EditText)sender;

            Shared.jobList[Shared.selectedJob].Notes = notes.Text;
            await Shared.firebaseClient.Child("jobs").Child(Shared.jobList[Shared.selectedJob].Id).Child("Notes").PutAsync(notes.Text);
        }

        private void CloseClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Hide(this).Commit();
        }

        #endregion
    }
}