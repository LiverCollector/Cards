using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class Deck
    {
        public List<Card> cards;
        public Deck(bool full)
        {
            cards = new List<Card>();
            if (full)
            {
                for (int i = 0; i <= (int)suits.spades; i++)
                {
                    for (int j = 0; j <= (int)values.king; j++)
                    {
                        cards.Add(new Card((suits)i, (values)j));
                    }
                }
            }
        }
        public Deck(List<Card> cards)
        {
            this.cards = cards;
        }
        public Deck(List<Deck> decks)
        {
            cards = new List<Card>();
            for (int i = 0; i < decks.Count; i++)
            {
                cards.AddRange(decks[i].cards);
            }
        }
        public Deck(int deckCount, bool shuffled)
        {
            cards = new List<Card>();
            for (int i = 0; i < deckCount; i++)
            {
                for (int j = 0; j <= (int)suits.spades; j++)
                {
                    for (int k = 0; k <= (int)values.king; k++)
                    {
                        cards.Add(new Card((suits)j, (values)k));
                    }
                }
            }
            if (shuffled)
            {
                Shuffle();
            }
        }
        public void Shuffle()
        {
            Random r = Program.random;
            for (int i = 1; i < cards.Count; i++)
            {
                int randi = r.Next(cards.Count - i);
                Card temp = cards[cards.Count - i];
                cards[cards.Count - i] = cards[randi];
                cards[randi] = temp;
            }
        }
        public void Sort()
        {
            Comparer<Card> comparer = new CardComparer();
            cards.Sort(comparer);
        }
        public int[] GetCardValues()
        {
            int[] output = new int[10];
            for(int i = 0; i < cards.Count; i++)
            {
                output[cards[i].GetBlackJackValue() - 1]++;
            }
            return output;
        }
        public Card DrawNext()
        {
            if (cards.Count == 0)
            {
                throw new Exception("no cards in deck");
            }
            Card card = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void SetFlip(bool faceUp)
        {
            foreach (Card card in cards)
            {
                card.faceUp = faceUp;
            }
        }
        public bool CanDoubleDown()
        {
            if (cards.Count != 2) return false;
            BlackJackHandState state = new BlackJackHandState(this);
            if (state.value == 9 || state.value == 10 || state.value == 11) return true;
            return false;
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < cards.Count; i++)
            {
                output += cards[i].ToString() + "\n";
            }
            return output;
        }
        public string ToShortString()
        {
            string output = "";
            for (int i = 0; i < cards.Count; i++)
            {
                output += cards[i].ToShortString() + "\n";
            }
            return output;
        }
        public string ToASCIIString(bool horizontal)
        {
            string output = "";
            if (horizontal)
            {
                if (cards.Count >= 1)
                {
                    string[] aggregated = cards[0].GetAsciiString().Split("\n");
                    for (int i = 1; i < cards.Count; i++)
                    {
                        string[] nextCard = cards[i].GetAsciiString().Split("\n");
                        for (int j = 0; j < nextCard.Length; j++)
                        {
                            aggregated[j] += nextCard[j];
                        }
                        if ((i + 1) % 13 == 0 && i < cards.Count - 1)
                        {
                            for (int j = 0; j < aggregated.Length - 1; j++)
                            {
                                output += aggregated[j] + "\n";
                                aggregated[j] = "";
                            }
                        }
                    }
                    for (int i = 0; i < aggregated.Length - 1; i++)
                    {
                        output += aggregated[i];
                        aggregated[i] = "";
                        if (i != aggregated.Length - 2)
                        {
                            output += "\n";
                        }
                    }
                    return output;
                }
            }
            for (int i = 0; i < cards.Count; i++)
            {
                output += cards[i].GetAsciiString() + "\n";
            }
            return output;
        }
    }
}
