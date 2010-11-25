using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace ReversiWpf
{
    public class Option
    {
        public Option()
        {
            BackImageFileName = "Board0.png";
            BlackStoneImageFileName = "StoneBlack1.png";
            WhiteStoneImageFileName = "StoneWhite1.png";
        }

        private static Option _instance;
        public static Option Instance
        {
            get
            {
                if (_instance == null) { _instance = CreateInstance(); }
                return _instance;
            }
        }
        private static Option CreateInstance()
        {
            var instance = LoadFromFile();
            if (instance == null) { instance = new Option(); }
            return instance;
        }

        public string BackImageFileName { get; set; }
        public string BlackStoneImageFileName { get; set; }
        public string WhiteStoneImageFileName { get; set; }

        public static string OptionFilePath
        {
            get{
                return System.Windows.Forms.Application.StartupPath + "\\Option.Xml";
            }
        }

        public static Option LoadFromFile()
        {
            if (!File.Exists(OptionFilePath)) return null;
            Option loadItem = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Option));
            using (FileStream fs = new FileStream(OptionFilePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    loadItem = (Option)serializer.Deserialize(fs);
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
            XmlSerializer serializer = new XmlSerializer(typeof(Option));
            using (FileStream fs = new FileStream(OptionFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

    }
}
