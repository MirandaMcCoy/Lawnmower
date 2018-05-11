using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
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
        public NotesActivity NotesFragment { get; set; }
        public EditJobActivity EditJobFragment { get; set; }
        public ImageView MenuImage { get; set; }
        public MenuFragment MenuFragment {get; set;}
        public AlertBoxActivity AlertBox { get; set; }
        public SwipeRefreshLayout SwipeRefresh { get; set; }
    }
}