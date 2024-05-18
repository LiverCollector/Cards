using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class Color
    {
        public byte r, g, b;
        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public override string ToString()
        {
            return  "\x1b[38;2;" + r + ";" + g + ";" + b + "m";
        }
        public string ToBackgroundString()
        {
            return "\x1b[48;2;" + r + ";" + g + ";" + b + "m";
        }
    }
}
