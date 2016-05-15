using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Org.Json;

using Newtonsoft.Json.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Quiz
{
    [Activity(Label = "quizActiviti")]
    public class quizActiviti : Activity
    {

        private int question; // aktualnie pobierane pytanie z tablicy
        private int time; // licznik czasu
        private int timeMinus;
        private string indexAnswer;

        private string id_user;

        private double timeAll;
        private int goodAnswer;
        private string id_cat = "";

        
        private JArray questionJson;
        private System.Timers.Timer timer;
        private ProgressBar czasBar;
        private TextView txtQuestion;
        private TextView txtPozostalo;
        private TextView txtQuizNr;
        private RadioGroup RadioGroup_Answer;
        private Button btnOdpowiedz;

     
       protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.quiz);

            if (this.Intent.Extras.ContainsKey("id_cat"))
            {
                id_cat = (String)this.Intent.Extras.Get("id_cat");
            }

            RadioGroup_Answer = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            txtPozostalo = FindViewById<TextView>(Resource.Id.txtPozostalo);
            txtQuizNr = FindViewById<TextView>(Resource.Id.txtQuizNr);
            czasBar = FindViewById<ProgressBar>(Resource.Id.czasBar);
            btnOdpowiedz = FindViewById<Button>(Resource.Id.btnOdpowiedz);

            id_user = has.retrieveset("id_user");

            WebClient webclient = new WebClient();
            txtQuizNr.Text = "Pobieram pytania...";

          //  webclient.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();
           
            webclient.DownloadStringAsync(new Uri("http://robocza.h2g.pl/quiz/questionA.php?id_user=" + id_user + "&id_cat=" + id_cat));
            webclient.DownloadStringCompleted += webClient_DownloadStringCompleted;

            //klikanie ; ) 
            btnOdpowiedz.Click+= delegate
                 {

                     nextQuestion();

                 };


        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            
            // Parse to Json String

            String response = e.Result.ToString();

            if (string.IsNullOrEmpty(response.Trim())) // brak pytan w bazie
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Brak pytañ!");
                alert.SetMessage("Brak pytañ w bazie, poczekaj na nowe");
                alert.SetNegativeButton("OK", (senderAlert, args) => {
                    Toast.MakeText(this, "Czekaj cierpliwie na nowe pytania!!", ToastLength.Short).Show();
                    Finish();
                    StartActivity(typeof(MenuActivity));
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }

            //pobranie pytan i rozpoaczecie quizu
            else
            {
                questionJson = JArray.Parse(response);
                question = 0;
                timeAll = 0;
                goodAnswer = 0;
                txtQuestion.Text = "";
                nextQuestion();

            }

        }


        private void nextQuestion()
        {
            if (!String.IsNullOrEmpty(txtQuestion.Text))
            {
                if (getIndexAnswer() || time < 0)
                {

                    if (question < questionJson.Count) nextQuestionSet(); //kolejne
                    else
                    { // end question 

                        timer.Stop();
                        //checkAnswerAndSendResult();
                        String text;
                        if (goodAnswer > 0) text = "Koniec quizu. Odpowiedzia³eœ poprawnie na: " + goodAnswer + " pytania. Œredni czas poprawnej odpowiedzi: " + String.Format("{0:N2}", timeAll/goodAnswer) + "s.";
                        else text = "Koniec quizu. Nie odpowiedzia³eœ poprawnie na ¿adne pytanie :(";


                        //set alert for executing the task
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Koniec Quizu!");
                        alert.SetMessage(text);
                        alert.SetNegativeButton("Koniec", (senderAlert, args) => {
                            Finish();
                            Toast.MakeText(this, "Zagraj ponownie!", ToastLength.Short).Show();
                        });

                        Dialog dialog = alert.Create();
                        dialog.Show();

                    }
                }
            }
            else // pierwsze pytanie
            {
                nextQuestionSet();
                Start_timer();
            }
        }

        private void nextQuestionSet()
        {
            if (!String.IsNullOrEmpty(txtQuestion.Text)) checkAnswerAndSendResult();
            txtQuizNr.Text = "Quiz nr: " + questionJson[question]["id_pyt"].ToString();
            txtQuestion.Text = questionJson[question]["question"].ToString();
            SetAnswerText();
            czasBar.Progress = 100;
            time = int.Parse(questionJson[question]["time"].ToString());
            timeMinus = 100 / time;

            txtPozostalo.Text = "Pozosta³o Ci " + time + " s";
            question++;
        }

        //sprawdzenie czy zaznaczono odpowiedz

        private bool getIndexAnswer()
        {
            RadioButton checkedButton = FindViewById<RadioButton>(RadioGroup_Answer.CheckedRadioButtonId);
            if (checkedButton !=null)
            {
                checkedButton.Checked = false;
                int checkedIndex = RadioGroup_Answer.IndexOfChild(checkedButton);
                indexAnswer = checkedIndex.ToString();
                return true;
            }
            else
            {
                if (time >= 0) Toast.MakeText(this, "Nie zaznaczyles zadnej odpowiedzi", ToastLength.Short).Show();
                return false;
            }
        }


        //ustawienie odpowiedzi pod RadioButton
        private void SetAnswerText()
        {
            string key;
            for (int i = 0; i < RadioGroup_Answer.ChildCount; i++)
            {
                key = "answer" + i;
                ((RadioButton)RadioGroup_Answer.GetChildAt(i)).Text = questionJson[question][key].ToString();
            }
        }


      

  private void checkAnswerAndSendResult()
  {
      double timeAnswer = -1;

      if (indexAnswer == questionJson[question - 1]["true_answ"].ToString()) // poprawna odpowiedz
      {
          timeAnswer = (100 - czasBar.Progress) / 10;
          goodAnswer++;
          timeAll += timeAnswer;

      }
        WebClient webclient = new WebClient();
         webclient.OpenReadAsync(new Uri("http://robocza.h2g.pl/quiz/answer.php?id_user=" + id_user + "&id_pyt=" + questionJson[question - 1]["id_pyt"].ToString() + "&time=" + timeAnswer));

  }


        //odmierzanie czasu
        private void Start_timer()
        {
            timer = new System.Timers.Timer(1000);
            timer.Interval = 1000;
            timer.Elapsed += OnTimeEvent;
            timer.Enabled = true;
            timer.Start();
        }



        private void OnTimeEvent(object sender, object e)
        {
            RunOnUiThread(delegate {
                time--;
                czasBar.Progress -= timeMinus;
                if (time >= 0) txtPozostalo.Text = "Pozosta³o Ci " + time + " s";

                else nextQuestion();
             
            });
        }

    }
}