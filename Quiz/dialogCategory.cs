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

namespace Quiz
{
    class onAddCategoryEventArgs : EventArgs
    {
        private String mName;

        public string Name
        {
           set { mName = value; }
            get { return mName; }
        }

        public onAddCategoryEventArgs(String Name)
        {
            mName = Name;
        }
    }

    class dialogCategory : DialogFragment
    {
        private EditText mTxtName;
        private Button mBtnAddCategory;

        public EventHandler<onAddCategoryEventArgs> mOnAddCategoryCompleted;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.addCategory,container,false); // change to addCategory layout

            mTxtName = view.FindViewById<EditText>(Resource.Id.txtCategoryName);
            mBtnAddCategory = view.FindViewById<Button>(Resource.Id.btnAddCategory);
            mBtnAddCategory.Click += MBtnAddCategory_Click;

            return view;
        }

        private void MBtnAddCategory_Click(object sender, EventArgs e)
        {
            mOnAddCategoryCompleted.Invoke(this, new onAddCategoryEventArgs(mTxtName.Text));
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_login_animation;
        }
    }
}