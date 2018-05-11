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

namespace Lawnmower
{
    [Activity(Label = "@string/app_name", WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class JobListActivityAdmin : Activity
    {
        ListViewHolder holder;
        
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.JobListAdmin);

            if (holder == null)
            {
                holder = new ListViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            var employeeTask = await Shared.GetEmployeesAsync(this);

            Shared.GetJobsAsync(this);

            SetViewAdapter();

            Shared.jobListAdapterAdmin.NotifyDataSetChanged();
        }

       
        #region Click Events

        private void AssignClickEvents()
        {
            holder.AddJobImage.Click += AddJobClick;
            holder.MenuImage.Click += OpenMenu;
        }

        private void UnassignClickEvents()
        {
            holder.AddJobImage.Click -= AddJobClick;
            holder.MenuImage.Click -= OpenMenu;
        }

        private void AddJobClick(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Show(holder.AddJobFragment).Commit();
        }

        private void OpenMenu(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Show(holder.MenuFragment).Commit();
        }

        #endregion

        private void SetHolderViews()
        {
            holder.JobListView = FindViewById<ListView>(Resource.Id.JobList);
            holder.AddJobImage = FindViewById<ImageView>(Resource.Id.AddJobButton);
            holder.AddJobFragment = FragmentManager.FindFragmentById<AddJobActivity>(Resource.Id.AddJobMenu);
            holder.AssignJobFragment = FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu);
            holder.EditJobFragment = FragmentManager.FindFragmentById<EditJobActivity>(Resource.Id.EditJobMenu);
            holder.MenuImage = FindViewById<ImageView>(Resource.Id.MenuBar);
            holder.MenuFragment = FragmentManager.FindFragmentById<MenuFragment>(Resource.Id.MenuMenu);
        }

        public void SetViewAdapter()
        {
            Shared.jobListAdapterAdmin = new JobListAdapterAdmin(this, Shared.jobList.ToArray());

            holder.JobListView.Adapter = Shared.jobListAdapterAdmin;
        }

        public override void OnBackPressed()
        {
            if (holder.AddJobFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.AddJobFragment).Commit();
            }
            else if (holder.AssignJobFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.AssignJobFragment).Commit();
            }
            else if (holder.EditJobFragment.IsVisible)
            {
                FragmentManager.BeginTransaction().Hide(holder.EditJobFragment).Commit();
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

        public void ShowAlert(string alert)
        {
            holder.AlertBox.SetAlert(alert);
            FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();
        }
    }
}

