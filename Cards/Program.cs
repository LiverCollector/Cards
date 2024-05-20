namespace Cards
{
    internal class Program
    {
        public static Color DEFAULT_COLOR = new Color(255, 255, 255);
        public static Color DEFAULT_BACKGROUND = new Color(0, 0, 0);
        public static Random random = new Random();
        static void Main(string[] args)
        {
            Deck deck = new Deck(true);
            deck.SetFlip(false);
            Deck discard = new Deck(false);
            deck.Shuffle();
            BlackJackPlayer player = new HumanPlayer();
            BlackJackPlayer dealer = new Dealer();

            while (true)
            {
                dealer.hand.AddCard(DrawCard(deck, discard));
                dealer.hand.cards[0].faceUp = false;
                dealer.hand.AddCard(DrawCard(deck, discard));

                player.hand.AddCard(DrawCard(deck, discard));
                player.hand.AddCard(DrawCard(deck, discard));

                PrintState(dealer, player, deck, discard);
                while (player.WillHit())
                {
                    player.hand.AddCard(DrawCard(deck, discard));
                    BlackJackHandState state = new BlackJackHandState(player.hand);
                    PrintState(dealer, player, deck, discard);
                    //if (state.state == SpecialHandStates.broke) { Thread.Sleep(300);  break; }
                }
                dealer.hand.cards[0].faceUp = true;
                PrintState(dealer, player, deck, discard);
                Thread.Sleep(200);
                while (dealer.WillHit())
                {
                    dealer.hand.AddCard(DrawCard(deck, discard));
                    PrintState(dealer, player, deck, discard);
                    Thread.Sleep(100);
                }
                BlackJackHandState dealerHand = new BlackJackHandState(dealer.hand);
                BlackJackHandState playerHand = new BlackJackHandState(player.hand);
                if(dealerHand.state == SpecialHandStates.blackjack)
                {
                    Console.WriteLine(dealer.name + " wins");
                }
                else if(playerHand.state == SpecialHandStates.blackjack)
                {
                    Console.WriteLine(player.name + " wins");
                }
                else if (dealerHand.value <= 21 && playerHand.value <= 21)
                {
                    //dealer wins on a draw
                    if(dealerHand.value >= playerHand.value)
                    {
                        Console.WriteLine(dealer.name + " wins");
                    }
                    else
                    {
                        Console.WriteLine(player.name + " wins");
                    }
                }
                else if (dealerHand.state == SpecialHandStates.broke)
                {
                    if(playerHand.state == SpecialHandStates.broke)
                    {
                        Console.WriteLine("draw");
                    }
                    else
                    {
                        Console.WriteLine(player.name + " wins");
                    }
                }
                else
                {
                    Console.WriteLine(dealer.name + " wins");
                }
                discard.cards.AddRange(player.hand.cards);
                discard.cards.AddRange(dealer.hand.cards);
                player.hand.cards = new List<Card>();
                dealer.hand.cards = new List<Card>();
                Console.ReadKey();
            }
        }
        public static Card DrawCard(Deck deck, Deck discard)
        {
            if(deck.cards.Count >= 1)
            {
                Card card = deck.DrawNext();
                card.faceUp = true;
                return card;
            }
            else
            {
                deck.cards.AddRange(discard.cards);
                discard.cards.Clear();
                deck.Shuffle();
                deck.SetFlip(false);
                return DrawCard(deck, discard);
            }
        }
        public static void PrintState(BlackJackPlayer dealer, BlackJackPlayer player, Deck deck, Deck discard)
        {
            Console.Clear();

            Console.WriteLine("   deck:     discard:");
            if (discard.cards.Count >= 1 && deck.cards.Count >= 1)
            {
                Deck display = new Deck(false);
                display.AddCard(deck.cards[deck.cards.Count - 1]);
                display.AddCard(discard.cards[discard.cards.Count - 1]);
                Console.WriteLine(display.ToASCIIString(true));
            }
            else if (deck.cards.Count >= 1)
            {
                Card card = deck.cards[deck.cards.Count - 1];
                Console.WriteLine(card.GetAsciiString());
            }
            else if (discard.cards.Count >= 1)
            {
                string card = discard.cards[discard.cards.Count - 1].GetAsciiString();
                card = card.Replace("\n|", "\n           |");
                card = card.Replace(" -", "            -");
                Console.WriteLine(card);
            }

            string state = "unknown value";
            if (dealer.hand.cards[0].faceUp)
            {
                state = new BlackJackHandState(dealer.hand).ToString();
            }
            Console.WriteLine(dealer.name + "'s cards (" + state + "): ");
            Console.WriteLine(dealer.hand.ToASCIIString(true));

            Console.WriteLine(player.name + "'s cards (" + new BlackJackHandState(player.hand) + "): ");
            Console.WriteLine(player.hand.ToASCIIString(true));
        }
    }
}
