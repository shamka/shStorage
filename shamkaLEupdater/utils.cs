using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Libraries;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Numerics;
using System.Globalization;

namespace shamkaLEupdater
{
    public enum formState
    {
        NONE,
        NEW,
        FILE
    }
    public enum formModState
    {
        UNMODIFIED,
        MODIFIED
    }
    public enum sessVer
    {
        VER1_0=1,
    }
    class utils
    {
        static public readonly string certOpen = "-----BEGIN CERTIFICATE-----";
        static public readonly string certClose = "-----END CERTIFICATE-----";
        static public readonly string keyOpen = "-----BEGIN RSA PRIVATE KEY-----";
        static public readonly string keyClose = "-----END RSA PRIVATE KEY-----";
        static public readonly string keyOpen2 = "-----BEGIN PRIVATE KEY-----";
        static public readonly string keyClose2 = "-----END PRIVATE KEY-----";
        static public readonly byte[] CName = { 0x55, 0x04, 0x03 };
        static public string getHex(IEnumerable<byte> str)
        {
            return getHex(str.ToArray());
        }
        static public string getHex(byte[] str)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                sBuilder.Append(str[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        static public void pu(int c)
        {
            Exception e = new Exception();
            e.Data.Add("sh", c);
            throw e;
        }
        static public byte[] makeSign(keyInfo key, byte[] data) {
            int len = key.bits >> 3;
            Ber signB = new Ber(BigInteger.Parse("3011300D060960864801650304020105000400", NumberStyles.AllowHexSpecifier).ToByteArray().Reverse().ToArray());
            signB.childs[1].payload = SHA256.Create().ComputeHash(data);
            byte[] messB = signB.makeDer().Reverse().Concat(new byte[] { 0 }).ToArray();
            messB = messB.Concat(Enumerable.Repeat((byte)0xff, len - (messB.Length % len) - 2)).Concat(new byte[] { 1 , 0}).ToArray();
            IEnumerable<byte> ans = new byte[] { 0 };
            byte[] block = new byte[len + 1];

            BigInteger n = new BigInteger(key.key.childs[1].payload.Reverse().ToArray());
            BigInteger d = new BigInteger(key.key.childs[3].payload.Reverse().ToArray());
            for (int i = 0; i < messB.Length; i += len) {
                Array.Copy(messB, i, block, 0, len);
                byte[] crBlock = BigInteger.ModPow(new BigInteger(block), d, n).ToByteArray();
                if (crBlock.Length < len) {
                    ans = ans.Concat(crBlock.Concat(Enumerable.Repeat((byte)0x0, len - crBlock.Length)).Reverse());
                }
                else if (crBlock.Length > len) {
                    ans = ans.Concat(crBlock.Take(len).Reverse());
                }
                else {
                    ans = ans.Concat(crBlock.Reverse());
                }
                
            }
            return ans.ToArray();
        }
        static public storageInfo openStorage(Stream file) {
            Ber data=null;
            byte[] buf = null;
            storageInfo sess = new storageInfo();
            sess.file = file;
            try
            {
                if (file.Length < 18) pu(98);
                if (file.Length > 99 * (1 << 20)) pu(99);
                try
                {
                    buf = new byte[file.Length];
                    file.Read(buf,0,(int)file.Length);
                    data = new Ber(buf);
                } catch (Exception ex) { pu(100); }

                if (data.tClass != 0x3) pu(97);
                if (data.tag != 0xB183651C18E500) pu(97);
                if (data.childs.Count < 3) pu(97);
                if (data.childs[0].tag != (UInt64)BerTags.UTF8String) pu(97);
                if (Encoding.UTF8.GetString(data.childs[0].payload) != "shStorage") pu(97);
                if (data.childs[1].tag != (UInt64)BerTags.INTEGER) pu(97);
                if (data.childs[0].tClass != 0 || data.childs[1].tClass != 0) pu(97);
                if (data.childs[1].payloadLength == 1) {
                    switch (data.childs[1].payload[0]) {
                        case 1:
                            if (data.childs.Count != 4) pu(96);
                            if (data.childs[3].container || data.childs[3].tag != (UInt64)BerTags.OCTET_STRING || data.childs[3].tClass != (byte)BerClass.PRIVATE || data.childs[3].payloadLength != 32) utils.pu(96);
                            if (!SHA256.Create().ComputeHash(data.childs[2].makeDer()).SequenceEqual(data.childs[3].payload)) pu(96);
                            storageParse.parseVER1(sess, data.childs[2]);
                            break;
                        default: pu(100); break;
                    }
                }
                else { pu(97); }

                if (data != null) data.Dispose();
                return sess;
            }
            catch (Exception ex) {
                if (data != null) data.Dispose();
                sess.Dispose();
                if (ex.Data.Contains("sh")) {
                    switch ((int)ex.Data["sh"]) {
                        case 96:
                            throw new Exception("Файл поврежден.");
                        case 97:
                            throw new Exception("Файл не является хранилищем");
                        case 98:
                            throw new Exception("Размер файла слишком мал, чтобы быть файлом программы.");
                        case 99:
                            throw new Exception("Размер файла слишком велик, чтобы быть файлом программы. Попробуте обновить программу и попробовать снова.");
                        case 100:
                            throw new Exception("Файл не поддерживается программой. Попробуте обновить программу и попробовать снова.");
                        default: throw ex;
                    }
                }
                else throw ex;
            }
        }
        static public bool saveStorage(storageInfo sess)
        {
            Ber data = new Ber(3, 0xB183651C18E500, true);
            data.UNKLength = true;
            using (data)
            {
                data.addChild(new Ber(BerClass.UNIVERSAL, BerTags.UTF8String, false, Encoding.UTF8.GetBytes("shStorage")));
                data.addChild(new Ber(BerClass.UNIVERSAL, BerTags.INTEGER, false, new byte[] { 1 }));
                data.addChild(storageParse.saveVER1(sess));
                data.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, SHA256.Create().ComputeHash(data.childs.Last().makeDer())));
                byte[] raw = data.makeDer();
                sess.file.Seek(0, SeekOrigin.Begin);
                sess.file.Write(raw, 0, raw.Length);
                sess.file.SetLength(raw.Length);
                sess.file.Flush();
            }
            return true;
        }
        static public bool saveStorage(Stream file, storageInfo sess)
        {
            sess.file = file;
            return saveStorage(sess);
        }
        static public keyInfo parsePrivKey(Stream myStream)
        {
            if (myStream.Length > 16 * 1024)
            {
                throw new Exception("Файл слишком большой..");
            }
            myStream.Seek(0, SeekOrigin.Begin);
            byte[] buff = new byte[myStream.Length];
            int buffMax = myStream.Read(buff, 0, (int)myStream.Length);
            if (buff[0] == '-')
            {
                int i = 0;
                bool rsa = true;
                for (i = 0; i < utils.keyOpen.Length; i++)
                {
                    if (buff[i] != utils.keyOpen[i])
                    {
                        rsa = false;
                        for (i = 0; i < utils.keyOpen2.Length; i++)
                        {
                            if (buff[i] != utils.keyOpen2[i]) throw new Exception("Файл не является (RSA)? PRIVATE KEY");
                        }
                        break;
                    }
                }

                while (i < myStream.Length)
                {
                    if (buff[i] == '\r' || buff[i] == '\n')
                    {
                        i++;
                        continue;
                    }
                    if (buff[i] != 'M') throw new Exception("Ключ возможно зашифрован. Выберете незашифрованный приватный ключ");
                    break;
                }
                int startBase64 = i;
                while ((i < myStream.Length) && (buff[i] != '-')) i++;
                if (rsa)
                {
                    for (int j = 0; j < utils.keyClose.Length; j++)
                    {
                        if (buff[i + j] != utils.keyClose[j]) throw new Exception("Файл не завершен как RSA PRIVATE KEY");
                    }
                }
                else
                {
                    for (int j = 0; j < utils.keyClose2.Length; j++)
                    {
                        if (buff[i + j] != utils.keyClose2[j]) throw new Exception("Файл не завершен как PRIVATE KEY");
                    }

                }
                buff = Convert.FromBase64CharArray(Encoding.UTF8.GetString(buff).ToCharArray(), startBase64, i - startBase64);
            }
            buffMax = buff.Length;
            if (buff[0] != 0x30) throw new Exception("Ошибка в ASN1 данных");
            Ber data = new Ber(buff, 0, (uint)buff.Length);
            if (data.childs.Count != 9)
            {
                try
                {
                    data = data.childs[2].childs[0];
                    if (data.childs.Count != 9) { throw new Exception(); }
                }
                catch (Exception Eex)
                {
                    throw new Exception("Не удалось обнаружить приватный ключ");
                }
            }
            try
            {
                if (data.childs[0].payloadLength != 1 || data.childs[0].tag != (ulong)BerTags.INTEGER || data.childs[0].payload[0] != 0) { throw new Exception(); }
            }
            catch (Exception Eex)
            {
                throw new Exception("Не удалось обнаружить приватный ключ");
            }
            data.deleteParrent();
            return new keyInfo(data);
        }
        static public certInfo parsePubKey(Stream myStream)
        {
            if (myStream.Length > 128 * 1024)
            {
                throw new Exception("Файл слишком большой..");
            }
            myStream.Seek(0, SeekOrigin.Begin);
            byte[] buff = new byte[myStream.Length];
            int buffMax = myStream.Read(buff, 0, (int)myStream.Length);
            if (buff[0] == '-')
            {
                int i = 0;
                for (i = 0; i < utils.certOpen.Length; i++)
                {
                    if (buff[i] != utils.certOpen[i])
                    {
                        throw new Exception("Файл не является CERTIFICATE");
                    }
                }

                while (i < myStream.Length)
                {
                    if (buff[i] == '\r' || buff[i] == '\n')
                    {
                        i++;
                        continue;
                    }
                    if (buff[i] != 'M') throw new Exception("Файл не является CERTIFICATE_");
                    break;
                }
                int startBase64 = i;
                while ((i < myStream.Length) && (buff[i] != '-')) i++;

                for (int j = 0; j < utils.certClose.Length; j++)
                {
                    if (buff[i + j] != utils.certClose[j]) throw new Exception("Файл не завершен как CERTIFICATE");
                }
                buff = Convert.FromBase64CharArray(Encoding.UTF8.GetString(buff).ToCharArray(), startBase64, i - startBase64);
            }
            buffMax = buff.Length;
            if (buff[0] != 0x30) throw new Exception("Ошибка в ASN1 данных");
            Ber data = new Ber(buff, 0, (uint)buff.Length);
            if (data.childs.Count!=3) { throw new Exception("Не удалось обнаружить сертификат"); }
            return new certInfo(data);
        }
        static public string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }
        static public object toServ(string link, string key, object data) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.AllowAutoRedirect = false;
            request.ContinueTimeout = 10000;
            request.Timeout = 30000;
            request.ReadWriteTimeout = 60000;
            request.KeepAlive = false;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.UserAgent = "C# client v1.00 shamka.ru";
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.Credentials = CredentialCache.DefaultCredentials;
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            request.ContentLength = byteArray.Length;
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            request.Headers.Add("Sh-Cur-Time", String.Format("{0:d}", unixTimestamp));
            SHA1 sha1 = SHA1.Create();
            sha1.Initialize();
            byte[] sp = new byte[3] { 0, 1, 0 };
            sha1.TransformBlock(sp, 0, 3, sp, 0);
            byte[] ut = BitConverter.GetBytes(unixTimestamp);
            sha1.TransformBlock(ut, 0, ut.Length, ut, 0);
            sha1.TransformBlock(sp, 0, 3, sp, 0);
            byte[] sk = Encoding.UTF8.GetBytes(key);
            sha1.TransformBlock(sk, 0, sk.Length, sk, 0);
            sha1.TransformBlock(sp, 0, 3, sp, 0);
            sha1.TransformFinalBlock(byteArray, 0, byteArray.Length);
            request.Headers.Add("Sh-Cur-Sign", getHex(sha1.Hash));
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            Exception exx=null;
            object raw = null;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                sha1.Initialize();
                sha1.TransformBlock(ut, 0, ut.Length, ut, 0);
                sha1.TransformBlock(sk, 0, sk.Length, sk, 0);
                sha1.TransformBlock(sp, 0, 3, sp, 0);
                byteArray = Encoding.UTF8.GetBytes((string)raw);
                sha1.TransformFinalBlock(byteArray, 0, byteArray.Length);
                string sign = response.GetResponseHeader("Sh-Cur-Sign");
                string calcSign = getHex(sha1.Hash);
                response.Close();
                if (calcSign != sign)
                {
                    throw new Exception(String.Format("Неправильная подпись сервера в ответе\nОжидание: {0:s}\nПолучено: {1:s}", calcSign, sign));
                }
                raw = JsonConvert.DeserializeObject<object>((string)raw);
                return raw;
            }
            catch (WebException www)
            {
                HttpWebResponse response = (HttpWebResponse)www.Response;
                raw = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                response.Close();
                throw new Exception("Сервер: " + ((((string)raw).Length>128)?(((string)raw).Substring(0, 128)):((string)raw)));
            }
        }
        static public byte[] makeRootCertFromPriv(string KeyName) {
            keyInfo key = State.session.keys[KeyName];
            Ber cert = new Ber(new byte[] {
0x30, 0x80,
      0x30, 0x80,
            0xA0, 0x03, 0x02, 0x01, 0x02,
            0x02, 0x00,
            0x30, 0x80,
                  0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x0B,
                  0x05, 0x00,
            0x00, 0x00,
            0x30, 0x80,
                  0x31, 0x80,
                        0x30, 0x80,
                              0x06, 0x03, 0x55, 0x04, 0x03,
                              0x0C, 0x00,
                        0x00, 0x00,
                  0x00, 0x00,
            0x00, 0x00,
            0x30, 0x80,
                  0x17, 0x00,
                  0x17, 0x00,
            0x00, 0x00,
            0x30, 0x80,
                  0x31, 0x80,
                        0x30, 0x80,
                              0x06, 0x03, 0x55, 0x04, 0x03,
                              0x0C, 0x00,
                        0x00, 0x00,
                  0x00, 0x00,
            0x00, 0x00,
            0xA3, 0x80,
                  0x30, 0x80,
                        0x30, 0x0F,
                              0x06, 0x03, 0x55, 0x1D, 0x13,
                              0x01, 0x01, 0xFF,
                              0x04, 0x05, 0x30, 0x03, 0x01, 0x01, 0xFF,
                  0x00, 0x00,
            0x00, 0x00,
      0x00, 0x00, 
      0x30, 0x80,
            0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x0B,
            0x05, 0x00,
      0x00, 0x00,
      0x03, 0x01, 0,
0x00, 0x00 });
            cert.childs[0].childs[4].childs[0].payload = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString(@"yyMMddhhmmssZ"));
            cert.childs[0].childs[4].childs[1].payload = Encoding.UTF8.GetBytes(DateTime.UtcNow.AddDays(365).ToString(@"yyMMddhhmmssZ"));
            cert.childs[0].childs[3].childs[0].childs[0].childs[1].payload = Encoding.UTF8.GetBytes(KeyName);
            cert.childs[0].childs[5].childs[0].childs[0].childs[1].payload = cert.childs[0].childs[3].childs[0].childs[0].childs[1].payload;
            cert.childs[0].childs[1].payload = cert.childs[0].childs[5].childs[0].childs[0].childs[1].payload;//BitConverter.GetBytes(DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            cert.childs[0].childs.Insert(6, key.pub);
            cert.childs[2].payload = makeSign(key,cert.childs[0].makeDer());
            return cert.makeDer();
        }
        static public string dataTo64(byte[] data,string open,string close) {
            StringBuilder str = new StringBuilder();
            str.Append(open + "\n");
            string flat = Convert.ToBase64String(data);
            int i = 0;
            for (i = 0; i < flat.Length - 64; i += 64)
            {
                str.Append(flat.Substring(i, 64) + "\n");
            }
            str.Append(flat.Substring(i, ((flat.Length & 0x3f) != 0) ? flat.Length % 64 : 64) + "\n");
            str.Append(close + "\n");
            return str.ToString();
        }
        static public string dataTo64(byte[] data, string block) {
            return dataTo64(data, "-----BEGIN " + block + "-----", "-----END " + block + "-----");
        }
        static public byte[] makeCSR(keyInfo key, string def, DomainInfo dom, System.ComponentModel.BackgroundWorker worker, bool star) {
            worker.ReportProgress(101, new object[] { -3, "Pattern parse.." });
            Ber csr = new Ber(BigInteger.Parse(
                "3042302e020100300b3109300706035504030c00a01c301a06092a864886f70d01090e310d300b30090603551d1104023000300d06092a864886f70d01010b0500030100", 
                NumberStyles.AllowHexSpecifier).ToByteArray().Reverse().ToArray());
            worker.ReportProgress(101, new object[] { -3, "OK\r\nEdit CN.." });
            if (star) {
                csr.childs[0].childs[1].childs[0].childs[0].childs[1].payload = Encoding.UTF8.GetBytes((def == "@") ? dom.dns : String.Format("{0}.{1}", def, dom.dns));
            }
            else {
                csr.childs[0].childs[1].childs[0].childs[0].childs[1].payload = Encoding.UTF8.GetBytes((def == "@") ? dom.dns : String.Format("{0}.{1}", def, dom.dns).Replace("*.",""));
            }
            Ber subs = csr.childs[0].childs[2].childs[0].childs[1].childs[0].childs[0].childs[1].childs[0];
            csr.childs[0].childs.Insert(2,key.pub.cloneAsParrent());
            worker.ReportProgress(101, new object[] { -3, "OK\r\nEdit subs.." });
            foreach (string sub in dom.subs2) {
                if (Regex.IsMatch(sub, "\\*"))
                {
                        subs.addChild(new Ber(BerClass.CONTEXT, BerTags.INTEGER, false, Encoding.UTF8.GetBytes(String.Format("{0}.{1}", sub, dom.dns))));
                        subs.addChild(new Ber(BerClass.CONTEXT, BerTags.INTEGER, false, Encoding.UTF8.GetBytes((sub == "*") ? dom.dns : String.Format("{0}.{1}", sub, dom.dns))));
                }
                else
                {
                    subs.addChild(new Ber(BerClass.CONTEXT, BerTags.INTEGER, false, Encoding.UTF8.GetBytes((sub == "@") ? dom.dns : String.Format("{0}.{1}", sub, dom.dns))));
                }
                
            };
            worker.ReportProgress(101, new object[] { -3, "OK\r\nMake sign.." });
            csr.childs[2].payload = makeSign(key, csr.childs[0].makeDer());
            worker.ReportProgress(101, new object[] { -3, "OK\r\nGet DER.." });
            return csr.makeDer();
        }
    }

    public class storageInfo : IDisposable
    {
        public Dictionary<string, keyInfo> keys;
        public Dictionary<string, DomainInfo> domains;
        public Dictionary<string, ServerInfo> servers;
        public Dictionary<string, certInfo> certs;
        private Stream _file;
        public List<string> keyPins;
        public void Dispose()
        {
            if (keyPins != null)
            {
                keyPins.Clear();
                keyPins = null;
            }
            if (_file != null) { _file.Close(); _file = null; }
            if (domains != null)
            {
                foreach (DomainInfo d in domains.Values) d.Dispose();
                domains.Clear();
                domains = null;
            }
            if (servers != null)
            {
                foreach (ServerInfo d in servers.Values) d.Dispose();
                servers.Clear();
                servers = null;
            }
            if (keys != null)
            {
                foreach (keyInfo d in keys.Values) d.Dispose();
                keys.Clear();
                keys = null;
            }
            if (certs != null)
            {
                foreach (certInfo d in certs.Values) d.Dispose();
                certs.Clear();
                certs = null;
            }
            if (LEinf != null)
            {
                LEinf.Dispose();
                LEinf = null;
            }
        }
        public Stream file {
            set {
                if (_file != null) {
                    _file.Close();
                }
                _file = value;
            }
            get { return _file; }
        }
        public LEInfo LEinf;
        public storageInfo()
        {
            keys = new Dictionary<string, keyInfo>();
            domains = new Dictionary<string, DomainInfo>();
            servers = new Dictionary<string, ServerInfo>();
            certs = new Dictionary<string, certInfo>();
            keyPins = new List<string>();
            LEinf = new LEInfo();
        }
    }
    public class keyInfo : IDisposable
    {
        public Ber key { get; }
        public Ber pub { get; }
        public int bits { get; }
        public string pinSHA256 { get; }
        public void Dispose()
        {
            if (key != null) key.Dispose();
            if (pub != null) pub.Dispose();
        }
        public keyInfo(Ber data)
        {
            key = data;
            int len = key.childs[1].payloadLength;
            bits = len * 8 - ((key.childs[1].payload[0] == 0) ? 8 : 0);
            Ber payload = new Ber(0, BerTags.SEQUENCE, true);
            payload.addChild(key.childs[1]);
            payload.addChild(key.childs[2]);
            pub = new Ber(0, BerTags.SEQUENCE, true);
            pub.addChild(new Ber(0, BerTags.SEQUENCE, true));
            pub.addChild(new Ber(0, BerTags.BIT_STRING, false)).addChild(payload);
            pub.childs.First().addChild(new Ber(0, BerTags.OBJECT_IDENTIFIER, false, new byte[] { 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01 }));
            pub.childs.First().addChild(new Ber(0, BerTags.NULL, false));
            pinSHA256 = Convert.ToBase64String(SHA256.Create().ComputeHash(pub.makeDer()));
        }
    }
    public class certInfo : IDisposable
    {
        public Ber cert { get; }
        public Ber pub { get; }
        public int bits { get; }
        public string pinSHA256 { get; }
        public string CN { get; }
        public string iCN { get; }
        public string fingerPrint { get; }
        public void Dispose()
        {
            if (cert != null) cert.Dispose();
            if (pub != null) pub.Dispose();
        }
        public certInfo(Ber data)
        {
            cert = data;
            int len = cert.childs[0].childs[6].childs[1].childs[0].childs[0].payloadLength;
            bits = len * 8 - 8 - cert.childs[0].childs[6].childs[1].childs[0].childs[0].payload[0];
            Ber payload = new Ber(0, BerTags.SEQUENCE, true);
            payload.addChild(cert.childs[0].childs[6].childs[1].childs[0].childs[0]);
            payload.addChild(cert.childs[0].childs[6].childs[1].childs[0].childs[1]);
            pub = new Ber(0, BerTags.SEQUENCE, true);
            pub.addChild(new Ber(0, BerTags.SEQUENCE, true));
            pub.addChild(new Ber(0, BerTags.BIT_STRING, false)).addChild(payload);
            pub.childs.First().addChild(new Ber(0, BerTags.OBJECT_IDENTIFIER, false, new byte[] { 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01 }));
            pub.childs.First().addChild(new Ber(0, BerTags.NULL, false));

            pinSHA256 = Convert.ToBase64String(SHA256.Create().ComputeHash(pub.makeDer()));
            fingerPrint = utils.getHex(SHA1.Create().ComputeHash(cert.makeDer()));
            payload = cert.childs[0].childs[3];
            iCN = null;
            for (int i = 0; i < payload.childs.Count; i++) {
                if (!Enumerable.SequenceEqual(payload.childs[i].childs[0].childs[0].payload,utils.CName)) continue;
                iCN = Encoding.UTF8.GetString(payload.childs[i].childs[0].childs[1].payload);
                break;
            }
            payload = cert.childs[0].childs[5];
            CN = null;
            for (int i = 0; i < payload.childs.Count; i++)
            {
                if (!Enumerable.SequenceEqual(payload.childs[i].childs[0].childs[0].payload, utils.CName)) continue;
                CN = Encoding.UTF8.GetString(payload.childs[i].childs[0].childs[1].payload);
                break;
            }
            //if (CN == iCN) {
                //BigInteger n = new BigInteger(cert.childs[0].childs[6].childs[1].childs[0].childs[0].payload.Reverse().ToArray());
                //BigInteger e = new BigInteger(cert.childs[0].childs[6].childs[1].childs[0].childs[1].payload.Reverse().ToArray());
                //BigInteger sign = new BigInteger(cert.childs[2].payload.Reverse().ToArray());

                //BigInteger ans = BigInteger.ModPow(sign, e, n);
                //string ss = utils.getHex(ans.ToByteArray().Reverse());
                //string ss2 = utils.getHex(SHA256.Create().ComputeHash(cert.childs[0].makeDer()));
            //}
        }
    }
    public class ServerInfo : IDisposable
    {
        public string link;
        public string pass;
        public ServerInfo(string _link, string _pass) {
            link = _link;
            pass = _pass;
        }

        public void Dispose()
        {

        }
    }
    public class DomainInfo : IDisposable
    {
        public string dns;
        private string _subs;
        private string[] _subs2;
        public string subs {
            get { return _subs; }
            set {
                _subs = value;
                _subs2 = _subs.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public string[] subs2 {
            get { return _subs2; }
        }
        public DomainInfo(string d, string s) {
            dns = d;
            subs = s;
        }
        public void Dispose()
        {

        }
    }
    public class LEInfo : IDisposable
    {
        public void Dispose()
        {
            
        }
        public LEInfo()
        {
            
        }
    }
    class State {

        static public string selectKeyName;
        static public string selectServerName;
        static public string selectDomainName;
        static public string selectCertName;
        static public bool ignoreSelectKeyName;
        static public bool ignoreSelectServerName;
        static public bool ignoreSelectDomainName;
        static public bool ignoreSelectCertName;

        static public storageInfo session;
        static public LE le;

        static public formState state;
        static public formModState modified;

        static public void Clear() {

            if (session != null)
            {
                session.Dispose();
                session = null;
            }
            if (le != null)
            {
                le.Dispose();
                le = null;
            }
            session = new storageInfo();
            le = new LE();
            state = formState.NONE;
            modified = formModState.UNMODIFIED;
        }
    }
}
