using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public static string FirebaseURL = "https://test-a05bd.firebaseio.com/";
        public static FirebaseClient FirebaseClient = new FirebaseClient(FirebaseURL);
        public static List<Job> jobList = new List<Job>();
        public static List<Employee> dummyEmployeeList = new List<Employee>();
        public static Adapters.JobListAdapter jobListAdapter;
        public static int selectedJob;
        public static bool showAdmin = false;

        public static void testDB()
        {
            //JavaDictionary<string, object> newUser = new JavaDictionary<string, object>();

            //newUser.Add("userName", "NathanDunn");
            //newUser.Add("passWord", "password");

            //fs.Collection("Users").Document("0").Set(newUser);
        }

        public static void FillEmployeeList()
        {
            for (int i = 0; i < 4; i++)
            {

                dummyEmployeeList.Add(new Employee());
            }

            dummyEmployeeList[0].FirstName = "Unassigned";
            dummyEmployeeList[0].LastName = "";

            dummyEmployeeList[1].FirstName = "Earl";
            dummyEmployeeList[1].LastName = "Gray";

            dummyEmployeeList[2].FirstName = "John";
            dummyEmployeeList[2].LastName = "White";

            dummyEmployeeList[3].FirstName = "Jane";
            dummyEmployeeList[3].LastName = "Doe";
        }
        public async static void TestIfAdmin()
        {
            var users = await Shared.FirebaseClient.Child("users").OnceAsync<Objects.User>();
            foreach (var item in users)
            {
                var y = item.Object.Uid;
                if (FirebaseAuth.Instance.CurrentUser.Uid == item.Object.Uid)
                {
                    showAdmin = item.Object.Admin;
                }
            }
        }
        public async static void GetJobs(Activity context)
        {          
            if (showAdmin == true || showAdmin == false)
            {              
                try
                {
                    var empJobs = await Shared.FirebaseClient.Child("jobs").OnceAsync<Objects.Job>();
                    for (int i = 0; i < jobList.Count; i++)
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

                    }
                }
                catch(Exception ex)
                {
                    Toast.MakeText(context, ex.ToString(), ToastLength.Short).Show();
                }
            }
            else
            {

            }
        }
        public async static void CreateJobs()
        {
            var job = new Objects.Job();
            job.Address = "1234 Banning St";
            job.FirstName = "John";
            job.LastName = "Smith";
            job.Notes = "Please work please work please work";
            job.ContactNumber = "417 555 1234";
            job.Date = new DateTime().Date;
            job.Assignee = "unassigned";
            var Item = await Shared.FirebaseClient.Child("jobs").PostAsync<Job>(job);
        }
    }
}