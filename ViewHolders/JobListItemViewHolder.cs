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
        public TextView FirstNameText { get; set; }
        public TextView LastNameText { get; set; }
        public TextView AddressNameText { get; set; }
    }
}