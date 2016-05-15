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
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : Activity
    {
        private Button mBtnLogOut;
        private Button mBtnSelectCategory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menu);

            mBtnLogOut = FindViewById<Button>(Resource.Id.btnLogOut);
            mBtnSelectCategory = FindViewById<Button>(Resource.Id.btnSelectCategory);


            mBtnLogOut.Click += delegate
            {
                has.saveset("id_user", null);
                Finish();
                StartActivity(typeof(MainActivity));

            };

            mBtnSelectCategory.Click += delegate
            {

                StartActivity(typeof(selectCategoryActivity));
            };
            // Create your application here
        }
    }
}