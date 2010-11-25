using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class PlaceRecord
    {
        public PlaceRecord()
        {
            _recordList = new List<PlaceRecordItem>();
        }

        private List<PlaceRecordItem> _recordList;

        public void ClearRecord()
        {
            _recordList.Clear();
        }

        public void AddRecord(PlaceRecordItem recordItem)
        {
            _recordList.Add(recordItem);
        }

        public int GetLastPassCount()
        {
            if (_recordList == null) { return 0; }
            int resultCnt = 0;
            for (int i = _recordList.Count - 1; i >= 0; i--)
            {
                if (_recordList[i].PlaceAndTurnableInfo != null) { break; }
                resultCnt += 1;
            }
            return resultCnt;
        }

        public PlaceRecordItem[] GetItems()
        {
            return _recordList.ToArray();
        }

    }
}
