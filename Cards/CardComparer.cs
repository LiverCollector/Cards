using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class CardComparer : Comparer<Card>
    {
        public override int Compare(Card? x, Card? y)
        {
            if (x.faceUp && !y.faceUp) return -1;
            if (!x.faceUp && y.faceUp) return 1;
            if (x.suit > y.suit) return -1;
            if (x.suit < y.suit) return 1;
            if (x.value < y.value) return -1;
            if (x.value > y.value) return 1;
            return 0;
        }
    }
}
