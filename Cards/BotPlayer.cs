using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class BotPlayer : BlackJackPlayer
    {
        public Deck deckGuess;
        public Deck inPlay;
        public Deck discardGuess;
        public BotPlayer(Deck deck)
        {
            chips = 10000;
            inPlay = new Deck(false);
            deckGuess = new Deck(false);
            for(int i = 0;  i < deck.cards.Count; i++)
            {
                deckGuess.AddCard(deck.cards[i]);
            }
            discardGuess = new Deck(false);
            name = "bot";
            hand = new Deck(false);
        }
        public override bool WillHit(Card dealerShowing)
        {
            BlackJackHandState state = new BlackJackHandState(hand);
            //always hit if there is no downside for hitting
            if(state.value <= 11)
            {
                return true;
            }
            //never hit if there is no upside for hitting
            if(state.value >= 21)
            {
                return false;
            }
            //never hit if have 5 or more cards because that is an instant win
            if(hand.cards.Count >= 5)
            {
                return false;
            }
            Deck dealerHand = new Deck(new List<Card> { dealerShowing });
            BlackJackHandState dealerState = new BlackJackHandState(dealerHand);

            //amount of each value of card
            int[] deckCards = deckGuess.GetCardValues();
            int[] discardCards = discardGuess.GetCardValues();

            //the chance the dealer wins with the current hand
            float standWinChance = 1 - GetDealerWinChance(dealerHand, deckCards, discardCards);

            //the chance that the dealer wins if the bot draws a card
            float drawWinChance = 0f;
            int totalWeight = deckGuess.cards.Count;
            //goes through each card the bot could draw and sees the chance of winning with those, then averages them all out
            for(int i = 0; i < deckCards.Length; i++)
            {
                int amount = deckCards[i];
                if (amount == 0)
                {
                    //Console.WriteLine((i + 1) + " (" + amount + ") win chance: nope%   ");
                    continue;
                }
                deckCards[i]--;
                discardCards[i]++;
                Card card = new Card(i + 1);
                hand.AddCard(card);
                float thisWinChance = (1 - GetDealerWinChance(dealerHand, deckCards, discardCards));
                drawWinChance += thisWinChance * amount;
                hand.cards.Remove(card);
                deckCards[i]++;
                discardCards[i]--;
                //Console.WriteLine((i + 1) + " (" + amount + ") win chance: " + Math.Round(thisWinChance * 1000) / 10 + "%   ");
            }
            drawWinChance /= totalWeight;
            //Console.WriteLine("draw " + Math.Round(1000 * drawWinChance) / 10 + "%    stand " + Math.Round(1000 * standWinChance) / 10 + "%   ");
            //Console.ReadKey();
            return (drawWinChance >= standWinChance);
        }
        public override bool WillDoubleDown(Card dealerShowing)
        {
            Deck dealerHand = new Deck(new List<Card> { dealerShowing });
            BlackJackHandState dealerState = new BlackJackHandState(dealerHand);
            //amount of each value of card
            int[] deckCards = deckGuess.GetCardValues();
            int[] discardCards = discardGuess.GetCardValues();

            //the chance that the dealer wins if the bot draws a card
            float drawWinChance = 0f;
            int totalWeight = deckGuess.cards.Count;
            //goes through each card the bot could draw and sees the chance of winning with those, then averages them all out
            for (int i = 0; i < deckCards.Length; i++)
            {
                int amount = deckCards[i];
                if (amount == 0)
                {
                   // Console.WriteLine((i + 1) + " (" + amount + ") win chance: nope%   ");
                    continue;
                }
                deckCards[i]--;
                discardCards[i]++;
                Card card = new Card(i + 1);
                hand.AddCard(card);
                float thisWinChance = (1 - GetDealerWinChance(dealerHand, deckCards, discardCards));
                drawWinChance += thisWinChance * amount;
                hand.cards.Remove(card);
                deckCards[i]++;
                discardCards[i]--;
                //Console.WriteLine((i + 1) + " (" + amount + ") win chance: " + Math.Round(thisWinChance * 1000) / 10 + "%   ");
            }
            drawWinChance /= totalWeight;
            return (drawWinChance >= 0.5);
        }
        public float GetDealerWinChance(Deck dealerHand, int[] deck, int[] discard)
        {
            float winChance = 0;
            BlackJackHandState dealerState = new BlackJackHandState(dealerHand);
            BlackJackHandState handState = new BlackJackHandState(hand);
            //if both the bot and the dealer broke
            if(handState.state == SpecialHandStates.broke && dealerState.state == SpecialHandStates.broke)
            {
                return 0.5f;
            }
            //if bot broke
            if (handState.state == SpecialHandStates.broke)
            {
                return 1;
            }
            //if the dealer breaks
            if (dealerState.state == SpecialHandStates.broke)
            {
                return 0;
            }

            //if the dealer gets above 16 and doesnt want to hit it wont break
            if (dealerState.value >= 16)
            {
                if (Program.EvaluateWin(dealerHand, hand) == dealerHand)
                {
                    return 1;
                }
                else if (Program.EvaluateWin(dealerHand, hand) == null)
                {
                    return 0.5f;
                }
                return 0;
            }
            //shuffles discard into deck if needed
            if (deck.Sum() == 0)
            {
                //makes a new instance to avoid weird shared refrences
                deck = new int[10];
                for(int i = 0; i < discard.Length; i++)
                {
                    deck[i] += discard[i];
                }
                discard = new int[10];
            }
            for (int i = 0; i < deck.Length; i++)
            {
                int count = deck[i];
                if (count == 0) continue;
                Card card = new Card(i);
                dealerHand.AddCard(card);
                deck[i]--;
                discard[i]++;
                winChance += GetDealerWinChance(dealerHand, deck, discard) * count;
                dealerHand.cards.Remove(card);
                deck[i]++;
                discard[i]--;
            }
            winChance /= deck.Sum();
            return winChance;
        }
        public override void AddWin()
        {
            base.AddWin();
            Console.WriteLine(name + " won               ");
        }
        public override void NotifyDraw(Card card)
        {
            for(int i  = 0; i < deckGuess.cards.Count; i++)
            {
                Card deckCard = deckGuess.cards[i];
                if (deckCard.Equals(card))
                {
                    deckGuess.cards.RemoveAt(i);
                    inPlay.AddCard(deckCard);
                    return;
                }
            }
        }
        public override void NotifyDiscard(Card card)
        {
            inPlay.cards.Remove(card);
            discardGuess.AddCard(card);
        }
        public override void NotifyShuffle()
        {
            deckGuess.cards.AddRange(discardGuess.cards);
            discardGuess.cards.Clear();
        }
    }
}
