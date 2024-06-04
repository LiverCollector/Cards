using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class SimpleBot : BlackJackPlayer
    {
        //strategy from https://bicyclecards.com/how-to-play/blackjack/
        public SimpleBot()
        {
            name = "simple bot";
            chips = 10000;
            hand = new Deck(false);
        }
        public override bool WillHit(Card dealerShowing)
        {
            BlackJackHandState myHand = new BlackJackHandState(hand);
            //With a soft hand, the general strategy is to keep hitting until a total of at least 18 is reached. Thus, with an ace and a six (7 or 17), the player would not stop at 17, but would hit.
            if (myHand.state == SpecialHandStates.soft)
            {
                return (myHand.value < 17);
            }
            //When the dealer's upcard is a good one, a 7, 8, 9, 10-card, or ace for example, the player should not stop drawing until a total of 17 or more is reached. 
            if (dealerShowing.value == values.ace || (int)dealerShowing.value > 7)
            {
                return (myHand.value < 17);
            }
            //When the dealer's upcard is a poor one, 4, 5, or 6, the player should stop drawing as soon as he gets a total of 12 or higher.
            //The strategy here is never to take a card if there is any chance of going bust.
            if ((int)dealerShowing.value > 4)
            {
                return (myHand.value < 12);
            }
            //when the dealer's up card is a fair one, 2 or 3, the player should stop with a total of 13 or higher.
            else
            {
                return (myHand.value < 13);
            }
        }
        public override bool WillDoubleDown(Card dealerShowing)
        {
            BlackJackHandState myHand = new BlackJackHandState(hand);
            int dealerValue = dealerShowing.GetBlackJackValue();
            //The basic strategy for doubling down is as follows: With a total of 11, the player should always double down.
            if(myHand.value == 11)
            {
                return true;
            }
            //With a total of 10, he should double down unless the dealer shows a ten-card or an ace.
            if(myHand.value == 10)
            {
                if(dealerShowing.value == values.ace || dealerValue == 10)
                {
                    return false;
                }
                return true;
            }
            //With a total of 9, the player should double down only if the dealer's card is fair or poor (2 through 6).
            else
            {
                if(dealerValue >= 2 && dealerValue <= 6)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
