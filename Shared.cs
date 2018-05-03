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
using Firebase.Firestore;
using Firebase.Xamarin.Database;
using Lawnmower.Objects;

namespace Lawnmower
{
    static class Shared
    {
        public static string FirebaseURL = "https://lawnmower-a4296.firebaseio.com/";
        public static FirebaseClient FirebaseClient = new FirebaseClient(FirebaseURL);
        public static List<Job> dummyJobList = new List<Job>();
        public static List<Employee> dummyEmployeeList = new List<Employee>();
        public static Adapters.JobListAdapter jobListAdapter;

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
    }
}