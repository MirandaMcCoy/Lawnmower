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
using Firebase.Auth;
using Firebase.Xamarin.Database.Query;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;

namespace Lawnmower.Adapters
{
    class JobListAdapterAdmin : BaseAdapter
    {

        Activity context;
        public List<Job> jobs;
        JobListItemViewHolder holder;
        View view;
        int position;

        public JobListAdapterAdmin(Activity context, Job[] jobs)
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

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.JobListItemAdmin, parent, false);

                holder = new JobListItemViewHolder();

                SetHolderViews();

                AssignClickEvents();

                view.Tag = holder;
            } else
            {
                holder = view.Tag as JobListItemViewHolder;
            }

            holder.AssignText.Tag = position;
            holder.NotesImage.Tag = position;
            holder.DirectionsImage.Tag = position;
            holder.CancelImage.Tag = position;

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
            var job = jobs[(int)holder.AssignText.Tag];

            holder.FirstNameText.Text = job.FirstName;
            holder.LastNameText.Text = job.LastName;
            holder.AddressNameText.Text = job.Address;
            holder.ContactText.Text = job.ContactNumber;
            holder.JobDateText.Text = job.Date.Month.ToString() + "/" + job.Date.Day.ToString() + "/" + job.Date.Year.ToString();
            holder.JobDayText.Text = job.Date.DayOfWeek.ToString();
            holder.JobTypeText.Text = job.JobType;

            if (job.Assignee == "")
            {
                holder.AssignText.Text = "Unassigned";
            } else
            {
                for (int i = 0; i < Shared.employeeList.Count; i++)
                {
                    if (Shared.employeeList[i].Uid == Shared.jobList[Shared.selectedJob].Assignee)
                    {
                        holder.AssignText.Text = Shared.employeeList[i].FirstName + " " + Shared.employeeList[i].LastName;
                    }
                }
            }
        }

        private void SetHolderViews()
        {
            holder.AssignText = view.FindViewById<TextView>(Resource.Id.AssignedText);
            holder.AddressNameText = view.FindViewById<TextView>(Resource.Id.AddressText);
            holder.FirstNameText = view.FindViewById<TextView>(Resource.Id.FirstNameTextView);
            holder.LastNameText = view.FindViewById<TextView>(Resource.Id.LastNameTextView);
            holder.ContactText = view.FindViewById<TextView>(Resource.Id.ContactText);
            holder.JobDateText = view.FindViewById<TextView>(Resource.Id.JobDateText);
            holder.JobDayText = view.FindViewById<TextView>(Resource.Id.JobDayText);
            holder.JobTypeText = view.FindViewById<TextView>(Resource.Id.JobTypeText);
            holder.RepeatingMark = view.FindViewById<TextView>(Resource.Id.RepeatingXText);
            holder.DirectionsImage = view.FindViewById<ImageView>(Resource.Id.DirectionsImage);
            holder.CancelImage = view.FindViewById<ImageView>(Resource.Id.DeleteImage);
            holder.NotesImage = view.FindViewById<ImageView>(Resource.Id.NotepadImage);
            holder.AssignJobFragment = this.context.FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu);
            holder.NotesFragment = this.context.FragmentManager.FindFragmentById<NotesActivity>(Resource.Id.NotesMenu);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.AssignText.Click += AssignJobOpen;
            holder.DirectionsImage.Click += DirectionsClick;
            holder.CancelImage.Click += CancelClick;
            holder.NotesImage.Click += NotesClick;
        }

        private void AssignJobOpen(object sender, EventArgs e)
        {
            var assignText = (TextView)sender;

            Shared.selectedJob = (int)assignText.Tag;
            this.context.FragmentManager.BeginTransaction().Show(holder.AssignJobFragment).Commit();
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
                using (Toast alert = Toast.MakeText(this.context, "No maps application found", ToastLength.Short))
                {
                    alert.Show();
                }

            }
        }

        private void NotesClick(object sender, EventArgs e)
        {
            var notesImage = (ImageView)sender;

            Shared.selectedJob = (int)notesImage.Tag;
            this.context.FragmentManager.BeginTransaction().Show(holder.NotesFragment).Commit();
        }

        private async void CancelClick(object sender, EventArgs e)
        {
            var cancelImage = (ImageView)sender;

            Shared.selectedJob = (int)cancelImage.Tag;

            await Shared.firebaseClient.Child("jobs").Child(Shared.jobList[Shared.selectedJob].Id).DeleteAsync();

            Shared.GetJobsAsync(this.context);
        }

#endregion

    }
}