﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Xamarin.Database.Query;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;

namespace Lawnmower.Adapters
{
    class JobListAdapter : BaseAdapter
    {

        Activity context;
        public List<Job> jobs;
        JobListItemViewHolder holder;
        View view;
        int position;

        public JobListAdapter(Activity context, Job[] jobs)
        {
            this.context = context;
            this.jobs = jobs.ToList();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            view = convertView;

            if (view == null && jobs[position].Id != null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.JobListItem, parent, false);

                holder = new JobListItemViewHolder();

                SetHolderViews();

                AssignClickEvents();

                view.Tag = holder;
            } else
            {
                holder = view.Tag as JobListItemViewHolder;
            }

            holder.NotesImage.Tag = position;
            holder.DirectionsImage.Tag = position;
            holder.FinishImage.Tag = position;

            SetViews(position);
            this.position = position;

            return view;
        }
        
        public override int Count
        {
            get
            {
                return jobs.Count;
            }
        }

        public override void NotifyDataSetChanged()
        {
            jobs = Shared.jobList;

            base.NotifyDataSetChanged();
        }

        private void SetViews(int position)
        {
            var job = jobs[(int)holder.FinishImage.Tag];

            holder.FirstNameText.Text = job.FirstName;
            holder.LastNameText.Text = job.LastName;
            holder.AddressNameText.Text = job.Address;
            holder.ContactText.Text = job.ContactNumber;
            holder.JobDateText.Text = job.Date.Month.ToString() + "/" + job.Date.Day.ToString() + "/" + job.Date.Year.ToString();
            holder.JobDayText.Text = job.Date.DayOfWeek.ToString();
            holder.JobTypeText.Text = job.JobType;
        }

        private void SetHolderViews()
        {
            holder.AddressNameText = view.FindViewById<TextView>(Resource.Id.AddressText);
            holder.FirstNameText = view.FindViewById<TextView>(Resource.Id.FirstNameTextView);
            holder.LastNameText = view.FindViewById<TextView>(Resource.Id.LastNameTextView);
            holder.ContactText = view.FindViewById<TextView>(Resource.Id.ContactText);
            holder.JobDateText = view.FindViewById<TextView>(Resource.Id.JobDateText);
            holder.JobDayText = view.FindViewById<TextView>(Resource.Id.JobDayText);
            holder.JobTypeText = view.FindViewById<TextView>(Resource.Id.JobTypeText);
            holder.DirectionsImage = view.FindViewById<ImageView>(Resource.Id.DirectionsImage);
            holder.FinishImage = view.FindViewById<ImageView>(Resource.Id.FinishImage);
            holder.NotesImage = view.FindViewById<ImageView>(Resource.Id.NotepadImage);
            holder.NotesFragment = this.context.FragmentManager.FindFragmentById<NotesActivity>(Resource.Id.NotesMenu);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.DirectionsImage.Click += DirectionsClick;
            holder.FinishImage.Click += FinishClick;
            holder.NotesImage.Click += NotesClick;
        }

        private void DirectionsClick(object sender, EventArgs e)
        {
            var directionsImage = (ImageView)sender;
            Shared.selectedJob = (int)directionsImage.Tag;

            try
            {
                this.context.StartActivity(new Intent(Intent.ActionView, global::Android.Net.Uri.Parse("google.navigation:q=" + Shared.jobList[Shared.selectedJob].Address)));
            }
            catch
            {
                ((JobListActivity)context).ShowAlert(context.Resources.GetString(Resource.String.no_maps_application));
            }
        }

        private void NotesClick(object sender, EventArgs e)
        {
            var notesImage = (ImageView)sender;

            Shared.selectedJob = (int)notesImage.Tag;
            this.context.FragmentManager.BeginTransaction().Show(holder.NotesFragment).Commit();
        }

        private async void FinishClick(object sender, EventArgs e)
        {
            var finishImage = (ImageView)sender;
            Shared.selectedJob = (int)finishImage.Tag;
            
            await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobInApproval).PutAsync<bool>(true);

            Shared.GetJobsAsync(this.context);
        }

#endregion

    }
}