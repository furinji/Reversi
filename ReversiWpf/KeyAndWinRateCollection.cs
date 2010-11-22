using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ReversiWpf
{
    public class KeyAndWinRateCollection
    {
        //static KeyAndWinRateCollection()
        //{
        //    _instance = LoadFromFile();
        //    if (_instance == null)
        //    {
        //        _instance = new KeyAndWinRateCollection();
        //    }
        //}
        public KeyAndWinRateCollection()
        {
            KeyAndWinRateDic = new SerializableDictionary<int, WinRateSample>();
        }

        private static KeyAndWinRateCollection _instance;
        public static KeyAndWinRateCollection Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = LoadFromFile();
                    if (_instance == null)
                    {
                        _instance = new KeyAndWinRateCollection();
                    }
                }
                return _instance;
            }
        }

        public SerializableDictionary<int, WinRateSample> KeyAndWinRateDic { get; set; }

        public double GetWinRate(int key)
        {
            if (KeyAndWinRateDic.ContainsKey(key))
            {
                return KeyAndWinRateDic[key].WinRate;
            }
            else
            {
                return 0.5;
            }
        }

        public void SetWinRates(PlaceRecordItem[] placeRecordItems, CellState winStone)
        {
            if (winStone == CellState.None) { return; }
            foreach (var item in placeRecordItems)
            {
                if (item == null || item.PlaceAndTurnableInfo == null) { continue; }
                var key = item.PlaceAndTurnableInfo.CreateKey();
                var isWin = (item.StoneColor == winStone);
                if (KeyAndWinRateDic.ContainsKey(key) == false)
                {
                    KeyAndWinRateDic[key] = new WinRateSample(0.0, 0);
                }
                KeyAndWinRateDic[key].AddSample(isWin);
            }
        }

        public static string ValueRecordFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\ValueRecord.Xml";
            }
        }

        public static KeyAndWinRateCollection LoadFromFile()
        {
            //KeyAndWinRateCollectionForSerialze forSerial = 
            //    KeyAndWinRateCollectionForSerialze.LoadFromFile();
            //if (forSerial == null) { return null; }
            //var loadItem = forSerial.ConvertToCollection();
            //return loadItem;



            if (!File.Exists(ValueRecordFilePath)) return null;
            KeyAndWinRateCollection loadItem = null;
            XmlSerializer serializer = new XmlSerializer(typeof(KeyAndWinRateCollection));
            using (FileStream fs = new FileStream(ValueRecordFilePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    loadItem = (KeyAndWinRateCollection)serializer.Deserialize(fs);
                }
                catch
                {
                    loadItem = null;
                }
            }
            return loadItem;


            
        }

        public void SaveToFile()
        {
            //var forSerialize = KeyAndWinRateCollectionForSerialze.CreateInstance(this);
            //forSerialize.SaveToFile();

            XmlSerializer serializer = new XmlSerializer(typeof(KeyAndWinRateCollection));
            using (FileStream fs = new FileStream(ValueRecordFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

    }
}
