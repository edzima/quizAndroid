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
        private Button mBtnToAddCategory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menu);
            mBtnToAddCategory = FindViewById<Button>(Resource.Id.btnToAddCategory);

            mBtnToAddCategory.Click += delegate
            {
                FragmentTransaction transcation = FragmentManager.BeginTransaction();
                dialogCategory dialogCat = new dialogCategory();
                dialogCat.Show(transcation, "dialog fragment");
                dialogCat.mOnAddCategoryCompleted += DialogCat_mOnAddCategoryCompleted;

            };
        }

        private void DialogCat_mOnAddCategoryCompleted(object sender, onAddCategoryEventArgs e)
        {
            DialogFragment dialogAddCategory = sender as DialogFragment; // do zamkniecia
        }
    }
}