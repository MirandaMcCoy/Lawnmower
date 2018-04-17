using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Lawnmower.Adapters;
using Lawnmower.ViewHolders;

namespace Lawnmower
{
    class ListViewAdapter : BaseAdapter
    {
        Activity activity;
        List<job> lstAccounts;
        LayoutInflater inflater;
        public ListViewAdapter(Activity activity, List<job> lstAccounts)
        {
            this.activity = activity;
            this.lstAccounts = lstAccounts;
        }
        public override int Count
        {
            get { return lstAccounts.Count; }
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
            inflater = (LayoutInflater)activity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.list_Item, null);
            var txtname = itemView.FindViewById<TextView>(Resource.Id.list_name);
            var txtaddress = itemView.FindViewById<TextView>(Resource.Id.list_address);
            if (lstAccounts.Count > 0)
            {
                txtname.Text = lstAccounts[position].cxName;
                txtaddress.Text = lstAccounts[position].cxAddress;
            }
            return itemView;
        }
    }
}