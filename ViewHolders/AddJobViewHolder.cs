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
    class AddJobViewHolder
    {
        public EditText FirstNameEdit { get; set; }
        public EditText LastNameEdit { get; set; }
        public EditText AddressEdit { get; set; }
        public EditText CityEdit { get; set; }
        public Spinner StateSpinner { get; set; }
        public EditText ZipcodeEdit { get; set; }
        public EditText ContactEdit { get; set; }
        public Spinner JobSpinner { get; set; }
        public LinearLayout DateLayout { get; set; }
        public TextView DateText { get; set; }
        public Spinner AssignSpinner { get; set; }
        public EditText NotesEdit { get; set; }
        public TextView CreateButton { get; set; }
    }
}