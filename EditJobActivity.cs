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
using Firebase.Xamarin.Database.Query;
using Lawnmower.Objects;
using Lawnmower.ViewHolders;

namespace Lawnmower
{
    public class EditJobActivity : Fragment
    {
        View view;
        EditJobViewHolder holder;
        DatePickerDialog picker;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.EditJobFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new EditJobViewHolder();
            }

            SetHolderViews();

            SetSpinners();

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

            try
            {
                SetSpinners();

                SetInfo();
            } catch (Exception ex)
            {

            }
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

        private async void AssignClickEvents()
        {
            holder.DateLayout.Click += OpenDatePicker;
            holder.CreateButton.Click += EditJob;
        }

        private void UnassignClickEvents()
        {
            holder.DateLayout.Click -= OpenDatePicker;
            holder.CreateButton.Click -= EditJob;
        }

        private void OpenDatePicker(object sender, EventArgs e)
        {
            picker.DatePicker.CalendarViewShown = true;
            picker.DatePicker.SpinnersShown = false;
            picker.Show();
        }

        private async void EditJob(object sender, EventArgs e)
        {
            bool complete = true;
            var job = Shared.jobList[Shared.selectedJob];

            for (int i = 0; i < holder.AllViewHolder.ChildCount; i++)
            {
                if (holder.AllViewHolder.GetChildAt(i) == holder.FirstNameEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.LastNameEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.AddressEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.CityEdit
                    || holder.AllViewHolder.GetChildAt(i) == holder.ZipcodeEdit)
                {
                    try
                    {
                        var view = (EditText)holder.AllViewHolder.GetChildAt(i);

                        if (view.Text == "")
                        {
                            complete = false;
                        }
                    } catch (Exception ex)
                    {

                    }
                }
            }

            ProgressDialog dialog = new ProgressDialog(this.Context);
            dialog.SetMessage("Updating...");
            dialog.Indeterminate = true;
            dialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            try
            {

                if (complete)
                {
                    dialog.Show();

                    var address = holder.AddressEdit.Text + ", " + holder.CityEdit.Text + ", " + holder.StateSpinner.SelectedItem.ToString() + " " + holder.ZipcodeEdit.Text;
                    job.Address = address;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobAddress).PutAsync(address);

                    job.City = holder.CityEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobCity).PutAsync(holder.CityEdit.Text);

                    job.State = holder.StateSpinner.SelectedItem.ToString();
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobState).PutAsync(holder.StateSpinner.SelectedItem.ToString());

                    job.Zipcode = holder.ZipcodeEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobZipcode).PutAsync(holder.ZipcodeEdit.Text);

                    job.Assignee = Shared.employeeList[(int)holder.AssignSpinner.SelectedItemId].Uid;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobAssignee).PutAsync(holder.AssignSpinner.SelectedItem.ToString());

                    job.ContactNumber = holder.ContactEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobContactNumber).PutAsync(holder.ContactEdit.Text);

                    string[] date = holder.DateText.Text.Split('/', ' ');
                    job.Date = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[0]), Int32.Parse(date[1]));
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobDate).PutAsync(new DateTime(Int32.Parse(date[2]), Int32.Parse(date[0]), Int32.Parse(date[1])));

                    job.FirstName = holder.FirstNameEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobFirstName).PutAsync(holder.FirstNameEdit.Text);

                    job.LastName = holder.LastNameEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobLastName).PutAsync(holder.LastNameEdit.Text);

                    job.Notes = holder.NotesEdit.Text;
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobNotes).PutAsync(holder.NotesEdit.Text);

                    job.JobType = holder.JobSpinner.SelectedItem.ToString();
                    await Shared.firebaseClient.Child(Shared.fbJob).Child(Shared.jobList[Shared.selectedJob].Id).Child(Shared.fbJobJobType).PutAsync(holder.JobSpinner.SelectedItem.ToString());

                    FragmentManager.BeginTransaction().Hide(this).Commit();

                    Shared.GetJobsAsync(this.Activity);
                }
                else
                {
                    Toast.MakeText(this.Context, "Please fill out all fields", ToastLength.Short).Show();
                }
            } catch (Exception ex)
            {

            }
            finally
            {
                dialog.Hide();
            }
        }

        #endregion

        private void SetDate(object sender, EventArgs e)
        {
            var datePicker = (DatePicker)sender;
            holder.DateText.Text = datePicker.DateTime.Month - 1 + "/" + datePicker.DateTime.Day + "/" + datePicker.DateTime.Year;
        }

        private void SetInfo()
        {
            var job = Shared.jobList[Shared.selectedJob];
            holder.FirstNameEdit.Text = job.FirstName;
            holder.LastNameEdit.Text = job.LastName;

            var addressString = job.Address.Split(',');
            holder.AddressEdit.Text = addressString[0];
            holder.CityEdit.Text = job.City;

            for (int i = 0; i < holder.StateSpinner.Count; i++)
            {
                if (holder.StateSpinner.GetItemAtPosition(i).ToString() == job.State)
                {
                    holder.StateSpinner.SetSelection(i);
                }
            }

            holder.ZipcodeEdit.Text = job.Zipcode;
            holder.ContactEdit.Text = job.ContactNumber;

            for (int i = 0; i < holder.JobSpinner.Count; i++)
            {
                if (holder.JobSpinner.GetItemAtPosition(i).ToString() == job.JobType)
                {
                    holder.JobSpinner.SetSelection(i);
                }
            }

            picker = new DatePickerDialog(this.Activity, SetDate, job.Date.Year, job.Date.Month, job.Date.Day);
            holder.DateText.Text = job.Date.Month + "/" + job.Date.Day + "/" + job.Date.Year;

            for (int i = 0; i < Shared.employeeList.Count; i++)
            {
                if (Shared.employeeList[i].Uid == job.Assignee)
                {
                    holder.AssignSpinner.SetSelection(i);
                }
            }

            holder.NotesEdit.Text = job.Notes;
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
    }
}