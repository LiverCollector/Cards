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

                PrintState(dealer, player);
                while (player.WillHit())
                {
                    player.hand.AddCard(DrawCard(deck, discard));
                    PrintState(dealer, player);
                }
                dealer.hand.cards[0].faceUp = true;
                PrintState(dealer, player);
                Thread.Sleep(200);
                while (dealer.WillHit())
                {
                    dealer.hand.AddCard(DrawCard(deck, discard));
                    PrintState(dealer, player);
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
                return deck.DrawNext();
            }
            else
            {
                deck.cards.AddRange(discard.cards);
                discard.cards.Clear();
                deck.Shuffle();
                return deck.DrawNext();
            }
        }
        public static void PrintState(BlackJackPlayer dealer, BlackJackPlayer player)
        {
            Console.Clear();
            Console.WriteLine(dealer.name + "'s cards: ");
            Console.WriteLine(dealer.hand.ToASCIIString(true));
            if (dealer.hand.cards[0].faceUp)
            {
                Console.WriteLine(dealer.name + "'s value: " + new BlackJackHandState(dealer.hand));
            }
            else
            {
                Console.WriteLine(dealer.name + "'s value: unknown");
            }
            Console.WriteLine(player.name + "'s cards: ");
            Console.WriteLine(player.hand.ToASCIIString(true));
            Console.WriteLine(player.name + "'s value: " + new BlackJackHandState(player.hand));
        }
    }
}
