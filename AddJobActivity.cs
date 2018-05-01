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
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace Lawnmower
{
    public class AddJobActivity : Fragment
    {
        View view;
        AddJobViewHolder holder;
        private const string firebaseURL = "https://lawnmower-a4296.firebaseio.com/";
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
            picker.DatePicker.CalendarViewShown = true;
            picker.DatePicker.SpinnersShown = false;
            picker.Show();
        }
        private async Task LoadData()
        {
            var firebase = new FirebaseClient(firebaseURL);
            var items = await firebase.Child("Jobs").OnceAsync<NewJobs>();
            foreach (var item in items)
            {
                NewJobs myJobs = new NewJobs();
                myJobs.cusAddress = item.Object.cusAddress;
                myJobs.cusCity = item.Object.cusCity;
                myJobs.cusZip = item.Object.cusZip;
                myJobs.cusPhone = item.Object.cusPhone;
                myJobs.cusTasks = item.Object.cusTasks;
                myJobs.cusNotes = item.Object.cusNotes;
            }
        }
        private async void CreateJob(object sender, EventArgs e)
        {
            // Add job to list
            //Shared.jobList[Shared.jobList.Length] = new Job(); --Miranda code, possibly needed later
            NewJobs newjob = new NewJobs();
            newjob.cusAddress = holder.AddressEdit.Text;
            newjob.cusCity = holder.CityEdit.Text;
            newjob.cusZip = holder.ZipcodeEdit.Text;
            newjob.cusPhone = holder.ContactEdit.Text;
            newjob.cusTasks = holder.JobSpinner.SelectedItem.ToString();
            newjob.cusNotes = holder.NotesEdit.Text;
            var firebase = new FirebaseClient(firebaseURL);
            //add item
            var item = await firebase.Child("jobs").PostAsync<NewJobs>(newjob);
            //HERE!

            // Update job list -- This use to add to the dummy job list. Hopefully we can change this in time.
            //Shared.jobListAdapter.jobs = Shared.dummyJobList.ToList();
            //Shared.jobListAdapter.NotifyDataSetChanged();

            FragmentManager.BeginTransaction().Hide(this).Commit();

            ClearInfo();

            // Perhaps test that all the fields were filled out, but later
        }

        #endregion

        private void SetDate(object sender, EventArgs e)
        {
            var datePicker = (DatePicker)sender;
            holder.DateText.Text = datePicker.DateTime.Month - 1 + "/" + datePicker.DateTime.Day + "/" + datePicker.DateTime.Year;
        }

        private void SetSpinners()
        {
            // To be replaced with a call to Firebase for this info instead of hardcoding it
            var jobList = new List<string>() { "Mow", "Weedeat", "Mow and Weedeat" };
            var stateList = new List<string>() { "AZ", "MO", "OH" };
            var employeeList = new List<string>();

            if (Shared.dummyEmployeeList.Count == 0)
            {
                Shared.FillEmployeeList();
            }

            for (int i = 0; i < Shared.dummyEmployeeList.Count; i++)
            {
                employeeList.Add(Shared.dummyEmployeeList[i].FirstName + " " + Shared.dummyEmployeeList[i].LastName);
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