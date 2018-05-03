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
    class NewUserViewHolder
    {
        public EditText FirstNameEdit { get; set; }
        public EditText LastNameEdit { get; set; }
        public EditText UsernameEdit { get; set; }
        public EditText PasswordEdit { get; set; }
        public EditText VerifyPasswordEdit { get; set; }
        public TextView CreateAccountButton { get; set; }
        public AlertBoxActivity AlertBox { get; set; }
    }
}