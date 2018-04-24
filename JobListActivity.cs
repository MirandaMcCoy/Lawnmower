using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;
using Lawnmower.Adapters;
using System;
using Android.Views;

namespace Lawnmower
{
    [Activity(Label = "Lawnmower", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivity : Activity
    {
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
            Shared.jobList = new Job[3];
            Shared.jobList[0] = new Job();

            Shared.jobList[0].FirstName = "Bob";
            Shared.jobList[0].LastName = "by";
            Shared.jobList[0].Address = "1505 W Mathew Ct, Ozark, MO 65721";
            Shared.jobList[0].ContactNumber = "555-555-5555";
            Shared.jobList[0].JobType = "Mow and Weedeat";
            Shared.jobList[0].Date = new DateTime(2018, 4, 23);
            Shared.jobList[0].Repeating = true;
            Shared.jobList[0].Notes = "A a aa";

            Shared.jobList[1] = new Job();

            Shared.jobList[1].FirstName = "Angie";
            Shared.jobList[1].LastName = "Fish";
            Shared.jobList[1].Address = "1111 Street St, Ozark, MO 65721";
            Shared.jobList[1].ContactNumber = "555-555-5555";
            Shared.jobList[1].JobType = "Mow and Weedeat";
            Shared.jobList[1].Date = new DateTime(2018, 4, 23);
            Shared.jobList[1].Repeating = true;
            Shared.jobList[1].Notes = "B b bb";

            Shared.jobList[2] = new Job();

            Shared.jobList[2].FirstName = "Raymond";
            Shared.jobList[2].LastName = "Noodles";
            Shared.jobList[2].Address = "2222 Street St, Ozark, MO 65721";
            Shared.jobList[2].ContactNumber = "555-555-5555";
            Shared.jobList[2].JobType = "Mow and Weedeat";
            Shared.jobList[2].Date = new DateTime(2018, 4, 23);
            Shared.jobList[2].Repeating = true;
            Shared.jobList[2].Notes = "C c cc";
        }

        private void SetHolderViews()
        {
            holder.JobListView = FindViewById<ListView>(Resource.Id.JobList);
            holder.AddJobImage = FindViewById<ImageView>(Resource.Id.AddJobButton);
            holder.AddJobFragment = FragmentManager.FindFragmentById<AddJobActivity>(Resource.Id.AddJobMenu);
            holder.AssignJobFragment = FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu);
            Shared.testDB();
        }

            private void SetViewAdapter()
        {
            JobListAdapter adapter = new JobListAdapter(this, Shared.jobList);

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

