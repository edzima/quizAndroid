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
    [Activity(Label = "selectCategoryActivity")]
    public class selectCategoryActivity : Activity
    {
        private List<String> catName;
        private ListView listvieww;
  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.selectCategory);
            listvieww = FindViewById<ListView>(Resource.Id.listView1);

            listvieww.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
            {
                //pobranie nazwy kategorii i przejscie do quizu
                string selectedFromList = listvieww.GetItemAtPosition(e.Position).ToString();
                Intent intent = new Intent(this, typeof(quizActiviti));
                intent.PutExtra("id_cat", selectedFromList);
                StartActivity(intent);

            };

            var webclient = new WebClient();
            webclient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webclient.DownloadStringAsync(new Uri(string.Format("http://www.robocza.h2g.pl/quiz/categoryA.php?cat=1")));

        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Parse to Json String
            if (!string.IsNullOrEmpty(e.Result))
            {

                catName = new List<String>();
                String[] categories = e.Result.Trim().Split('\n');
           
        
                foreach (string cat in categories){
                    catName.Add(cat);
                }
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, catName);
                listvieww.Adapter = adapter; 

            }
        }
    }



}