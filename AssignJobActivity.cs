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

namespace Lawnmower
{
    public class AssignJobActivity : Fragment
    {
        View view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.AssignJobFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            Spinner spinner = view.FindViewById<Spinner>(Resource.Id.EmployeeListSpinner);

            return view;
        }
    }
}