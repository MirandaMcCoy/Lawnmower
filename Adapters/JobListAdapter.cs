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

namespace Lawnmower.Adapters
{
    class JobListAdapter : BaseAdapter
    {

        Activity context;
        List<Job> jobs;
        JobListItemViewHolder holder;
        View view;

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

            if (view == null)
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

            SetViews(position);

            return view;
        }
        
        public override int Count
        {
            get
            {
                return jobs.Count;
            }
        }

        private void SetViews(int position)
        {
            holder.FirstNameText.Text = jobs[position].FirstName;
            holder.LastNameText.Text = jobs[position].LastName;
            holder.AddressNameText.Text = jobs[position].Address;
        }

        private void SetHolderViews()
        {
            holder.AddressNameText = view.FindViewById<TextView>(Resource.Id.AddressTextView);
            holder.FirstNameText = view.FindViewById<TextView>(Resource.Id.FirstNameTextView);
            holder.LastNameText = view.FindViewById<TextView>(Resource.Id.LastNameTextView);
        }

        private void AssignClickEvents()
        {
            holder.FirstNameText.Click += AssignJobOpen;
        }

        private void AssignJobOpen(object sender, EventArgs e)
        {
            this.context.FragmentManager.BeginTransaction().Show(this.context.FragmentManager.FindFragmentById<AssignJobActivity>(Resource.Id.AssignJobMenu)).Commit();
        }

    }
}