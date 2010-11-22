using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ReversiWpf
{
    public class SerializableDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;    //スキーマの定義はめんどくさいので省略
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(KeyValue));
            reader.Read();
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                KeyValue kv = serializer.Deserialize(reader) as KeyValue;
                if (kv != null) Add(kv.Key, kv.Value);
            }
            reader.Read();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(KeyValue));
            foreach (var key in Keys)
            {
                serializer.Serialize(writer, new KeyValue(key, this[key]));
            }
        }

        public class KeyValue
        {
            public KeyValue() { }
            public KeyValue(Tkey key, Tvalue value) { Key = key; Value = value; }
            public Tkey Key { get; set; }
            public Tvalue Value { get; set; }
        }
    }
}
