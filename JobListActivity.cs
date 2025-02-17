﻿using Android.App;
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
using Android.Support.V4.Widget;

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
            holder.MenuImage = FindViewById<ImageView>(Resource.Id.MenuBar);
            holder.MenuFragment = FragmentManager.FindFragmentById<MenuFragment>(Resource.Id.MenuMenu);
            holder.AlertBox = FragmentManager.FindFragmentById<AlertBoxActivity>(Resource.Id.AlertBoxFragment);
            holder.SwipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.SwipeRefresh);
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
            else if (holder.MenuFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.MenuFragment).Commit();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.MenuImage.Click += OpenMenu;
            holder.SwipeRefresh.Refresh += RefreshJobList;
        }

        private void UnassignClickEvents()
        {
            holder.MenuImage.Click -= OpenMenu;
            holder.SwipeRefresh.Refresh -= RefreshJobList;
        }

        private void OpenMenu(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Show(holder.MenuFragment).Commit();
        }

        private void RefreshJobList(object sender, EventArgs e)
        {
            var swipe = (SwipeRefreshLayout)sender;

            Shared.GetJobsAsync(this);

            swipe.Refreshing = false;
        }

        #endregion

        public void ShowAlert(string alert)
        {
            holder.AlertBox.SetAlert(alert);
            FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();
        }
    }
}

