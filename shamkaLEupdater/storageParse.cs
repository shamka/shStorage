using Libraries;
using System;
using System.Text;

namespace shamkaLEupdater
{
    class storageParse
    {
        static public Ber saveVER1(storageInfo sess)
        {
            int j = 77;
            Ber storage = new Ber(BerClass.UNIVERSAL, BerTags.SEQUENCE, true);
            Ber box = null;
            string[] ar = null;
            //save domains
            if (sess.domains.Count > 0)
            {
                box = storage.addChild(new Ber(3, 'D', true));
                ar = new String[sess.domains.Keys.Count];
                sess.domains.Keys.CopyTo(ar, 0);
                Array.Sort(ar, StringComparer.InvariantCulture);

                foreach (string domainName in ar)
                {
                    Ber temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(domainName)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 70)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j += 43;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(sess.domains[domainName].dns)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 73)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j -= 9;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(sess.domains[domainName].subs)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 79)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j = j * 3 - 1;
                }
                box = null;
            }
            //certs
            if (sess.certs.Count > 0)
            {
                box = storage.addChild(new Ber(3, 'C', true));
                ar = new String[sess.certs.Keys.Count];
                sess.certs.Keys.CopyTo(ar, 0);
                Array.Sort(ar, StringComparer.InvariantCulture);

                foreach (string certName in ar)
                {
                    Ber temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(certName)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 149)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j += 35;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, sess.certs[certName].cert.makeDer()));
                    for (int i = 0; i < temp.payload.Length; i++, j += 137)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j -= 15;
                }
                box = null;
            }
            //save keys
            if (sess.keys.Count > 0)
            {
                box = storage.addChild(new Ber(3, 'K', true));
                ar = new String[sess.keys.Keys.Count];
                sess.keys.Keys.CopyTo(ar, 0);
                Array.Sort(ar, StringComparer.InvariantCulture);

                foreach (string keyName in ar)
                {
                    Ber temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(keyName)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 171)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j += 31;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, sess.keys[keyName].key.makeDer()));
                    for (int i = 0; i < temp.payload.Length; i++, j += 73)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j -= 18;
                }
                box = null;
            }
            //save servers
            if (sess.servers.Count > 0)
            {
                box = storage.addChild(new Ber(3, 'S', true));
                ar = new String[sess.servers.Keys.Count];
                sess.servers.Keys.CopyTo(ar, 0);
                Array.Sort(ar, StringComparer.InvariantCulture);

                foreach (string keyName in ar)
                {
                    Ber temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(keyName)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 97)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j += 89;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(sess.servers[keyName].link)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 29)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j -= 21;
                    temp = box.addChild(new Ber(BerClass.PRIVATE, BerTags.OCTET_STRING, false, Encoding.UTF8.GetBytes(sess.servers[keyName].pass)));
                    for (int i = 0; i < temp.payload.Length; i++, j += 31)
                    {
                        temp.payload[i] ^= (byte)j;
                    }
                    j = j * 2 + 1;
                }
                box = null;
            }
            

            return storage;
        }
        static public void parseVER1(storageInfo sess, Ber box)
        {
            int j = 77;
            if (box.tag != (UInt64)BerTags.SEQUENCE || box.tClass != (byte)BerClass.UNIVERSAL || !box.container) utils.pu(100);

            for (int stType = 0; stType < box.childs.Count; stType++)
            {
                Ber box1 = box.childs[stType];
                if (box1.tClass != (byte)BerClass.PRIVATE || !box1.container) utils.pu(96);
                switch (box1.tag)
                {
                    case 67:
                        if (box1.childs.Count == 0) break;
                        if ((box1.childs.Count & 1) != 0) utils.pu(96);
                        for (int certIndex = 0; certIndex < box1.childs.Count; certIndex += 2)
                        {
                            byte[] kk = null;
                            if (box1.childs[certIndex].container || box1.childs[certIndex].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[certIndex].tClass != (byte)BerClass.PRIVATE || box1.childs[certIndex].payloadLength < 1) utils.pu(96);
                            kk = box1.childs[certIndex].payload;
                            for (int i = 0; i < kk.Length; i++, j += 149)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j += 35;
                            string certName = Encoding.UTF8.GetString(kk);
                            if (box1.childs[certIndex + 1].container || box1.childs[certIndex + 1].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[certIndex + 1].tClass != (byte)BerClass.PRIVATE || box1.childs[certIndex + 1].payloadLength < 16) utils.pu(96);
                            kk = box1.childs[certIndex + 1].payload;
                            for (int i = 0; i < kk.Length; i++, j += 137)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j -= 15;
                            Ber cert = new Ber(kk, 0, (uint)kk.Length);
                            if (cert.childs.Count != 3) utils.pu(96);
                            sess.certs.Add(certName, new certInfo(cert));
                        }
                        break;
                    case 68:
                        if (box1.childs.Count == 0) break;
                        if ((box1.childs.Count % 3) != 0) utils.pu(96);
                        for (int domainIndex = 0; domainIndex < box1.childs.Count; domainIndex += 3)
                        {
                            byte[] kk = null;
                            if (box1.childs[domainIndex].container || box1.childs[domainIndex].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[domainIndex].tClass != (byte)BerClass.PRIVATE || box1.childs[domainIndex].payloadLength < 1) utils.pu(96);
                            kk = box1.childs[domainIndex].payload;
                            for (int i = 0; i < kk.Length; i++, j += 70)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j += 43;
                            string domainName = Encoding.UTF8.GetString(kk);
                            if (box1.childs[domainIndex + 1].container || box1.childs[domainIndex + 1].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[domainIndex + 1].tClass != (byte)BerClass.PRIVATE || box1.childs[domainIndex + 2].payloadLength < 2) utils.pu(96);
                            kk = box1.childs[domainIndex + 1].payload;
                            for (int i = 0; i < kk.Length; i++, j += 73)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j -= 9;
                            string dns = Encoding.UTF8.GetString(kk);
                            if (box1.childs[domainIndex + 2].container || box1.childs[domainIndex + 2].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[domainIndex + 2].tClass != (byte)BerClass.PRIVATE || box1.childs[domainIndex + 2].payloadLength < 1) utils.pu(96);
                            kk = box1.childs[domainIndex + 2].payload;
                            for (int i = 0; i < kk.Length; i++, j += 79)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j = j * 3 - 1;
                            string subs = Encoding.UTF8.GetString(kk);

                            sess.domains.Add(domainName, new DomainInfo(dns, subs));
                        }
                        break;
                    case 75:
                        if (box1.childs.Count == 0) break;
                        if ((box1.childs.Count & 1) != 0) utils.pu(96);
                        for (int keyIndex = 0; keyIndex < box1.childs.Count; keyIndex += 2)
                        {
                            byte[] kk = null;
                            if (box1.childs[keyIndex].container || box1.childs[keyIndex].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[keyIndex].tClass != (byte)BerClass.PRIVATE || box1.childs[keyIndex].payloadLength < 1) utils.pu(96);
                            kk = box1.childs[keyIndex].payload;
                            for (int i = 0; i < kk.Length; i++, j += 171)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j += 31;
                            string keyName = Encoding.UTF8.GetString(kk);
                            if (box1.childs[keyIndex + 1].container || box1.childs[keyIndex + 1].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[keyIndex + 1].tClass != (byte)BerClass.PRIVATE || box1.childs[keyIndex + 1].payloadLength < 16) utils.pu(96);
                            kk = box1.childs[keyIndex + 1].payload;
                            for (int i = 0; i < kk.Length; i++, j += 73)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j -= 18;
                            Ber key = new Ber(kk, 0, (uint)kk.Length);
                            if (key.childs.Count != 9) utils.pu(96);
                            sess.keys.Add(keyName, new keyInfo(key));
                        }
                        break;
                    case 83:
                        if (box1.childs.Count == 0) break;
                        if ((box1.childs.Count % 3) != 0) utils.pu(96);
                        for (int keyIndex = 0; keyIndex < box1.childs.Count; keyIndex += 3)
                        {
                            byte[] kk = null;
                            if (box1.childs[keyIndex].container || box1.childs[keyIndex].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[keyIndex].tClass != (byte)BerClass.PRIVATE || box1.childs[keyIndex].payloadLength < 1) utils.pu(96);
                            kk = box1.childs[keyIndex].payload;
                            for (int i = 0; i < kk.Length; i++, j += 97)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j += 89;
                            string serverName = Encoding.UTF8.GetString(kk);
                            if (box1.childs[keyIndex + 1].container || box1.childs[keyIndex + 1].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[keyIndex + 1].tClass != (byte)BerClass.PRIVATE || box1.childs[keyIndex + 1].payloadLength < 16) utils.pu(96);
                            kk = box1.childs[keyIndex + 1].payload;
                            for (int i = 0; i < kk.Length; i++, j += 29)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j -= 21;
                            string link = Encoding.UTF8.GetString(kk);
                            if (box1.childs[keyIndex + 2].container || box1.childs[keyIndex + 2].tag != (UInt64)BerTags.OCTET_STRING || box1.childs[keyIndex + 2].tClass != (byte)BerClass.PRIVATE || box1.childs[keyIndex + 2].payloadLength < 16) utils.pu(96);
                            kk = box1.childs[keyIndex + 2].payload;
                            for (int i = 0; i < kk.Length; i++, j += 31)
                            {
                                kk[i] ^= (byte)j;
                            }
                            j = j * 2 + 1;
                            string pass = Encoding.UTF8.GetString(kk);

                            sess.servers.Add(serverName, new ServerInfo(link, pass));
                        }
                        break;
                    default: utils.pu(96); break;
                }
            }
        }
    }
}
