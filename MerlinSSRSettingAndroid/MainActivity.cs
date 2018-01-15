
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using MerlinSSRSetting;


namespace MerlinSSRSettingAndroid
{
    [Activity(Label = "MerlinSSRSettingAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private void updateSSRSubscribe(string serverList)
        {
            List<string> urls = new List<string>();
            Server.URL_Split(serverList, ref urls);
            for (int i = urls.Count - 1; i >= 0; --i)
            {
                if (!urls[i].StartsWith("ssr"))
                    urls.RemoveAt(i);
            }
            if (urls.Count > 0)
            {
                LinearLayout serverListView = FindViewById<LinearLayout>(Resource.Id.serverListView);
                LayoutInflater inflater = LayoutInflater.From(serverListView.Context);

                foreach (string url in urls)
                {
                    Server server = new Server(url, null);

                    var entityView = inflater.Inflate(Resource.Layout.ServerEntity, null);
                    entityView.FindViewById<TextView>(Resource.Id.textServerName).Text = server.remarks;
                    entityView.FindViewById<TextView>(Resource.Id.textServerAddress).Text = server.server;                    

                    serverListView.AddView(entityView);
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.myButton);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            FindViewById<Button>(Resource.Id.updateButton).Click += updateServerListButtonClick;

            var serverListFilePath = Path.Combine((string)Android.OS.Environment.ExternalStorageDirectory, "serverList");
            try
            {
                var serverList = File.ReadAllText(serverListFilePath);

                updateSSRSubscribe(serverList);
            }
            catch (Exception e)
            {
                LinearLayout serverListView = FindViewById<LinearLayout>(Resource.Id.serverListView);

                LayoutInflater inflater = LayoutInflater.From(serverListView.Context);
                var entityView = inflater.Inflate(Resource.Layout.ServerEntity, null);
                var entityServerName = entityView.FindViewById<TextView>(Resource.Id.textServerName);
                entityServerName.Text = "XXXX1";

                serverListView.AddView(entityView);

                entityView = inflater.Inflate(Resource.Layout.ServerEntity, null);
                entityServerName = entityView.FindViewById<TextView>(Resource.Id.textServerName);
                entityServerName.Text = "XXXX2";

                serverListView.AddView(entityView);
            }

        }

        private void updateServerListButtonClick(object sender, EventArgs args)
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            http.QueryString["rnd"] = Server.RandUInt32().ToString();
            http.Proxy = null;           

            var serverList = Base64.DecodeBase64(http.DownloadString(@"https://npsboost.com/link/0v097PgufaO1yXzJ?mu=1"));
            var serverListFilePath = Path.Combine((string)Android.OS.Environment.ExternalStorageDirectory, "serverList");
            File.WriteAllText(serverListFilePath, serverList);

            updateSSRSubscribe(serverList);
        }
    }
}

