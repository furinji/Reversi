using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReversiWpf
{
    public class ClickInfo
    {
        public ClickInfo(System.Drawing.Point pos)
        {
            Position = pos;
        }

        public System.Drawing.Point Position { get; set; }
    }
}
