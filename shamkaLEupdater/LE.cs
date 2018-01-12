using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Libraries;
using System.Net;
using Newtonsoft.Json;
using System.Numerics;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace shamkaLEupdater
{
    class LE : IDisposable
    {
        private string e;
        private string n;
        private string json_jwk_raw;
        private string json_jwk;

        private static readonly string JWK_HEADERPLACE_PART1 = "{\"nonce\": \"";
        private static readonly string JWK_HEADERPLACE_PART2 = "\"alg\": \"RS256\"";//

        public static readonly string pdf = "https://letsencrypt.org/documents/LE-SA-v1.1.1-August-1-2016.pdf";
        public static readonly string pdf2 = "https://letsencrypt.org/documents/LE-SA-v1.2-November-15-2017.pdf";
        public static readonly string acme1 = "https://acme-v01.api.letsencrypt.org/directory";
        public static readonly string acme2 = "https://acme-staging-v02.api.letsencrypt.org/directory";
        public static Dictionary<string, string> acme1to2;
        private string thumbprint;
        private byte[] _csr;
        private keyInfo _key;
        public int code;
        public string details;
        private static LE me=null;
        public LE(){
            acme1to2 = new Dictionary<string, string>();
            acme1to2.Add("new-reg", "newAccount");
            acme1to2.Add("new-nonce", "newNoncet");
            acme1to2.Add("new-cert", "newOrder");
            acme1to2.Add("revoke-cert", "revokeCert");
            acme1to2.Add("key-change", "keyChange");
        }
        public static LE ME
        {
            get { return me; }
        }
        public static string th
        {
            get { return me.thumbprint; }
        }
        public void setKey(keyInfo key){
            byte[] y = null;
            if (key.pub.childs[1].childs[0].childs[0].payload[0] == 0) {
                y = key.pub.childs[1].childs[0].childs[0].payload.Skip(1).ToArray();
            }
            else {
                y = key.pub.childs[1].childs[0].childs[0].payload;
            }
            
            n = Convert.ToBase64String(y).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (key.pub.childs[1].childs[0].childs[1].payload[0] == 0)
            {
                y = key.pub.childs[1].childs[0].childs[1].payload.Skip(1).ToArray();
            }
            else
            {
                y = key.pub.childs[1].childs[0].childs[1].payload;
            }
            e = Convert.ToBase64String(y).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            json_jwk_raw = String.Format("{{\"e\":\"{0}\",\"kty\":\"RSA\",\"n\":\"{1}\"}}", e, n);
            thumbprint = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(json_jwk_raw))).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            json_jwk = "{\"alg\":\"RS256\",\"jwk\":"+ json_jwk_raw + "}";
            e = null;
            n = null;
            me = this;
            _key = key;
        }
        public void Dispose()
        {
            _csr = null;
            json_jwk_raw = null;
            json_jwk = null;
            thumbprint = null;
            me = null;
        }
        public byte[] csr {
            get { return _csr; }
            set { _csr=value; }
        }
        static public object makeReq2(string method, object data, System.ComponentModel.BackgroundWorker worker, bool isRaw) {
            if (me == null) return null;
            Dictionary<string, string> api_serv = new Dictionary<string, string>();
            
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "GET_DIR.." });
            if (worker != null) if (worker.CancellationPending)
            {
                worker.ReportProgress(101, new object[] { -3, "CANCEL" });
                return null;
            }
            JObject API = (JObject)GET(acme2 + "?cachebuster=" + utils.getHex(MD5.Create().ComputeHash(BitConverter.GetBytes(DateTime.UtcNow.ToBinary()))), api_serv, false);
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc protect.." });
            string protect = Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"nonce\":\"" + api_serv["r"] + "\"}")).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc payload.." });
            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes((data.GetType().Name != "String") ? JsonConvert.SerializeObject(data) : (string)data)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc sign.." });
            string sign = Convert.ToBase64String(utils.makeSign(me._key, Encoding.UTF8.GetBytes(protect + "." + payload))).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\nPOST_API.." });
            string url = (API[method] != null) ? API[method].ToObject<string>() : method;

            string json = "{\"header\":" + me.json_jwk +
                ",\"protected\":\"" + protect + "\"" +
                ",\"payload\":\"" + payload + "\"" +
                ",\"signature\":\"" + sign + "\"}";
            if (worker != null) if (worker.CancellationPending)
            {
                worker.ReportProgress(101, new object[] { -3, "CANCEL" });
                return null;
            }
            if (isRaw)
            {
                return POST(url, json, true);
            }
            API = (JObject)POST(url, json, false);
            if (worker != null) worker.ReportProgress(101, new object[] { -2, "OK" });
            if (API == null && me.code == 409)
            {
                worker.ReportProgress(101, new object[] { -2, "Вы уже зарегистрированы!" });
            }
            return API;
        }
        static public object makeReq(string method, object data, System.ComponentModel.BackgroundWorker worker, bool isRaw)
        {
            if (me == null) return null;
            Dictionary<string, string> api_serv = new Dictionary<string, string>();

            if (worker != null) worker.ReportProgress(101,new object[] { -3, "GET_DIR.." });
            if (worker != null) if (worker.CancellationPending)
            {
                worker.ReportProgress(101, new object[] { -3, "CANCEL" });
                return null;
            }
            JObject API = (JObject)GET(acme1 + "?cachebuster=" + utils.getHex(MD5.Create().ComputeHash(BitConverter.GetBytes(DateTime.UtcNow.ToBinary()))), api_serv, false);
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc protect.." });
            string protect = Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"nonce\":\"" + api_serv["r"] + "\"}")).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc payload.." });
            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes((data.GetType().Name != "String") ? JsonConvert.SerializeObject(data) : (string)data)).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\ncalc sign.." });
            string sign = Convert.ToBase64String(utils.makeSign(me._key,Encoding.UTF8.GetBytes(protect + "." + payload))).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            if (worker != null) worker.ReportProgress(101, new object[] { -3, "OK\r\nPOST_API.." });
            string url = (API[method] != null) ? API[method].ToObject<string>() : method;
            string json = "{\"header\":" + me.json_jwk +
                ",\"protected\":\"" + protect + "\"" +
                ",\"payload\":\"" + payload + "\"" +
                ",\"signature\":\"" + sign + "\"}";
            if (worker != null) if (worker.CancellationPending)
            {
                worker.ReportProgress(101, new object[] { -3, "CANCEL" });
                return null;
            }
            if (isRaw) {
                return POST(url, json, true);
            }
            API = (JObject)POST(url, json, false);
            if (worker != null) worker.ReportProgress(101, new object[] { -2, "OK" });
            if (API == null && me.code == 409)
            {
                worker.ReportProgress(101, new object[] { -2, "Вы уже зарегистрированы!" });
            }

            return API;
        }
        static private object POST(string url, object data, bool isRaw)
        {
            me.code = 0;
            me.details = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.ContinueTimeout = 10000;
            request.Timeout = 30000;
            request.ReadWriteTimeout = 60000;
            request.KeepAlive = false;
            if(!isRaw)request.Accept = "application/json";
            request.ContentType = "application/json";
            request.UserAgent = "C# client v1.00 shamka.ru";
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteArray = Encoding.UTF8.GetBytes((data.GetType().Name!="String")?JsonConvert.SerializeObject(data):(string)data);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                if (isRaw) {
                    using (Stream responseStream = request.GetResponse().GetResponseStream())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            int count = 0;
                            byte[] ooo = new byte[4096];
                            do
                            {
                                count = responseStream.Read(ooo, 0, ooo.Length);
                                memoryStream.Write(ooo, 0, count);

                            } while (count != 0);

                            return memoryStream.ToArray();

                        }
                    }
                }
                else
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    me.code = (int)response.StatusCode;
                    object raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                    response.Close();
                    raw = JsonConvert.DeserializeObject<object>((string)raw);
                    return raw;
                }
            }
            catch (WebException www)
            {
                if (www.Status == WebExceptionStatus.Timeout) {
                    throw new Exception("Сервер: " + www.Message);
                }
                else if (www.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response1 = (HttpWebResponse)www.Response;
                    me.code = (int)response1.StatusCode;
                    me.details = (new StreamReader(response1.GetResponseStream()).ReadToEnd());
                    response1.Close();
                    return null;
                }
                HttpWebResponse response = (HttpWebResponse)www.Response;
                me.code = (int)response.StatusCode;
                object raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                response.Close();
                throw new Exception("Сервер: " + (string)raw);
            }
        }
        static public object GET(string url, Dictionary<string, string> serv_api, bool isRaw)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.ContinueTimeout = 10000;
            request.Timeout = 30000;
            request.ReadWriteTimeout = 60000;
            request.KeepAlive = false;
            request.Accept = "application/json";
            request.UserAgent = "C# client v1.00 shamka.ru";
            request.Method = "GET";
            request.ServicePoint.Expect100Continue = false;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                me.code = (int)response.StatusCode;
                if (serv_api != null) {
                    //serv_api.Add("b", response.GetResponseHeader("Boulder-Request-Id"));
                    serv_api.Add("r", response.GetResponseHeader("Replay-Nonce"));
                }
                object raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                if (isRaw) return (byte[])raw;
                byte[] byteArray = Encoding.UTF8.GetBytes((string)raw);
                response.Close();
                raw = JsonConvert.DeserializeObject<object>((string)raw);
                return raw;
            }
            catch (WebException www)
            {
                if (www.Status == WebExceptionStatus.Timeout)
                {
                    throw new Exception("Сервер: " + www.Message);
                }
                else if (www.Status == WebExceptionStatus.ProtocolError)
                {
                    me.code = (int)((HttpWebResponse)www.Response).StatusCode;
                    return null;
                }
                HttpWebResponse response = (HttpWebResponse)www.Response;
                me.code = (int)response.StatusCode;
                object raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                response.Close();
                throw new Exception("Сервер: " + (string)raw);
            }
        }
    }
}
