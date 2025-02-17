﻿using System;
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
using Firebase.Xamarin.Database.Query;

namespace Lawnmower
{
    public class AssignJobActivity : Fragment
    {
        View view;
        AssignViewHolder holder;

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

        public override void OnDestroy()
        {
            base.OnDestroy();

            UnassignClickEvents();
        }

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);

            SetSpinner();
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

        private void UnassignClickEvents()
        {
            holder.ConfirmButton.Click -= ConfirmClick;
        }

        private async void ConfirmClick(object sender, EventArgs e)
        {
            // Assign job to proper employee
            for (int i = 0; i < Shared.employeeList.Count; i++)
            {
                if (holder.EmployeeSpinner.SelectedItem.ToString() == (Shared.employeeList[i].FirstName + " " + Shared.employeeList[i].LastName))
                {
                    Shared.jobList[Shared.selectedJob].Assignee = Shared.employeeList[i].Uid;

                    await Shared.firebaseClient.Child("jobs").Child(Shared.jobList[Shared.selectedJob].Id).Child("Assignee").PutAsync(Shared.employeeList[i].Uid);
                }
            }

            Shared.jobListAdapterAdmin.NotifyDataSetChanged();

            FragmentManager.BeginTransaction().Hide(this).Commit();

            holder.EmployeeSpinner.SetSelection(0);
        }
        #endregion


        private void SetSpinner()
        {
            var employeeList = new List<string>();
            
            for (int i = 0; i < Shared.employeeList.Count; i++)
            {
                employeeList.Add(Shared.employeeList[i].FirstName + " " + Shared.employeeList[i].LastName);
            }


            holder.EmployeeSpinner.Adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, employeeList);
        }
    }
}