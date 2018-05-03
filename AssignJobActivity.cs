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
    public class AssignJobActivity : Fragment
    {
        View view;
        AssignViewHolder holder;
        Job selectedJob;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = this.View;

            view = inflater.Inflate(Resource.Layout.AssignJobFragment, container, false);

            FragmentManager.BeginTransaction().Hide(this).Commit();

            if (holder == null)
            {
                holder = new AssignViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();

            SetSpinner();

            return view;
        }

        private void SetHolderViews()
        {
            holder.EmployeeSpinner = view.FindViewById<Spinner>(Resource.Id.EmployeeListSpinner);
            holder.ConfirmButton = view.FindViewById<TextView>(Resource.Id.ConfirmButton);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.ConfirmButton.Click += ConfirmClick;
        }

        private void ConfirmClick(object sender, EventArgs e)
        {
            // Assign job to proper employee
            Shared.dummyJobList[Shared.selectedJob].Assignee = holder.EmployeeSpinner.SelectedItem.ToString();

            Shared.jobListAdapter.NotifyDataSetChanged();

            FragmentManager.BeginTransaction().Hide(this).Commit();

            holder.EmployeeSpinner.SetSelection(0);
        }
        #endregion


        private void SetSpinner()
        {
            var employeeList = new List<string>();

            if (Shared.dummyEmployeeList.Count == 0)
            {
                Shared.FillEmployeeList();
            }

            for (int i = 0; i < Shared.dummyEmployeeList.Count; i++)
            {
                employeeList.Add(Shared.dummyEmployeeList[i].FirstName + " " + Shared.dummyEmployeeList[i].LastName);
            }


            holder.EmployeeSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, employeeList);
        }
    }
}