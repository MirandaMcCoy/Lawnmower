using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lawnmower.ViewHolders;

using Android.Support.V7.App;
using Lawnmower.Objects;
using Lawnmower.Adapters;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Firebase.Auth;


namespace Lawnmower
{
    [Activity(Label = "NewUserActivity", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar")]
    public class NewUserActivity : Activity
    {
        private NewUserViewHolder holder;
        private int minPasswordLength = 8;
        private bool passwordContainsLetter = false;
        private bool passwordContainsNum = false;
        private bool passwordContainsSpace = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewUser);

            if (holder == null)
            {
                holder = new NewUserViewHolder();
            }

            SetHolderViews();

            AssignClickEvents();
        }

        protected override void OnDestroy()
        {
            UnassignClickEvents();

            base.OnDestroy();
        }

        private void SetHolderViews()
        {
            holder.FirstNameEdit = FindViewById<EditText>(Resource.Id.FirstNameEdit);
            holder.LastNameEdit = FindViewById<EditText>(Resource.Id.LastNameEdit);
            holder.UsernameEdit = FindViewById<EditText>(Resource.Id.UsernameEdit);
            holder.PasswordEdit = FindViewById<EditText>(Resource.Id.PasswordEdit);
            holder.VerifyPasswordEdit = FindViewById<EditText>(Resource.Id.VerifyPasswordEdit);
            holder.CreateAccountButton = FindViewById<TextView>(Resource.Id.CreateUserButton);
            holder.AlertBox = FragmentManager.FindFragmentById<AlertBoxActivity>(Resource.Id.AlertBoxFragment);
        }

        #region Click Events
        private void AssignClickEvents()
        {
            holder.CreateAccountButton.Click += CreateAccountClick;
        }

        private void UnassignClickEvents()
        {
            holder.CreateAccountButton.Click -= CreateAccountClick;
        }

        private async void CreateAccountClick(object sender, EventArgs e)
        {
            // Verify that there is not an account with that username in the db
            // If there is, alert the user

            if (holder.PasswordEdit.Text == holder.VerifyPasswordEdit.Text)
            {
                if (holder.PasswordEdit.Text.Length >= minPasswordLength)
                {
                    var passwordChars = holder.PasswordEdit.Text.ToCharArray();

                    for (int i = 0; i < passwordChars.Length; i++)
                    {
                        var chara = passwordChars[i];
                        if((chara >= 'A' && chara <= 'Z') || (chara >= 'a' && chara <= 'z'))
                        {
                            passwordContainsLetter = true;
                        }

                        if((chara >= '0' && chara <= '9'))
                        {
                            passwordContainsNum = true;
                        }

                        if ((chara == ' '))
                        {
                            passwordContainsSpace = true;
                        }
                    }

                    if (passwordContainsNum && passwordContainsLetter && !passwordContainsSpace)
                    {
                        ProgressDialog dialog = new ProgressDialog(this);
                        dialog.SetMessage("Creating user...");
                        dialog.Indeterminate = true;
                        dialog.SetCancelable(false);
                        dialog.SetProgressStyle(ProgressDialogStyle.Spinner);

                        try
                        {
                            dialog.Show();
                            UnassignClickEvents(); // So user cannot spam the button

                            await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(holder.UsernameEdit.Text, holder.PasswordEdit.Text);
                            CreateUser();
                            Shared.CheckIfAdmin();

                            StartActivity(typeof(JobListActivity));

                            Finish();

                        }
                        catch (Exception ex)
                        {
                            holder.AlertBox.SetAlert(Resources.GetString(Resource.String.sign_in_failed));
                            FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();

                            holder.UsernameEdit.Text = String.Empty;
                            holder.PasswordEdit.Text = String.Empty;
                            holder.VerifyPasswordEdit.Text = String.Empty;

                            AssignClickEvents();
                        }
                        finally
                        {
                            dialog.Hide();
                        }
                    } else
                    {
                        holder.AlertBox.SetAlert(Resources.GetString(Resource.String.password_does_not_meet_reqs));
                        FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();

                        holder.PasswordEdit.Text = String.Empty;
                        holder.VerifyPasswordEdit.Text = String.Empty;
                    }
                }
                else
                {
                    holder.AlertBox.SetAlert(Resources.GetString(Resource.String.password_does_not_meet_length));
                    FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();

                    holder.PasswordEdit.Text = String.Empty;
                    holder.VerifyPasswordEdit.Text = String.Empty;
                }
                
            }
            else
            {
                holder.AlertBox.SetAlert(Resources.GetString(Resource.String.mismatched_passwords));
                FragmentManager.BeginTransaction().Show(holder.AlertBox).Commit();

                holder.PasswordEdit.Text = String.Empty;
                holder.VerifyPasswordEdit.Text = String.Empty;
                
            }
            
        }
#endregion
        private async void CreateUser()
        {
            var user = new Objects.User();
            user.Admin = false;
            user.FirstName = holder.FirstNameEdit.Text.FirstOrDefault().ToString().ToUpper() + holder.FirstNameEdit.Text.Substring(1);
            user.LastName = holder.LastNameEdit.Text.FirstOrDefault().ToString().ToUpper() + holder.LastNameEdit.Text.Substring(1);
            user.Email = holder.UsernameEdit.Text;
            user.Uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var userTask = await Shared.firebaseClient.Child("users").PostAsync<User>(user);
        }
    }
}