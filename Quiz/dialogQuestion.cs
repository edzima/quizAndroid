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
    class onAddQuestionEventArgs : EventArgs
    {
        private String mName;

        public string Name
        {
            set { mName = value; }
            get { return mName; }
        }

        public onAddQuestionEventArgs(String Name)
        {
            mName = Name;
        }
    }

    class dialogQuestion : DialogFragment
    {
        private EditText mTxTAddQuestion;
        private Button mBtnAddQuestion;

        public EventHandler<onAddQuestionEventArgs> mOnAddQuestionCompleted;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.addQuiz, container, false); // change to addquestion layout

            mTxTAddQuestion = view.FindViewById<EditText>(Resource.Id.txtAddQuestion);
            mBtnAddQuestion = view.FindViewById<Button>(Resource.Id.btnAddQuestion);
            mBtnAddQuestion.Click += MBtnAddQuestion_Click;

            return view;
        }

        private void MBtnAddQuestion_Click(object sender, EventArgs e)
        {
            mOnAddQuestionCompleted.Invoke(this, new onAddQuestionEventArgs(mTxTAddQuestion.Text));
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_login_animation;
        }
    }
}