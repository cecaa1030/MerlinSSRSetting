using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace MerlinSSRSetting
{
    public partial class mainForm : Form
    {
        private MerlinSSH merlinSsh;

        public mainForm()
        {
            InitializeComponent();

            merlinSsh = new MerlinSSH();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            serverDataGridView.Columns.Add("name", "name");
            serverDataGridView.Columns.Add("address", "address");
            serverDataGridView.Columns.Add("port", "port");
            serverDataGridView.Columns.Add("password", "password");
            serverDataGridView.Columns.Add("method", "method");
            serverDataGridView.Columns.Add("obfs", "obfs");
            serverDataGridView.Columns.Add("obfsparam", "obfsparam");
            serverDataGridView.Columns.Add("protocol", "protocol");
            serverDataGridView.Columns.Add("protocolparam", "protocolparam");
            serverDataGridView.Columns.Add("group", "group");

            try
            {
                var serverList = File.ReadAllText("./serverList");
                updateSSRSubscribe(serverList);
            }
            catch (Exception exception)
            {
            }
        }

        private void mainForm_Closing(object sender, EventArgs e)
        {
            merlinSsh.Disconnect();
        }

        private void getConfig()
        {
            configText.Text = merlinSsh.scpDownload("/jffs/scripts/ssrm.json");

            serverText.Text = merlinSsh.configRegex.Match(configText.Text).Groups["server"].Value;
        }

        private void setConfig()
        {
            serverText.Text = merlinSsh.configRegex.Match(configText.Text).Groups["server"].Value;

            merlinSsh.scpUpload("/jffs/scripts/ssrm.json", configText.Text);
        }

        private void updateConsole()
        {
            consoleOutput.Text = merlinSsh.ConsoleText;
            consoleOutput.SelectionStart = consoleOutput.TextLength;
            consoleOutput.ScrollToCaret();
        }

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
                serverDataGridView.Rows.Clear();

                foreach (string url in urls)
                {
                    Server server = new Server(url, null);

                    serverDataGridView.Rows.Add(new object[]
                    {
                        server.remarks,
                        server.server,
                        server.server_port,
                        server.password,
                        server.method,
                        server.obfs,
                        server.obfsparam,
                        server.protocol,
                        server.protocolparam,
                        server.group,

                    });
                }

                serverDataGridView.AutoResizeColumns();
                serverDataGridView.Sort(serverDataGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {                     
            merlinSsh.Connect();

            getConfig();

            updateConsole();
        }

        private void changeServerButton_Click(object sender, EventArgs e)
        {
            configText.Text = merlinSsh.configRegex.Replace(configText.Text, "$1" + serverText.Text + "$2");

            setConfig();

            merlinSsh.restartSSR();

            updateConsole();
        }        

        private void changeConfigButton_Click(object sender, EventArgs e)
        {
            setConfig();

            merlinSsh.restartSSR();

            updateConsole();
        }        

        private void serverDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (merlinSsh.IsConnected())
            {
                var row = serverDataGridView.Rows[e.RowIndex].Cells;

                serverText.Text = row["address"].Value.ToString();
                configText.Text = String.Format(
@"{{
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
                    row["address"].Value.ToString(),
                    row["port"].Value.ToString(),
                    row["password"].Value.ToString(),
                    row["method"].Value.ToString(),
                    row["obfs"].Value.ToString(),
                    row["obfsparam"].Value.ToString(),
                    row["protocol"].Value.ToString(),
                    row["protocolparam"].Value.ToString());


                setConfig();

                merlinSsh.restartSSR();

                updateConsole();
            }
        }

        private void cmdTextBox_keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                merlinSsh.exec(cmdTextBox.Text);                
                cmdTextBox.Text = "";

                updateConsole();

                e.Handled = true;
            }
        }

        private void cmdExecButton_Click(object sender, EventArgs e)
        {
            merlinSsh.exec(cmdTextBox.Text);
            cmdTextBox.Text = "";

            updateConsole();
        }
       
        private void updateSubscribeButton_Click(object sender, EventArgs e)
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            http.QueryString["rnd"] = Server.RandUInt32().ToString();
            http.Proxy = null;

            var serverList = Base64.DecodeBase64(http.DownloadString(@"https://npsboost.com/link/0v097PgufaO1yXzJ?mu=1"));
            File.WriteAllText("./serverList", serverList);

            updateSSRSubscribe(serverList);
        }

        private void loadLocalConfigsButton_Click(object sender, EventArgs e)
        {
            var configString = File.ReadAllText("./gui-config.json");

            var configs = JObject.Parse(configString)["configs"].Value<JArray>();

            serverDataGridView.Rows.Clear();

            foreach (var config in configs)
            {
                if (config["remarks"].Value<string>().Contains("443"))
                serverDataGridView.Rows.Add(new object[]
                    {
                        config["remarks"].Value<string>(),
                        config["server"].Value<string>(),
                        config["server_port"].Value<string>(),
                        config["password"].Value<string>(),
                        config["method"].Value<string>(),
                        config["obfs"].Value<string>(),
                        config["obfsparam"].Value<string>(),                        
                        config["protocol"].Value<string>(),
                        config["protocolparam"].Value<string>(),
                        config["group"].Value<string>(),
            
                    });                
            }
            
            serverDataGridView.AutoResizeColumn(0);
            serverDataGridView.AutoResizeColumn(1);
            
            serverDataGridView.Sort(serverDataGridView.Columns[0], ListSortDirection.Ascending);
        }
    }
}
