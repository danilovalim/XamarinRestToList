using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace XamarinRestToList
{
    [Activity(Label = "XamarinRestToList", MainLauncher = true, Icon = "@drawable/icon", Theme ="@android:style/Theme.Black.NoTitleBar")]
    public class MainActivity : Activity
    {
        private Button mButton;
        private ListView mList;
        private ProgressBar mProgress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mButton = FindViewById<Button>(Resource.Id.MyButton);
            mList = FindViewById<ListView>(Resource.Id.list);
            mProgress = FindViewById<ProgressBar>(Resource.Id.pb);

            mProgress.Visibility = ViewStates.Invisible;

            mButton.Click += MButton_Click;
        }

        private async void MButton_Click(object sender, EventArgs e)
        {
            var service = new Service();
            List<Post> postsList = new List<Post>();
            List<string> titles;
            mProgress.Visibility = ViewStates.Visible;
            mButton.Enabled = false;

            try
            {
                postsList = await service.GetPosts();

                mButton.Enabled = true;
                mProgress.Visibility = ViewStates.Invisible;
               
            }
            catch (Exception)
            {
                mProgress.Enabled = false;
                mButton.Enabled = true;
            }
            finally
            {
                if (postsList != null)
                {
                    titles = new List<string>();

                    foreach (var p in postsList)
                    {
                        titles.Add(p.title);
                    }

                    var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, titles);
                    mList.Adapter = adapter;
                }
                else
                {
                    Toast.MakeText(this, "No itens found!", ToastLength.Short).Show();
                }
            }

        }
    }
}

