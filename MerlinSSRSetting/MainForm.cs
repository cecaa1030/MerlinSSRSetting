using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MerlinSSRSetting
{
    public partial class mainForm : Form
    {        
        private ConnectionInfo connectionInfo = new ConnectionInfo("192.168.0.1", "zhxu",
            new PasswordAuthenticationMethod("zhxu", "APMm6cVn"));

        private SshClient sshClient;
        private ShellStream shellStream;

        private ScpClient scpClient;

        private Regex configRegex = new Regex(@"(.*""server"":.*"")(?<server>.*)("",)");

        public mainForm()
        {
            InitializeComponent();
        }

        private void pn(string line)
        {
            cmdOutput.Text += line.Replace("\n","\r\n");
            cmdOutput.SelectionStart = cmdOutput.TextLength;
            cmdOutput.ScrollToCaret();
        }

        private string exec(string cmdline)
        {
            shellStream.WriteLine(cmdline);
            var rep = shellStream.Expect(new Regex(@"[$#]"));
            pn(rep);
            return rep;
        }

        private string scpDownload(string path)
        {
            pn("[download " + path + "]");

            var stream = new MemoryStream();
            scpClient.Download(path, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        private void scpUpload(string path, string content)
        {
            pn("[upload " + path + "]");

            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            scpClient.Upload(stream, path);
        }

        private void restartSSR()
        {
            var plist = exec("ps");
            var plistReg = new Regex(@"(\d+)\s*zhxu.*/opt/bin/ss-[local|redir].*\n");
            var matches = plistReg.Matches(plist);

            foreach (Match m in matches)
            {
                exec("kill " + m.Groups[1].Value);
            }

            exec("nohup /opt/bin/ss-local -c /jffs/scripts/ssrm.json -l 7913 -L 203.80.96.10:53 -u >/dev/null 2>&1 &");
            exec("nohup /opt/bin/ss-redir -c /jffs/scripts/ssrm.json -b 0.0.0.0 >/dev/null 2>&1 &");
        }

        private void getConfig()
        {
            configText.Text = scpDownload("/jffs/scripts/ssrm.json");

            serverText.Text = configRegex.Match(configText.Text).Groups["server"].Value;
        }

        private void setConfig()
        {
            serverText.Text = configRegex.Match(configText.Text).Groups["server"].Value;

            scpUpload("/jffs/scripts/ssrm.json", configText.Text);
        }

        private void updateSSRSubscribe(string serverList)
        {

        }

        private void connectBtn_Click(object sender, EventArgs e)
        {                     
            sshClient = new SshClient(connectionInfo);
            sshClient.Connect();                              
                                
            var termkvp = new Dictionary<TerminalModes, uint>();
            termkvp.Add(TerminalModes.ECHO, 53);
            shellStream = sshClient.CreateShellStream("xterm", 256, 128, 256, 128, 1024, termkvp);

            string rep = shellStream.Expect(new Regex(@"[$#]"));
            pn(rep);

            exec("ps");            

            scpClient = new ScpClient(connectionInfo);
            scpClient.Connect();

            getConfig();
        }

        private void changeBtn_Click(object sender, EventArgs e)
        {
            configText.Text = configRegex.Replace(configText.Text, "$1" + serverText.Text + "$2");

            setConfig();

            restartSSR();
        }

        private void mainForm_Closing(object sender, EventArgs e)
        {
            if (sshClient != null && sshClient.IsConnected)
                sshClient.Disconnect();
            if (scpClient != null && scpClient.IsConnected)
                scpClient.Disconnect();
        }

        private void applyConfigBtn_Click(object sender, EventArgs e)
        {
            setConfig();

            restartSSR();
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
        }

        private void serverDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sshClient != null && sshClient.IsConnected)
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
                ",  row["address"].Value.ToString(),
                    row["port"].Value.ToString(),
                    row["password"].Value.ToString(),
                    row["method"].Value.ToString(),
                    row["obfs"].Value.ToString(),
                    row["obfsparam"].Value.ToString(),
                    row["protocol"].Value.ToString(),
                    row["protocolparam"].Value.ToString());


                setConfig();

                restartSSR();
            }
        }

        private void cmdTextBox_keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                exec(cmdTextBox.Text);
                cmdTextBox.Text = "";

                e.Handled = true;
            }
        }

        private void cmdExecBtn_Click(object sender, EventArgs e)
        {
            exec(cmdTextBox.Text);
            cmdTextBox.Text = "";
        }
       
        private void updateBtn_Click(object sender, EventArgs e)
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            http.QueryString["rnd"] = Server.RandUInt32().ToString();
            http.Proxy = null;

            var serverList = Base64.DecodeBase64(http.DownloadString(@"https://npsboost.com/link/0v097PgufaO1yXzJ?mu=1"));
            File.WriteAllText("./serverList", serverList);

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

        private void serverLoadBtn_Click(object sender, EventArgs e)
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
