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
    class onLoginEventsArgs : EventArgs
    {
        private string mLogin;
        private string mPassword;
      

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


        public onLoginEventsArgs(string login, string password) : base()
        {
            Login = login;
            Password = password;
        }

    }

    class dialog_Login : DialogFragment
    {
        private EditText mTxtLogin;
        private EditText mTxtPassword;

        private Button mBtnLogin;

        public event EventHandler<onLoginEventsArgs> mOnLoginComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.login, container, false);

            mTxtLogin = view.FindViewById<EditText>(Resource.Id.txtLoginLogin);
       
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtLoginPassword);
 
            mBtnLogin = view.FindViewById<Button>(Resource.Id.btnMenuIn);

            mBtnLogin.Click += MBtnLogin_Click;

            return view;
        }

        private void MBtnLogin_Click(object sender, EventArgs e)
        {

            mOnLoginComplete.Invoke(this, new onLoginEventsArgs(mTxtLogin.Text, mTxtPassword.Text));
        }

      

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_login_animation;
        }
    }
}