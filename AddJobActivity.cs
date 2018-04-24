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
using Lawnmower.Objects;
using Lawnmower.ViewHolders;

namespace Lawnmower
{
    public class AddJobActivity : Fragment
    {
        View view;
        AddJobViewHolder holder;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.AddJobFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new AddJobViewHolder();
            }

            SetHolderViews();

            SetSpinners();

            AssignClickEvents();

            holder.DateText.Text = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;

            return view;
        }

        public override void OnDestroyView()
        {
            UnassignClickEvents();

            base.OnDestroyView();
        }

        private void SetHolderViews()
        {
            holder.AddressEdit = view.FindViewById<EditText>(Resource.Id.AddressEdit);
            holder.AssignSpinner = view.FindViewById<Spinner>(Resource.Id.AssignSpinner);
            holder.FirstNameEdit = view.FindViewById<EditText>(Resource.Id.FirstNameEdit);
            holder.LastNameEdit = view.FindViewById<EditText>(Resource.Id.LastNameEdit);
            holder.CityEdit = view.FindViewById<EditText>(Resource.Id.CityEdit);
            holder.StateSpinner = view.FindViewById<Spinner>(Resource.Id.StateSpinner);
            holder.ZipcodeEdit = view.FindViewById<EditText>(Resource.Id.ZipEdit);
            holder.ContactEdit = view.FindViewById<EditText>(Resource.Id.ContactEdit);
            holder.JobSpinner = view.FindViewById<Spinner>(Resource.Id.JobSpinner);
            holder.DateLayout = view.FindViewById<LinearLayout>(Resource.Id.DateHolder);
            holder.NotesEdit = view.FindViewById<EditText>(Resource.Id.NotesEdit);
            holder.DateText = view.FindViewById<TextView>(Resource.Id.Date);
            holder.CreateButton = view.FindViewById<TextView>(Resource.Id.CreateButton);
        }

        #region Click Events

        private void AssignClickEvents()
        {
            holder.DateLayout.Click += OpenDatePicker;
            holder.CreateButton.Click += CreateJob;
        }

        private void UnassignClickEvents()
        {
            holder.DateLayout.Click -= OpenDatePicker;
            holder.CreateButton.Click -= CreateJob;
        }

        private void OpenDatePicker(object sender, EventArgs e)
        {
            DatePickerDialog picker = new DatePickerDialog(this.Activity, SetDate, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            picker.DatePicker.CalendarViewShown = true;
            picker.DatePicker.SpinnersShown = false;
            picker.Show();
        }

        private void CreateJob(object sender, EventArgs e)
        {
            // Add job to list
            Shared.jobList[Shared.jobList.Length] = new Job();



            // Perhaps test that all the fields were filled out, but later
        }

        #endregion

        private void SetDate(object sender, EventArgs e)
        {
            var datePicker = (DatePicker)sender;
            holder.DateText.Text = datePicker.DateTime.ToString();
        }

        private void SetSpinners()
        {
            // To be replaced with a call to Firebase for this info instead of hardcoding it
            var jobList = new List<string>() { "Mow", "Weedeat", "Mow and Weedeat" };
            var stateList = new List<string>() { "AZ", "MO", "OH" };
            var employeeList = new List<string>() { "John White", "Earl Grey", "John Buck" };

            holder.JobSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, jobList);
            holder.StateSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, stateList);
            holder.AssignSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, employeeList);
        }
    }
}