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

namespace Lawnmower.Objects
{
    class Job
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string JobType { get; set; } // Eventually make this an enum
        public DateTime Date { get; set; }
        public bool Repeating { get; set; }
        public string Notes { get; set; }
    }
}