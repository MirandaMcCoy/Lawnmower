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
        DatePickerDialog picker;

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
            ClearInfo();

            AssignClickEvents();

            return view;
        }

        public override void OnDestroy()
        {
            UnassignClickEvents();

            base.OnDestroy();
        }

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);

            SetSpinners();
            ClearInfo();
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
            holder.AllViewHolder = view.FindViewById<LinearLayout>(Resource.Id.AddJobLayoutHolder);
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
            picker.DatePicker.CalendarViewShown = true;
            picker.DatePicker.SpinnersShown = false;
            picker.Show();
        }

        private void CreateJob(object sender, EventArgs e)
        {
            bool complete = true;
            var job = new Job();

            for (int i = 0; i < holder.AllViewHolder.ChildCount; i++)
            {
                if (holder.AllViewHolder.GetChildAt(i) == holder.FirstNameEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.LastNameEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.AddressEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.CityEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.ZipcodeEdit)
                {
                    var view = (EditText)holder.AllViewHolder.GetChildAt(i);

                    if (view.Text == "")
                    {
                        complete = false;
                    }
                }
            }

            if (complete)
            {
                job.Address = holder.AddressEdit.Text + ", " + holder.CityEdit.Text + ", " + holder.StateSpinner.SelectedItem.ToString() + " " + holder.ZipcodeEdit.Text;
                job.City = holder.CityEdit.Text;
                job.State = holder.StateSpinner.SelectedItem.ToString();
                job.Zipcode = holder.ZipcodeEdit.Text;
                job.Assignee = Shared.employeeList[(int)holder.AssignSpinner.SelectedItemId].Uid;
                job.ContactNumber = holder.ContactEdit.Text;

                string[] date = holder.DateText.Text.Split('/', ' ');
                job.Date = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[0]), Int32.Parse(date[1]));

                job.FirstName = holder.FirstNameEdit.Text;
                job.LastName = holder.LastNameEdit.Text;
                job.Notes = holder.NotesEdit.Text;
                job.JobType = holder.JobSpinner.SelectedItem.ToString();

                Shared.CreateJob(this.Activity, job);

                FragmentManager.BeginTransaction().Hide(this).Commit();

                ClearInfo();
            } else
            {
                Toast.MakeText(this.Context, "Please fill out all fields", ToastLength.Short).Show();
            }
        }

        #endregion

        private void SetDate(object sender, EventArgs e)
        {
            var datePicker = (DatePicker)sender;
            holder.DateText.Text = datePicker.DateTime.Month - 1 + "/" + datePicker.DateTime.Day + "/" + datePicker.DateTime.Year;
        }

        private async void SetSpinners()
        {
            // To be replaced with a call to Firebase for this info instead of hardcoding it
            var jobList = new List<string>() { "Mow", "Weedeat", "Mow and Weedeat" };
            var stateList = new List<string>() { "AZ", "MO", "OH" };

            var employeeList = new List<string>();

            for (int i = 0; i < Shared.employeeList.Count; i++)
            {
                employeeList.Add(Shared.employeeList[i].FirstName + " " + Shared.employeeList[i].LastName);
            }

            holder.JobSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, jobList);
            holder.StateSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, stateList);
            holder.AssignSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, employeeList);
        }

        private void ClearInfo()
        {
            holder.FirstNameEdit.Text = "";
            holder.LastNameEdit.Text = "";
            holder.AddressEdit.Text = "";
            holder.CityEdit.Text = "";
            holder.StateSpinner.SetSelection(0);
            holder.ZipcodeEdit.Text = "";
            holder.ContactEdit.Text = "";
            holder.JobSpinner.SetSelection(0);
            picker = new DatePickerDialog(this.Activity, SetDate, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            holder.DateText.Text = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
            holder.AssignSpinner.SetSelection(0);
            holder.NotesEdit.Text = "";
        }
    }
}