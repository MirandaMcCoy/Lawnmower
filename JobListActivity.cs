﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;
using Lawnmower.Adapters;
using System;

namespace Lawnmower
{
    [Activity(Label = "Lawnmower", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivity : Activity
    {
        Job[] jobs;

        ListViewHolder holder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.JobList);

            if (holder == null)
            {
                holder = new ListViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            CreateJobs();

            SetViewAdapter();
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.AddJobImage.Click += AddJobClick;
        }

        private void UnassignClickEvents()
        {

        }

        private void AddJobClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Show(holder.AddJobFragment).Commit();
        }

        #endregion

        private void CreateJobs()
        {
            jobs = new Job[3];
            jobs[0] = new Job();

            jobs[0].FirstName = "Bob";
            jobs[0].LastName = "by";
            jobs[0].Address = "1505 W Mathew Ct, Ozark, MO 65721";
            jobs[0].ContactNumber = "555-555-5555";
            jobs[0].JobType = "Mow and Weedeat";
            jobs[0].Date = new DateTime(2018, 4, 23);
            jobs[0].Repeating = true;
            jobs[0].Notes = "A a aa";

            jobs[1] = new Job();

            jobs[1].FirstName = "Angie";
            jobs[1].LastName = "Fish";
            jobs[1].Address = "1111 Street St, Ozark, MO 65721";
            jobs[1].ContactNumber = "555-555-5555";
            jobs[1].JobType = "Mow and Weedeat";
            jobs[1].Date = new DateTime(2018, 4, 23);
            jobs[1].Repeating = true;
            jobs[1].Notes = "B b bb";

            jobs[2] = new Job();

            jobs[2].FirstName = "Raymond";
            jobs[2].LastName = "Noodles";
            jobs[2].Address = "2222 Street St, Ozark, MO 65721";
            jobs[2].ContactNumber = "555-555-5555";
            jobs[2].JobType = "Mow and Weedeat";
            jobs[2].Date = new DateTime(2018, 4, 23);
            jobs[2].Repeating = true;
            jobs[2].Notes = "C c cc";
        }

        private void SetHolderViews()
        {
            holder.JobListView = FindViewById<ListView>(Resource.Id.JobList);
            holder.AddJobImage = FindViewById<ImageView>(Resource.Id.AddJobButton);
            holder.AddJobFragment = FragmentManager.FindFragmentById<AddJobActivity>(Resource.Id.AddJobMenu);
            holder.AssignJobFragment = FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu);

        }

            private void SetViewAdapter()
        {
            JobListAdapter adapter = new JobListAdapter(this, jobs);

            holder.JobListView.Adapter = adapter;
        }

        public override void OnBackPressed()
        {
            if (holder.AddJobFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.AddJobFragment).Commit();
            } else if (holder.AssignJobFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.AssignJobFragment).Commit();
            } else
            {
                base.OnBackPressed();
            }
        }
    }
}

