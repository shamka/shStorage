using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries
{
    public enum BerTags : UInt64
    {
        EOC = 0,
        BOOLEAN = 1,
        INTEGER = 2,
        BIT_STRING = 3,
        OCTET_STRING = 4,
        NULL = 5,
        OBJECT_IDENTIFIER = 6,
        OBJECT_DESCRIPTOR = 7,
        EXTERNAL = 8,
        REAL = 9,
        ENUMERATED = 10,
        EMBEDDED_PDV = 11,
        UTF8String = 12,
        RELATIVE_OID = 13,
        SEQUENCE = 16,
        SET = 17,
        NumericString = 18,
        PrintableString = 19,
        T61String = 20,
        VideotexString = 21,
        IA5String = 22,
        UTCTime = 23,
        GeneralizedTime = 24,
        GraphicString = 25,
        VisibleString = 26,
        GeneralString = 27,
        UniversalString = 28,
        CHARACTER_STRING = 29,
        BMPString = 30,
        LONGTAG = 31,
    }
    public enum BerClass : byte
    {
        UNIVERSAL = 0,
        APPLICATION = 1,
        CONTEXT = 2,
        PRIVATE = 3,
    }
    public class Ber : IDisposable
    {
        private uint totalSize;

        private byte tagClass;
        private bool _container;
        private bool mustUNKOWNlength;
        private UInt64 _tag;

        private int _payloadLength;

        private Ber _parrent;
        private byte[] _payload;
        private List<Ber> _childs;
        public List<Ber> childs
        {
            get { return _childs; }
        }
        public List<Ber> c
        {
            get { return _childs; }
        }

        public Ber getParrent()
        {
            return _parrent;
        }
        private void allSet() {
            totalSize = 0;
            tagClass = 0;
            _container = false;
            _tag = 0;
            _payloadLength = 0;
            _parrent = null;
            _childs = new List<Ber>();
            _payload = null;
            mustUNKOWNlength = false;
        }
        public Ber()
        {
            allSet();
        }
        public Ber(BerClass cl, BerTags tg, bool c)
        {
            allSet();
            if ((int)cl > 3) throw new Exception("class id must be from 0 to 3");
            tagClass = (byte)((int)cl & 3);
            _container = c;
            _tag = (ulong)tg;
        }

        public Ber(BerClass cl, BerTags tg, bool c, byte[] pl)
        {
            allSet();
            if ((int)cl > 3) throw new Exception("class id must be from 0 to 3");
            tagClass = (byte)((int)cl & 3);
            _container = c;
            _tag = (ulong)tg;
            _payload = pl;
            _payloadLength = pl.Length;
        }
        public Ber(byte cl, ulong tg, bool c)
        {
            allSet();
            if (cl > 3) throw new Exception("class id must be from 0 to 3");
            tagClass = (byte)(cl & 3);
            _container = c;
            _tag = tg;
        }
        public byte tClass
        {
            get { return tagClass; }
            set
            {
                if (value > 3) throw new Exception("class id must be from 0 to 3");
                tagClass = (byte)(value & 3);
                if (_parrent != null) _parrent.payload = null;
            }
        }
        public ulong tag {
            get{ return _tag; }
            set
            {
                _tag = value;
                if (_parrent != null) _parrent.payload = null;
            }
        }
        public bool setTag(byte cl, UInt64 tg, bool c)
        {
            tClass = cl;
            tag=tg;
            container = c;
            return true;
        }
        public void setTag(BerTags tg)
        {
            tag = (ulong)tg;
        }
        public bool container {
            get { return _container; }
            set {
                _container = value;
                for (int i = 0; i < _childs.Count; i++)
                {
                    if (_childs[i]._parrent == null) continue;
                    _childs[i].cleaner();
                }
                _childs.Clear();
                if (_parrent != null) _parrent._payload = null;
            }
        }
        public bool UNKLength {
            get { return mustUNKOWNlength; }
            set {
                if (!_container)
                {
                    mustUNKOWNlength = false;
                }
                else {
                    mustUNKOWNlength = value;
                }
            }
        }
        public void deleteParrent() {
            if (_parrent == null) return;
            Ber p = _parrent;
            _parrent = null;
            p.Dispose();
        }
        public Ber cloneAsParrent()
        {
            return new Ber(this.makeDer());
        }
        private void cleaner() {
            if (_childs == null) return;
            container = false;
            _parrent = null;
            _payload = null;
            _childs = null;
           
        }
        public void Dispose() {
            if (_parrent == null)
            {
                cleaner();
            }
            else {
                _parrent.Dispose();
            }
        }
        public Ber addChild(Ber child)
        {
            if (!container) {
                if (tClass != 0 || (_tag != 3 && _tag != 4))
                    return null;
                container = false;
            }
            if (child == null) child = new Ber();
            if (child._parrent != null)
                child = child.cloneAsParrent();
            child._parrent = this;
            _childs.Add(child);
            if (tClass == 0 && (_tag == 3 || _tag == 4)) {
                if (_tag == 3) {
                    payload = new byte[] { 0 }.Concat(child.makeDer()).ToArray();
                }
                else {
                    payload = child.makeDer();
                }
            }
            else {
                this.payload = null;
            }
            return child;
        }
        public bool delChild(Ber child)
        {
            if (!container) return false;
            if (child == null) return false;
            if (child._parrent != this) return false;
            child._parrent = null;
            _childs.Remove(child);
            child.Dispose();
            child = null;
            if (_parrent != null) _parrent.payload = null;
            return true;
        }
        public bool delAllChild()
        {
            if (!container) return false;
            container = true;
            if (_parrent != null) _parrent.payload = null;
            return true;
        }
        public byte[] payload { 
            get {
                if (_payload == null) calcPayload();
                return _payload;
            }
            set {
                if (_parrent != null) _parrent.payload = null;
                if (value == null)
                {
                    _payload = null;
                    _payloadLength = 0;
                    return;
                }
                if (_container) return;
                _payload = value;
                _payloadLength = (_payload == null) ? 0 : value.Length;
                
            }
        }
        public int payloadLength { 
            get { return _payloadLength; }
        }
        private byte[] getTagArray() {
            if (tag < 0x1F) return new byte[1]       { (byte)((tagClass<<6)|(container?(1<<5):0)|((int)tag&0x1F)) };
            if (tag < (1 << 7)) return new byte[2]   { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)tag };
            if (tag < (1 << 14)) return new byte[3]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag>>7)|0x80), (byte)(tag&0x7f) };
            if (tag < (1 << 21)) return new byte[4]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7)  | 0x80), (byte)(tag & 0x7f) };
            if (tag < (1 << 28)) return new byte[5]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7)  | 0x80), (byte)(tag & 0x7f) };
            if (tag < (1L << 35)) return new byte[6]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 28) | 0x80), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7)  | 0x80), (byte)(tag & 0x7f) };
            if (tag < (1L << 42)) return new byte[7]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 35) | 0x80), (byte)((tag >> 28) | 0x80), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7) | 0x80), (byte)(tag & 0x7f) };
            if (tag < (1L << 49)) return new byte[8]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 42) | 0x80), (byte)((tag >> 35) | 0x80), (byte)((tag >> 28) | 0x80), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7) | 0x80), (byte)(tag & 0x7f) };
            if (tag < (1L << 56)) return new byte[9]  { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 49) | 0x80), (byte)((tag >> 42) | 0x80), (byte)((tag >> 35) | 0x80), (byte)((tag >> 28) | 0x80), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7) | 0x80), (byte)(tag & 0x7f) };
            return new byte[10] { (byte)((tagClass << 6) | (container ? (1 << 5) : 0) | (0x1F)), (byte)((tag >> 56) | 0x80), (byte)((tag >> 49) | 0x80), (byte)((tag >> 49) | 0x80), (byte)((tag >> 35) | 0x80), (byte)((tag >> 28) | 0x80), (byte)((tag >> 21) | 0x80), (byte)((tag >> 14) | 0x80), (byte)((tag >> 7) | 0x80), (byte)(tag & 0x7f) };
        }
        private byte[] getLengthArray() {
            if (payloadLength < 0x80) return new byte[1] { (byte)(payloadLength) };
            if (payloadLength < (1 << 8)) return new byte[2] { 0x81, (byte)(payloadLength) };
            if (payloadLength < (1 << 16)) return new byte[3] { 0x82, (byte)(payloadLength >> 8), (byte)(payloadLength) };
            if (payloadLength < (1 << 24)) return new byte[4] { 0x83, (byte)(payloadLength >> 16), (byte)(payloadLength >> 8), (byte)(payloadLength) };
            return new byte[5] { 0x84, (byte)(payloadLength >> 24), (byte)(payloadLength >> 16), (byte)(payloadLength >> 8), (byte)(payloadLength) };
        }
        private void calcPayload() {
            IEnumerable<byte> ans = new byte[0];
            for (int i = 0; i < _childs.Count; i++) {
                ans = ans.Concat(_childs[i].makeDer());
            }
            if (_parrent != null) _parrent._payload = null;
            if (_tag == 3 && tagClass == 0) {
                _payload = new byte[] { 0 }.Concat(ans).ToArray();
            }
            else {
                _payload = ans.ToArray();
            }
            _payloadLength = _payload.Length;
        }
        public byte[] makeDer() {
            if (_container)
            {
                if (_payload == null)
                    calcPayload();
            }
            else if (_childs.Count > 0 && _payload == null) {
                calcPayload();
            }
            if (_payload == null) _payloadLength = 0;
            if (container && mustUNKOWNlength) {
                return getTagArray().Concat(new byte[1] { 0x80 }).Concat(_payload).Concat(new byte[] { 0, 0 }).ToArray();
            }
            else
            {
                if (payloadLength == 0)
                {
                    return getTagArray().Concat(new byte[1] { 0 }).ToArray();
                }
                else
                {
                    return getTagArray().Concat(getLengthArray()).Concat(_payload).ToArray();
                }
            }
        }
        private void _Ber(byte[] buff, uint offset, uint max) {
            int b;
            uint j, k = offset;
            if (((uint)buff.Length)>= (((uint)2<<30))) throw new Exception("BER:parse. large data (musb me <2GB). ERROR");
            if (max > buff.Length) throw new Exception("BER:parse. border out of memory. ERROR");
            if (offset >= max) throw new Exception("BER: parse out of border. ERROR");

            allSet();

            //parse tag
            b = buff[k++];
            totalSize++;
            tagClass = (byte)(b >> 6);
            _container = (b & 0x20) != 0;
            b &= 0x1F;
            if (b == 0x1F) {
                do
                {
                    b = buff[k++];
                    totalSize++;
                    UInt64 after = tag;
                    _tag = (_tag <<= 7) | ((ulong)b & 0x7F);
                    if(after>=_tag) throw new Exception("BER: tag ID is very big. ERROR");
                } while ((b & 0x80) != 0);
            }
            else {
                _tag = (ulong)b;
            }
            //parse length
            b = buff[k++];
            totalSize++;
            if (b < 0x80)
            {
                _payloadLength = b;
                totalSize += (uint)_payloadLength;
            }
            else if (b > 0x80) {
                b &= 0x7F;
                if(b>4) throw new Exception("BER: Length is very big. ERROR");
                while (b > 0) {
                    b--;
                    _payloadLength = (_payloadLength << 8) | (buff[k++]);
                    totalSize++;
                }
                totalSize+= (uint)_payloadLength;
            }
            else {
                if(!_container) throw new Exception("BER: Length must be set for notcontainer element. ERROR");
                _payloadLength = -1;
            }
            //parse payload
            if (_container) {
                if (_payloadLength == -1) {
                    while (k < max)
                    {
                        _childs.Add(new Ber(buff, k, max));
                        k += _childs.Last().totalSize;
                        totalSize += _childs.Last().totalSize;
                        if (_childs.Last().tag == 0 && _childs.Last().tClass==0)
                        {
                            _childs.Last().Dispose();
                            _childs.Remove(_childs.Last());
                            break;
                        };
                        _childs.Last()._parrent = this;
                    }
                    if (k > max) throw new Exception("BER: container Length ERROR");
                }
                else {
                    j = k + (uint)_payloadLength;
                    while (k < (offset + totalSize))
                    {
                        _childs.Add(new Ber(buff, k, j));
                        _childs.Last()._parrent = this;
                        k += _childs.Last().totalSize;
                    }
                    if(k > (offset + totalSize)) throw new Exception("BER: container Length ERROR");
                }

            }
            else {
                _payload = buff.Skip((int)k).Take(_payloadLength).ToArray();
                if ((tagClass == 0 ) && (tag == 4 || tag == 3))
                {
                    try
                    {
                        _childs.Add(new Ber(_payload, (uint)((tag==3)?1:0), (uint)_payload.Length));
                        _childs.First()._parrent = this;
                    }
                    catch (Exception ex) {
                        
                    }
                }
            }
        }
        public Ber(byte[] buff) {
            _Ber(buff, 0, (uint)(buff.Length));
        }
        public Ber(byte[] buff, uint offset, uint max) {
            _Ber(buff, offset, max);
        }
    }
}
