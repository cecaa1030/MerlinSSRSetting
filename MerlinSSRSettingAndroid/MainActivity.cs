
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
        private MerlinSSH merlinSsh;
        private TextView ssrConfigText;

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

                    entityView.FindViewById<Button>(Resource.Id.useServerButton).Click += delegate
                    {
                        if (merlinSsh.IsConnected())
                        {
                            ssrConfigText.Text = String.Format(@"{{
    ""server"": ""{0}"",
    ""server_port"": {1},
    ""local_port"": 1080,
    ""password"": ""{2}"",
    ""timeout"": 60,
    ""method"" : ""{3}"",
    ""obfs"" : ""{4}"",
    ""obfs_param"" : ""{5}"",
    ""protocol"" : ""{6}"",
    ""protocol_param"" : ""{7}"",
}}
",
                                server.server,
                                server.server_port,
                                server.password,
                                server.method,
                                server.obfs,
                                server.obfsparam,
                                server.protocol,
                                server.protocolparam);
                                                        
                            merlinSsh.scpUpload("/jffs/scripts/ssrm.json", ssrConfigText.Text);

                            merlinSsh.restartSSR();
                        }
                    };

                    serverListView.AddView(entityView);
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            merlinSsh = new MerlinSSH();

            ssrConfigText = FindViewById<TextView>(Resource.Id.ssrConfigText);
            ssrConfigText.Text = @"{
SSR Config
}";

            FindViewById<Button>(Resource.Id.updateButton).Click += updateServerListButtonClick;
            FindViewById<Button>(Resource.Id.connectButton).Click += connectButtonClick;

            var serverListFilePath =
                Path.Combine((string) Android.OS.Environment.ExternalStorageDirectory, "serverList");
            try
            {
                var serverList = File.ReadAllText(serverListFilePath);
                updateSSRSubscribe(serverList);
            }
            catch (Exception e)
            {

            }

        }

        private void updateServerListButtonClick(object sender, EventArgs args)
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            http.QueryString["rnd"] = Server.RandUInt32().ToString();
            http.Proxy = null;

            var serverList =
                Base64.DecodeBase64(http.DownloadString(@"https://npsboost.com/link/0v097PgufaO1yXzJ?mu=1"));
            var serverListFilePath =
                Path.Combine((string) Android.OS.Environment.ExternalStorageDirectory, "serverList");
            File.WriteAllText(serverListFilePath, serverList);

            updateSSRSubscribe(serverList);
        }

        private void connectButtonClick(object sender, EventArgs args)
        {
            merlinSsh.Connect();

            ssrConfigText.Text = merlinSsh.scpDownload("/jffs/scripts/ssrm.json");
        }
    }
}

