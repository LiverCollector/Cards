using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal abstract class BlackJackPlayer
    {
        public Deck hand;
        public string name;
        public abstract bool WillHit();
    }
}
