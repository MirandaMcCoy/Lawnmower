using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Support.V7.App;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;
using Lawnmower.Adapters;
using System;
using System.Threading.Tasks;
using Android.Views;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Collections.Generic;
using Firebase.Auth;

namespace Lawnmower
{
    [Activity(Label = "@string/app_name", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivity : Activity
    {
        ListViewHolder holder;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.JobList);

            if (holder == null)
            {
                holder = new ListViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            Shared.GetJobsAsync(this);

            if (Shared.jobList.Count > 0)
            {
                SetViewAdapter();

                Shared.jobListAdapter.NotifyDataSetChanged();
            }
        }


        private void SetHolderViews()
        {
            holder.JobListView = FindViewById<ListView>(Resource.Id.JobList);
            holder.NotesFragment = FragmentManager.FindFragmentById<NotesActivity>(Resource.Id.NotesMenu);

            if (Shared.showAdmin) // Only need to set this view if the user is an admin
            {
                holder.AddJobImage = FindViewById<ImageView>(Resource.Id.AddJobButton);
                holder.AddJobFragment = FragmentManager.FindFragmentById<AddJobActivity>(Resource.Id.AddJobMenu);
                holder.AssignJobFragment = FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu);
            }
        }

        public void SetViewAdapter()
        {
            Shared.jobListAdapter = new JobListAdapter(this, Shared.jobList.ToArray());

            holder.JobListView.Adapter = Shared.jobListAdapter;
        }

        public override void OnBackPressed()
        {

            if (holder.NotesFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.NotesFragment).Commit();
            }
            else
            {
                base.OnBackPressed();
            }
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

