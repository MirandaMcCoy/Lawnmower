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

namespace Lawnmower.ViewHolders
{
    class ListViewHolder
    {
        public ListView JobListView { get; set; }
        public ImageView AddJobImage { get; set; }
        public AddJobActivity AddJobFragment { get; set; }
        public AssignJobActivity AssignJobFragment { get; set; }
    }
}