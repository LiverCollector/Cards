using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class Dealer : BlackJackPlayer
    {
        public Dealer()
        {
            name = "dealer";
            hand = new Deck(false);
        }
        public override bool WillHit()
        {
            BlackJackHandState handState = new BlackJackHandState(hand);
            return (handState.value <= 16);
        }
    }
}
