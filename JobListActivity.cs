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

            CreateDummyJobs();

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

        private void CreateDummyJobs()
        {
            Shared.dummyJobList.AddRange(new Job[3]);
            Shared.dummyJobList[0] = new Job();

            Shared.dummyJobList[0].FirstName = "Bob";
            Shared.dummyJobList[0].LastName = "by";
            Shared.dummyJobList[0].Address = "1505 W Mathew Ct, Ozark, MO 65721";
            Shared.dummyJobList[0].ContactNumber = "555-555-5555";
            Shared.dummyJobList[0].JobType = "Mow and Weedeat";
            Shared.dummyJobList[0].Date = new DateTime(2018, 4, 23);
            Shared.dummyJobList[0].Repeating = true;
            Shared.dummyJobList[0].Notes = "A a aa";

            Shared.dummyJobList[1] = new Job();

            Shared.dummyJobList[1].FirstName = "Angie";
            Shared.dummyJobList[1].LastName = "Fish";
            Shared.dummyJobList[1].Address = "1111 Street St, Ozark, MO 65721";
            Shared.dummyJobList[1].ContactNumber = "555-555-5555";
            Shared.dummyJobList[1].JobType = "Mow and Weedeat";
            Shared.dummyJobList[1].Date = new DateTime(2018, 4, 23);
            Shared.dummyJobList[1].Repeating = true;
            Shared.dummyJobList[1].Notes = "B b bb";

            Shared.dummyJobList[2] = new Job();

            Shared.dummyJobList[2].FirstName = "Raymond";
            Shared.dummyJobList[2].LastName = "Noodles";
            Shared.dummyJobList[2].Address = "2222 Street St, Ozark, MO 65721";
            Shared.dummyJobList[2].ContactNumber = "555-555-5555";
            Shared.dummyJobList[2].JobType = "Mow and Weedeat";
            Shared.dummyJobList[2].Date = new DateTime(2018, 4, 23);
            Shared.dummyJobList[2].Repeating = true;
            Shared.dummyJobList[2].Notes = "C c cc";
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
            Shared.jobListAdapter = new JobListAdapter(this, Shared.dummyJobList.ToArray());

            holder.JobListView.Adapter = Shared.jobListAdapter;
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

