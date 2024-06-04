using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Cards
{
    internal class Dealer : BlackJackPlayer
    {
        public Dealer()
        {
            chips = 0;
            name = "dealer";
            hand = new Deck(false);
        }
        public override bool WillHit(Card dealerShowing)
        {
            BlackJackHandState handState = new BlackJackHandState(hand);
            return (handState.value <= 16);
        }
        public override void AddWin()
        {
            base.AddWin();
            //Console.WriteLine(name + " won                    ");
        }
    }
}
