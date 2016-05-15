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
using System.Net;
using Newtonsoft.Json;



namespace Quiz
{
    [Activity(Label = "RankingActivity")]
    public class RankingActivity : Activity
    {
        private ListView mListView;
        private BaseAdapter<ranking> mAdapter;
        private List<ranking> mClient;
        private ProgressBar mProgressBar;
        private WebClient webClient;
        private Uri mUrl;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ranking);

            mListView = FindViewById<ListView>(Resource.Id.listTime);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            webClient = new WebClient();
            mUrl = new Uri("http://www.robocza.h2g.pl/quiz/ranking.php?id_user=1");

            webClient.DownloadDataAsync(mUrl);
            webClient.DownloadDataCompleted += webClient_DownloadDataCompleted;

            var button = FindViewById<Button>(Resource.Id.btn_3);

            button.Click += button_Click;
        }
        
        private void webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
                string json = Encoding.UTF8.GetString(e.Result);
                mClient = JsonConvert.DeserializeObject<List<ranking>>(json);
                mAdapter = new listarankListAdapter(this, Resource.Layout.ranking, mClient);
                mListView.Adapter = mAdapter;
                mProgressBar.Visibility = ViewStates.Gone;         
        }      
        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Result));
            });

        }

       
        protected override void OnStart()
        {
            base.OnStart();
        }

        void button_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RankingActivity));
            StartActivity(intent);
        }
        

    }
    }
