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
        private Button mBtnAddQuestion;
        private Button mBtnRank;
        private Button mBtnQuiz;

        private Button mBtnLogOut;
        private Button mBtnSelectCategory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.menu);

            mBtnLogOut = FindViewById<Button>(Resource.Id.btnLogOut);
            mBtnSelectCategory = FindViewById<Button>(Resource.Id.btnSelectCategory);


            mBtnLogOut.Click += delegate
            mBtnAddQuestion = FindViewById<Button>(Resource.Id.btnAddQuestion);

            mBtnRank = FindViewById<Button>(Resource.Id.btnRank);
            mBtnRank.Click += delegate
            {
                StartActivity(typeof(RankingActivity));
            };

            mBtnQuiz = FindViewById<Button>(Resource.Id.btnQuiz);
            mBtnQuiz.Click += delegate
            {
                StartActivity(typeof(QuizActivity));
            };


            mBtnToAddCategory.Click += delegate
            {
                has.saveset("id_user", null);
                Finish();
                StartActivity(typeof(MainActivity));

            };
            mBtnAddQuestion.Click += delegate
            {
                FragmentTransaction transcation = FragmentManager.BeginTransaction();
                dialogQuestion dialogQue = new dialogQuestion();
                dialogQue.Show(transcation, "dialog fragment");
                dialogQue.mOnAddQuestionCompleted += DialogQue_mOnAddQuestionCompleted;

            };

        }

            mBtnSelectCategory.Click += delegate
            {

            if (e.Name.Length > 3)
            {
                MySqlConnection con = new MySqlConnection("Server=mysql8.hekko.net.pl;database=majsterw_quiz;User=majsterw_quiz;Password=fnipSWs4;charset=utf8");
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open(); //"SELECT * FROM user where login = '$login'";   
                        MySqlCommand cmd = new MySqlCommand("SELECT name FROM categories where name = @name", con);
                        cmd.Parameters.AddWithValue("@name", e.Name);
                        MySqlDataReader Reader = cmd.ExecuteReader();

                        if (Reader.Read())
                        {
                            Toast.MakeText(this, "Podana kateogria ju¿ instnieje!", ToastLength.Long).Show();
                        }
                        else
                        {
                            Reader.Close();
                            cmd.CommandText = "INSERT INTO categories(name) VALUES(@name)";
                            cmd.ExecuteNonQuery();
                            Toast.MakeText(this, "Pomyœlnie dodano now¹ kategoriê!", ToastLength.Long).Show();
                            dialogAddCategory.Dismiss();
                        }
                        if (!Reader.IsClosed) Reader.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                Toast.MakeText(this, "Minimalna d³ugoœæ nazwy to 3 znaki", ToastLength.Short).Show();
            }
        }

        private void DialogQue_mOnAddQuestionCompleted(object sender, onAddQuestionEventArgs e)
        {
            DialogFragment dialogQuestion = sender as DialogFragment; // do zamkniecia

            if (e.Name.Length > 3)
            {
                MySqlConnection con = new MySqlConnection("Server=mysql8.hekko.net.pl;database=majsterw_quiz;User=majsterw_quiz;Password=fnipSWs4;charset=utf8");
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open(); //"SELECT * FROM user where login = '$login'";   
                        MySqlCommand cmd = new MySqlCommand("SELECT question FROM questions where question = @question", con);
                        cmd.Parameters.AddWithValue("@question", e.Name);
                        MySqlDataReader Reader = cmd.ExecuteReader();

                        if (Reader.Read())
                        {
                            Toast.MakeText(this, "Podane pytanie ju¿ instnieje!", ToastLength.Long).Show();
                        }
                        else
                        {
                            Reader.Close();
                            cmd.CommandText = "INSERT INTO questions(question) VALUES(@question)";
                            cmd.ExecuteNonQuery();
                            Toast.MakeText(this, "Pomyœlnie dodano nowe pytanie!", ToastLength.Long).Show();
                            dialogQuestion.Dismiss();
                        }
                        if (!Reader.IsClosed) Reader.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                Toast.MakeText(this, "Minimalna d³ugoœæ pytania to 3 znaki", ToastLength.Short).Show();
            }
        }
    }
}