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
    class LoginViewHolder
    {
        public EditText UsernameEdit { get; set; }
        public EditText PasswordEdit { get; set; }
        public Button LoginButton { get; set; }
        public TextView NewUserText { get; set; }
        public AlertBoxActivity AlertBox { get; set; }
    }
}