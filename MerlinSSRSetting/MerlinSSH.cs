using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MerlinSSRSetting
{
    class MerlinSSH
    {
        private ConnectionInfo connectionInfo = new ConnectionInfo("192.168.0.1", "zhxu",
            new PasswordAuthenticationMethod("zhxu", "APMm6cVn"));

        private SshClient sshClient;
        private ShellStream shellStream;

        private ScpClient scpClient;

        public Regex configRegex = new Regex(@"(.*""server"":.*"")(?<server>.*)("",)");

        public string ConsoleText;        

        public void Connect()
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
        }

        public void Disconnect()
        {
            if (sshClient != null && sshClient.IsConnected)
                sshClient.Disconnect();
            if (scpClient != null && scpClient.IsConnected)
                scpClient.Disconnect();
        }

        public bool IsConnected()
        {
            return sshClient != null && sshClient.IsConnected;
        }

        private void pn(string line)
        {
            ConsoleText += line.Replace("\n", "\r\n");
        }

        public string exec(string cmdline)
        {
            shellStream.WriteLine(cmdline);
            var rep = shellStream.Expect(new Regex(@"[$#]"));
            pn(rep);
            return rep;
        }

        public string scpDownload(string path)
        {
            pn("[download " + path + "]");

            var stream = new MemoryStream();
            scpClient.Download(path, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        public void scpUpload(string path, string content)
        {
            pn("[upload " + path + "]");

            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            scpClient.Upload(stream, path);
        }

        public void restartSSR()
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
    }
}
