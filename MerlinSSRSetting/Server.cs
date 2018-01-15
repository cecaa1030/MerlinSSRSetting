using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MerlinSSRSetting
{
    public class Server
    {
        public string id;
        public string server;
        public int server_port;
        public int server_udp_port;
        public string password;
        public string method;
        public string protocol;
        public string protocolparam;
        public string obfs;
        public string obfsparam;
        public string remarks_base64;
        public string group;
        public bool enable;
        public bool udp_over_tcp;

        private object protocoldata;
        private object obfsdata;

        private static Server forwardServer = new Server();

        public void CopyServer(Server Server)
        {
            protocoldata = Server.protocoldata;
            obfsdata = Server.obfsdata;
            enable = Server.enable;
        }

        public void CopyServerInfo(Server Server)
        {
            remarks = Server.remarks;
            group = Server.group;
        }

        public static Server GetForwardServerRef()
        {
            return forwardServer;
        }

        public string remarks
        {
            get
            {
                if (remarks_base64.Length == 0)
                {
                    return string.Empty;
                }
                try
                {
                    return Base64.DecodeUrlSafeBase64(remarks_base64);
                }
                catch (FormatException)
                {
                    var old = remarks_base64;
                    remarks = remarks_base64;
                    return old;
                }
            }
            set
            {
                remarks_base64 = Base64.EncodeUrlSafeBase64(value);
            }
        }

        public Server Clone()
        {
            Server ret = new Server();
            ret.server = server;
            ret.server_port = server_port;
            ret.password = password;
            ret.method = method;
            ret.protocol = protocol;
            ret.obfs = obfs;
            ret.obfsparam = obfsparam ?? "";
            ret.remarks_base64 = remarks_base64;
            ret.group = group;
            ret.enable = enable;
            ret.udp_over_tcp = udp_over_tcp;
            ret.id = id;
            ret.protocoldata = protocoldata;
            ret.obfsdata = obfsdata;
            return ret;
        }

        public static void RandBytes(byte[] buf, int length)
        {
            byte[] temp = new byte[length];
            RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider();
            rngServiceProvider.GetBytes(temp);
            temp.CopyTo(buf, 0);
        }

        public static UInt32 RandUInt32()
        {
            byte[] temp = new byte[4];
            RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider();
            rngServiceProvider.GetBytes(temp);
            return BitConverter.ToUInt32(temp, 0);
        }

        public static void URL_Split(string text, ref List<string> out_urls)
        {
            if (String.IsNullOrEmpty(text))
            {
                return;
            }
            int ss_index = text.IndexOf("ss://", 1, StringComparison.OrdinalIgnoreCase);
            int ssr_index = text.IndexOf("ssr://", 1, StringComparison.OrdinalIgnoreCase);
            int index = ss_index;
            if (index == -1 || index > ssr_index && ssr_index != -1) index = ssr_index;
            if (index == -1)
            {
                out_urls.Insert(0, text);
            }
            else
            {
                out_urls.Insert(0, text.Substring(0, index));
                URL_Split(text.Substring(index), ref out_urls);
            }
        }

        public Server()
        {
            server = "server host";
            server_port = 8388;
            method = "aes-256-cfb";
            protocol = "origin";
            protocolparam = "";
            obfs = "plain";
            obfsparam = "";
            password = "0";
            remarks_base64 = "";
            group = "FreeSSR-public";
            udp_over_tcp = false;
            enable = true;
            byte[] id = new byte[16];
            RandBytes(id, id.Length);
            this.id = BitConverter.ToString(id).Replace("-", "");
        }

        public Server(string ssURL, string force_group) : this()
        {
            if (ssURL.StartsWith("ss://", StringComparison.OrdinalIgnoreCase))
            {
                ServerFromSS(ssURL, force_group);
            }
            else if (ssURL.StartsWith("ssr://", StringComparison.OrdinalIgnoreCase))
            {
                ServerFromSSR(ssURL, force_group);
            }
            else
            {
                throw new FormatException();
            }
        }

        public bool isMatchServer(Server server)
        {
            if (this.server == server.server
                && server_port == server.server_port
                && server_udp_port == server.server_udp_port
                && method == server.method
                && protocol == server.protocol
                && protocolparam == server.protocolparam
                && obfs == server.obfs
                && obfsparam == server.obfsparam
                && password == server.password
                && udp_over_tcp == server.udp_over_tcp
                )
                return true;
            return false;
        }

        private Dictionary<string, string> ParseParam(string param_str)
        {
            Dictionary<string, string> params_dict = new Dictionary<string, string>();
            string[] obfs_params = param_str.Split('&');
            foreach (string p in obfs_params)
            {
                if (p.IndexOf('=') > 0)
                {
                    int index = p.IndexOf('=');
                    string key, val;
                    key = p.Substring(0, index);
                    val = p.Substring(index + 1);
                    params_dict[key] = val;
                }
            }
            return params_dict;
        }

        public void ServerFromSSR(string ssrURL, string force_group)
        {
            // ssr://host:port:protocol:method:obfs:base64pass/?obfsparam=base64&remarks=base64&group=base64&udpport=0&uot=1
            Match ssr = Regex.Match(ssrURL, "ssr://([A-Za-z0-9_-]+)", RegexOptions.IgnoreCase);
            if (!ssr.Success)
                throw new FormatException();

            string data = Base64.DecodeUrlSafeBase64(ssr.Groups[1].Value);
            Dictionary<string, string> params_dict = new Dictionary<string, string>();

            Match match = null;

            int param_start_pos = data.IndexOf("?");
            if (param_start_pos > 0)
            {
                params_dict = ParseParam(data.Substring(param_start_pos + 1));
                data = data.Substring(0, param_start_pos);
            }
            if (data.IndexOf("/") >= 0)
            {
                data = data.Substring(0, data.LastIndexOf("/"));
            }

            Regex UrlFinder = new Regex("^(.+):([^:]+):([^:]*):([^:]+):([^:]*):([^:]+)");
            match = UrlFinder.Match(data);

            if (match == null || !match.Success)
                throw new FormatException();

            server = match.Groups[1].Value;
            server_port = int.Parse(match.Groups[2].Value);
            protocol = match.Groups[3].Value.Length == 0 ? "origin" : match.Groups[3].Value;
            protocol = protocol.Replace("_compatible", "");
            method = match.Groups[4].Value;
            obfs = match.Groups[5].Value.Length == 0 ? "plain" : match.Groups[5].Value;
            obfs = obfs.Replace("_compatible", "");
            password = Base64.DecodeStandardSSRUrlSafeBase64(match.Groups[6].Value);

            if (params_dict.ContainsKey("protoparam"))
            {
                protocolparam = Base64.DecodeStandardSSRUrlSafeBase64(params_dict["protoparam"]);
            }
            if (params_dict.ContainsKey("obfsparam"))
            {
                obfsparam = Base64.DecodeStandardSSRUrlSafeBase64(params_dict["obfsparam"]);
            }
            if (params_dict.ContainsKey("remarks"))
            {
                remarks = Base64.DecodeStandardSSRUrlSafeBase64(params_dict["remarks"]);
            }
            if (params_dict.ContainsKey("group"))
            {
                group = Base64.DecodeStandardSSRUrlSafeBase64(params_dict["group"]);
            }
            else
                group = "";
            if (params_dict.ContainsKey("uot"))
            {
                udp_over_tcp = int.Parse(params_dict["uot"]) != 0;
            }
            if (params_dict.ContainsKey("udpport"))
            {
                server_udp_port = int.Parse(params_dict["udpport"]);
            }
            if (!String.IsNullOrEmpty(force_group))
                group = force_group;
        }

        public void ServerFromSS(string ssURL, string force_group)
        {
            Regex UrlFinder = new Regex("^(?i)ss://([A-Za-z0-9+-/=_]+)(#(.+))?", RegexOptions.IgnoreCase),
                DetailsParser = new Regex("^((?<method>.+):(?<password>.*)@(?<hostname>.+?)" +
                                      ":(?<port>\\d+?))$", RegexOptions.IgnoreCase);

            var match = UrlFinder.Match(ssURL);
            if (!match.Success)
                throw new FormatException();

            var base64 = match.Groups[1].Value;
            match = DetailsParser.Match(Encoding.UTF8.GetString(Convert.FromBase64String(
                base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '='))));
            protocol = "origin";
            method = match.Groups["method"].Value;
            password = match.Groups["password"].Value;
            server = match.Groups["hostname"].Value;
            server_port = int.Parse(match.Groups["port"].Value);
            if (!String.IsNullOrEmpty(force_group))
                group = force_group;
            else
                group = "";
        }

        public string GetSSLinkForServer()
        {
            string parts = method + ":" + password + "@" + server + ":" + server_port;
            string base64 = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(parts)).Replace("=", "");
            return "ss://" + base64;
        }

        public string GetSSRLinkForServer()
        {
            string main_part = server + ":" + server_port + ":" + protocol + ":" + method + ":" + obfs + ":" + Base64.EncodeUrlSafeBase64(password);
            string param_str = "obfsparam=" + Base64.EncodeUrlSafeBase64(obfsparam ?? "");
            if (!string.IsNullOrEmpty(protocolparam))
            {
                param_str += "&protoparam=" + Base64.EncodeUrlSafeBase64(protocolparam);
            }
            if (!string.IsNullOrEmpty(remarks))
            {
                param_str += "&remarks=" + Base64.EncodeUrlSafeBase64(remarks);
            }
            if (!string.IsNullOrEmpty(group))
            {
                param_str += "&group=" + Base64.EncodeUrlSafeBase64(group);
            }
            if (udp_over_tcp)
            {
                param_str += "&uot=" + "1";
            }
            if (server_udp_port > 0)
            {
                param_str += "&udpport=" + server_udp_port.ToString();
            }
            string base64 = Base64.EncodeUrlSafeBase64(main_part + "/?" + param_str);
            return "ssr://" + base64;
        }

        public bool isEnable()
        {
            return enable;
        }

        public void setEnable(bool enable)
        {
            this.enable = enable;
        }

        public object getObfsData()
        {
            return this.obfsdata;
        }
        public void setObfsData(object data)
        {
            this.obfsdata = data;
        }

        public object getProtocolData()
        {
            return this.protocoldata;
        }
        public void setProtocolData(object data)
        {
            this.protocoldata = data;
        }
    }

}
