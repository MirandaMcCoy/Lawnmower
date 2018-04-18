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
using Lawnmower.ViewHolders;

namespace Lawnmower.Adapters
{
    class EmployeeListAdapter : BaseAdapter
    {

        Context context;

        public EmployeeListAdapter(Context context)
        {
            this.context = context;
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
            var view = convertView;
            JobListItemViewHolder holder = null;

            if (view != null)
                holder = view.Tag as JobListItemViewHolder;

            if (holder == null)
            {
                holder = new JobListItemViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view.Tag = holder;
            }

            return view;
        }

        public override int Count
        {
            get
            {
                return 0;
            }
        }

    }
}