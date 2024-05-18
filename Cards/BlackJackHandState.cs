using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public enum SpecialHandStates
    {
        blackjack,
        soft,
        broke,
        normal
    }
    internal class BlackJackHandState
    {
        public int value;
        public SpecialHandStates state;
        public BlackJackHandState(Deck deck)
        {
            value = 0;
            state = SpecialHandStates.normal;
            bool containsAce = false;
            foreach(Card card in deck.cards)
            {
                value += card.GetBlackJackValue();
                if(card.value == values.ace)
                {
                    containsAce = true;
                }
            }
            if(value > 21)
            {
                state = SpecialHandStates.broke;
                return;
            }
            if(containsAce && value + 10 <= 21)
            {
                value += 10;
                state = SpecialHandStates.soft;
                if(deck.cards.Count == 2 && value == 21)
                {
                    state = SpecialHandStates.blackjack;
                }
            }
        }
        public override string ToString()
        {
            switch (state)
            {
                case SpecialHandStates.broke:
                    return "broke";
                case SpecialHandStates.soft:
                    return "soft " + value;
                case SpecialHandStates.blackjack:
                    return "blackjack";
                default:
                    return "" + value;
            }
        }
    }
}
