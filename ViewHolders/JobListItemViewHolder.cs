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
    class JobListItemViewHolder : Java.Lang.Object
    {
        public TextView AssignText { get; set; }
        public TextView FirstNameText { get; set; }
        public TextView LastNameText { get; set; }
        public TextView AddressNameText { get; set; }
        public TextView ContactText { get; set; }
        public TextView JobTypeText { get; set; }
        public TextView JobDayText { get; set; }
        public TextView JobDateText { get; set; }
        public TextView RepeatingMark { get; set; } // To be an image
        public ImageView DirectionsImage { get; set; }
        public ImageView NotesImage { get; set; }
        public ImageView CancelImage { get; set; }
        public ImageView FinishImage { get; set; }
        public ImageView ApproveImage { get; set; }
        public AssignJobActivity AssignJobFragment { get; set; }
        public NotesActivity NotesFragment { get; set; }
    }
}