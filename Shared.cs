﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Xamarin.Database;
using Lawnmower.Objects;

namespace Lawnmower
{
    static class Shared
    {
        public static string firebaseURL = "https://test-a05bd.firebaseio.com/";
        public static FirebaseClient firebaseClient = new FirebaseClient(firebaseURL);

        public static List<Job> jobList = new List<Job>();
        public static List<Employee> employeeList = new List<Employee>();

        public static Adapters.JobListAdapter jobListAdapter;

        public static int selectedJob;

        public static bool showAdmin = false;

        public async static Task<bool> GetEmployeesAsync(Activity context)
        {
            var employees = await Shared.firebaseClient.Child("users").OnceAsync<Objects.User>();

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees.ElementAt(i).Object;
                if (employees.ElementAt(i).Object.Uid != FirebaseAuth.Instance.CurrentUser.Uid && employees.ElementAt(i).Object.LastName != "")
                {
                    employeeList.Add(new Employee());

                    employeeList[i].FirstName = employee.FirstName;
                    employeeList[i].LastName = employee.LastName;
                    employeeList[i].Uid = employee.Uid;
                } else
                {
                    employeeList.Add(new Employee());

                    employeeList[i].FirstName = "Assign to";
                    employeeList[i].LastName = "self";
                    employeeList[i].Uid = employee.Uid;
                }
            }

            employeeList.Add(new Employee());
            employeeList[employeeList.Count - 1].LastName = "Unassigned";

            return true;
        }

        public async static Task<bool> CheckIfAdmin()
        {
            var users = await Shared.firebaseClient.Child("users").OnceAsync<Objects.User>();

            foreach (var user in users)
            {
                if (FirebaseAuth.Instance.CurrentUser.Uid == user.Object.Uid)
                {
                    showAdmin = user.Object.Admin;
                }
            }

            return showAdmin;
        }

        public static async void GetJobsAsync(Activity context)
        {
            ProgressDialog dialog = new ProgressDialog(context);
            dialog.SetMessage("Retrieving jobs...");
            dialog.Indeterminate = true;
            dialog.SetProgressStyle(ProgressDialogStyle.Spinner);

            jobList = new List<Job>();

            if (showAdmin) // If an admin, show all jobs
            {
                try
                {
                    dialog.Show();

                    var empJobs = await Shared.firebaseClient.Child("jobs").OnceAsync<Objects.Job>();

                    for (int i = 0; i < empJobs.Count; i++)
                    {
                        jobList.Add(new Job());
                        jobList[i].FirstName = empJobs.ElementAt(i).Object.FirstName;
                        jobList[i].LastName = empJobs.ElementAt(i).Object.LastName;
                        jobList[i].Address = empJobs.ElementAt(i).Object.Address;
                        jobList[i].ContactNumber = empJobs.ElementAt(i).Object.ContactNumber;
                        jobList[i].JobType = empJobs.ElementAt(i).Object.JobType;
                        jobList[i].Date = empJobs.ElementAt(i).Object.Date;
                        jobList[i].Notes = empJobs.ElementAt(i).Object.Notes;
                        jobList[i].Assignee = empJobs.ElementAt(i).Object.Assignee;
                        jobList[i].Id = empJobs.ElementAt(i).Key;
                    }

                }
                catch(Exception ex)
                {
                    Toast.MakeText(context, "There was a problem retrieving jobs.", ToastLength.Long).Show();
                }
                finally
                {
                    dialog.Hide();
                }
            }
            else // If not admin, only show jobs assigned to the employee
            {
                try
                {
                    dialog.Show();

                    var empJobs = await Shared.firebaseClient.Child("jobs").OnceAsync<Objects.Job>();

                    for (int i = 0; i < empJobs.Count; i++)
                    {
                        if (empJobs.ElementAt(i).Object.Assignee == FirebaseAuth.Instance.CurrentUser.Uid)
                        {
                            jobList.Add(new Job());
                            jobList[jobList.Count-1].FirstName = empJobs.ElementAt(i).Object.FirstName;
                            jobList[jobList.Count-1].LastName = empJobs.ElementAt(i).Object.LastName;
                            jobList[jobList.Count-1].Address = empJobs.ElementAt(i).Object.Address;
                            jobList[jobList.Count-1].ContactNumber = empJobs.ElementAt(i).Object.ContactNumber;
                            jobList[jobList.Count-1].JobType = empJobs.ElementAt(i).Object.JobType;
                            jobList[jobList.Count-1].Date = empJobs.ElementAt(i).Object.Date;
                            jobList[jobList.Count-1].Notes = empJobs.ElementAt(i).Object.Notes;
                            jobList[jobList.Count-1].Assignee = empJobs.ElementAt(i).Object.Assignee;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(context, "There was a problem retrieving jobs.", ToastLength.Long).Show();
                }
                finally
                {
                    dialog.Hide();
                }
            }

            try
            {
                Shared.jobListAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {

            }
        }

        public async static void CreateJob(Activity context, Job job)
        {
            var jobTask = await Shared.firebaseClient.Child("jobs").PostAsync<Job>(job);

            GetJobsAsync(context);
        }
    }
}