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
using System.Threading;


namespace Quiz
{
    class OnAddUserEventArgs : EventArgs
    {
        private string mLogin;
        private string mPassword;
        private string mPassword2;
        private string mEmail;

        public string Login
        {
            get { return mLogin; }
            set { mLogin = value; }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public string Password2
        {
            get { return mPassword2; }
            set { mPassword2 = value; }
        }
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public OnAddUserEventArgs(string login, string email, string password, string password2) :base()
        {
            Login = login;
            Password = password;
            Password2 = password2;
            Email = email;
        }

    }

    class dialog_Register :DialogFragment 
    {
        private EditText mTxtLogin;
        private EditText mTxtPassword;
        private EditText mTxtPassword2;
        private EditText mTxtMail;
        private Button mBtnAddUser;

        public event EventHandler<OnAddUserEventArgs> mOnAddUserComplete;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.register, container, false);

            mTxtLogin = view.FindViewById<EditText>(Resource.Id.txtLogin);
            mTxtMail = view.FindViewById<EditText>(Resource.Id.txtMail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mTxtPassword2 = view.FindViewById<EditText>(Resource.Id.txtPassword2);
            mBtnAddUser = view.FindViewById<Button>(Resource.Id.btnAddUser);

            mBtnAddUser.Click += MBtnAddUser_Click;

            return view;
        }

     

        private void MBtnAddUser_Click(object sender, EventArgs e)
        {
            OnAddUserEventArgs userArgs =  new OnAddUserEventArgs(mTxtLogin.Text, mTxtMail.Text, mTxtPassword.Text, mTxtPassword2.Text);
        
      
            mOnAddUserComplete.Invoke(this, userArgs );
            
           // this.Dismiss();
           
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); 
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}