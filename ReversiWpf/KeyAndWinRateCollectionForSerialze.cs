using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace ReversiWpf
{
    //public class KeyAndWinRateCollectionForSerialze
    //{
    //    public KeyAndWinRateCollectionForSerialze()
    //    {
    //        //KeyAndWinRateList = new KeyAndWinRate[]();
    //    }

    //    public List<KeyAndWinRate> KeyAndWinRateList { get; set; }

    //    public static KeyAndWinRateCollectionForSerialze CreateInstance(
    //        KeyAndWinRateCollection keyAndWinCollection)
    //    {
    //        var resultList = new List<KeyAndWinRate>();
    //        var result = new KeyAndWinRateCollectionForSerialze();
    //        foreach (var key in keyAndWinCollection.KeyAndWinRateDic.Keys)
    //        {
    //            var val = keyAndWinCollection.KeyAndWinRateDic[key];
    //            resultList.Add(
    //                new KeyAndWinRate(key, val));
    //        }
    //        result.KeyAndWinRateList = resultList;
    //        return result;
    //    }

    //    public KeyAndWinRateCollection ConvertToCollection()
    //    {
    //        var result = new KeyAndWinRateCollection();
    //        foreach (var item in KeyAndWinRateList)
    //        {
    //            result.KeyAndWinRateDic[item.Key] = item.WinRate;
    //        }
    //        return result;
    //    }

    //    public static string ValueRecordFilePath
    //    {
    //        get
    //        {
    //            return System.Windows.Forms.Application.StartupPath + "\\ValueRecord.Xml";
    //        }
    //    }

    //    public static KeyAndWinRateCollectionForSerialze LoadFromFile()
    //    {
    //        if (!File.Exists(ValueRecordFilePath)) return null;
    //        KeyAndWinRateCollectionForSerialze loadItem = null;
    //        XmlSerializer serializer = new XmlSerializer(typeof(KeyAndWinRateCollectionForSerialze));
    //        using (FileStream fs = new FileStream(ValueRecordFilePath, FileMode.Open, FileAccess.Read))
    //        {
    //            try
    //            {
    //                loadItem = (KeyAndWinRateCollectionForSerialze)serializer.Deserialize(fs);
    //            }
    //            catch
    //            {
    //                loadItem = null;
    //            }
    //        }
    //        return loadItem;
    //    }

    //    public void SaveToFile()
    //    {
    //        XmlSerializer serializer = new XmlSerializer(typeof(KeyAndWinRateCollectionForSerialze));
    //        using (FileStream fs = new FileStream(ValueRecordFilePath, FileMode.Create))
    //        {
    //            serializer.Serialize(fs, this);
    //        }
    //    }


       

    //}
}
