using System.Security.Cryptography;

namespace Cards
{
    internal class Program
    {
        public static Color DEFAULT_COLOR = new Color(255, 255, 255);
        public static Color DEFAULT_BACKGROUND = new Color(13, 13, 13);
        public static int MIN_BET = 5;
        public static Random random = new Random();
        public static bool fairDealer = false;
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Deck deck = new Deck(1, true);
            Deck discard = new Deck(false);
            deck.Shuffle();
            //BlackJackPlayer player = new BotPlayer(deck);
            BlackJackPlayer player = new HumanPlayer();
            //BlackJackPlayer player = new SimpleBot();
            BlackJackPlayer dealer = new Dealer();
            //BlackJackPlayer dealer = new HumanPlayer();

            int gameCount = 50000;
            for(int i = 0; i < gameCount && player.chips > MIN_BET; i++)
            {
                int playerBet = player.GetBet();
                var temp = DrawCard(deck, discard);
                if(temp.Item2 == true)
                {
                    dealer.NotifyShuffle();
                    player.NotifyShuffle();
                }
                dealer.hand.AddCard(temp.Item1);
                dealer.hand.cards[0].faceUp = false;
                temp = DrawCard(deck, discard);
                if (temp.Item2 == true)
                {
                    dealer.NotifyShuffle();
                    player.NotifyShuffle();
                }
                dealer.hand.AddCard(temp.Item1);

                temp = DrawCard(deck, discard);
                if (temp.Item2 == true)
                {
                    dealer.NotifyShuffle();
                    player.NotifyShuffle();
                }
                player.hand.AddCard(temp.Item1);
                temp = DrawCard(deck, discard);
                if (temp.Item2 == true)
                {
                    dealer.NotifyShuffle();
                    player.NotifyShuffle();
                }
                player.hand.AddCard(temp.Item1);

                dealer.NotifyDraw(dealer.hand.cards[1]);
                dealer.NotifyDraw(player.hand.cards[0]);
                dealer.NotifyDraw(player.hand.cards[1]);

                player.NotifyDraw(dealer.hand.cards[1]);
                player.NotifyDraw(player.hand.cards[0]);
                player.NotifyDraw(player.hand.cards[1]);

                //if the player can double down it
                bool playerDoubledDown = false;
                if (player.hand.CanDoubleDown())
                {
                    PrintState(dealer, player, i, gameCount);
                    playerDoubledDown = player.WillDoubleDown(dealer.hand.cards[1]);
                    if (playerDoubledDown)
                    {
                        var val = DrawCard(deck, discard);
                        Card card = val.Item1;
                        if (val.Item2 == true)
                        {
                            player.NotifyShuffle();
                            dealer.NotifyShuffle();
                        }
                        player.NotifyDraw(card);
                        dealer.NotifyDraw(card);
                        player.hand.AddCard(card);
                        BlackJackHandState state = new BlackJackHandState(player.hand);
                        PrintState(dealer, player, i, gameCount);
                        if (state.state == SpecialHandStates.broke) { Thread.Sleep(300); break; }
                    }
                }

                PrintState(dealer, player, i, gameCount);
                while (player.WillHit(dealer.hand.cards[1]) && !playerDoubledDown)
                {
                    var val = DrawCard(deck, discard);
                    Card card = val.Item1;
                    if (val.Item2 == true)
                    {
                        player.NotifyShuffle();
                        dealer.NotifyShuffle();
                    }
                    player.NotifyDraw(card);
                    dealer.NotifyDraw(card);
                    player.hand.AddCard(card);
                    BlackJackHandState state = new BlackJackHandState(player.hand);
                    PrintState(dealer, player,i, gameCount);
                    if (state.state == SpecialHandStates.broke) { Thread.Sleep(300);  break; }
                }
                dealer.hand.cards[0].faceUp = true;

                dealer.NotifyDraw(dealer.hand.cards[0]);
                player.NotifyDraw(dealer.hand.cards[0]);

                PrintState(dealer, player,i,gameCount);
                //Thread.Sleep(200);
                while (dealer.WillHit(dealer.hand.cards[1]))
                {
                    var val = DrawCard(deck, discard);
                    Card card = val.Item1;
                    if (val.Item2 == true)
                    {
                        player.NotifyShuffle();
                        dealer.NotifyShuffle();
                    }
                    player.NotifyDraw(card);
                    dealer.NotifyDraw(card);
                    dealer.hand.AddCard(card);
                    PrintState(dealer, player,i, gameCount);
                    //Thread.Sleep(100);
                }

                BlackJackPlayer winner = EvaluateWin(dealer, player);
                int coef = 1;
                if (playerDoubledDown) coef = 2;
                if (winner != null)
                {
                    if (playerDoubledDown) winner.AddWin();
                    winner.AddWin();
                    if (winner == player)
                    {
                        player.chips += playerBet * coef;
                        dealer.chips -= playerBet * coef;
                        if (new BlackJackHandState(winner.hand).state == SpecialHandStates.blackjack)
                        {
                            player.chips += playerBet * coef / 2;
                            dealer.chips -= playerBet * coef/ 2;

                            if (playerDoubledDown) winner.AddWin();
                            else winner.AddHalfWin();
                        }
                    }
                    else
                    {
                        player.chips -= playerBet * coef;
                        dealer.chips += playerBet * coef;
                    }
                }

                for (int j = 0; j < player.hand.cards.Count; j++)
                {
                    Card card = player.hand.cards[j];
                    player.NotifyDiscard(card);
                    dealer.NotifyDiscard(card);
                }
                for (int j = 0; j < dealer.hand.cards.Count; j++)
                {
                    Card card = dealer.hand.cards[j];
                    player.NotifyDiscard(card);
                    dealer.NotifyDiscard(card);
                }

                discard.cards.AddRange(player.hand.cards);
                discard.cards.AddRange(dealer.hand.cards);
                PrintState(dealer, player, i, gameCount);
                player.hand.cards = new List<Card>();
                dealer.hand.cards = new List<Card>();

                //Console.Clear();
                /*
                if (i % 1 == 0)
                {
                    Console.WriteLine("deck:");
                    deck.SetFlip(true);
                    deck.Sort();
                    Console.WriteLine(deck.ToASCIIString(true));
                    deck.Shuffle();
                    Console.WriteLine("guess:");
                    player.deckGuess.Sort();
                    Console.WriteLine(player.deckGuess.ToASCIIString(true));
                    deck.SetFlip(false);

                    Console.WriteLine("discard:");
                    discard.Sort();
                    Console.WriteLine(discard.ToASCIIString(true));
                    discard.Shuffle();
                    Console.WriteLine("guess:");
                    player.discardGuess.Sort();
                    Console.WriteLine(player.discardGuess.ToASCIIString(true));
                    Console.ReadKey();
                    Console.Clear();
                }
                //*/

                //Thread.Sleep(100);
                //Console.ReadKey();
            }
        }
        public static BlackJackPlayer EvaluateWin(BlackJackPlayer dealer, BlackJackPlayer player)
        {
            Deck deck = EvaluateWin(dealer.hand, player.hand);
            if(deck == null)
            {
                return null;
            }
            else if (deck == dealer.hand)
            {
                return dealer;
            }
            else
            {
                return player;
            }
        }
        public static Deck EvaluateWin(Deck dealer, Deck player)
        {
            if (fairDealer)
            {
                BlackJackHandState dealerHand = new BlackJackHandState(dealer);
                BlackJackHandState playerHand = new BlackJackHandState(player);
                if (playerHand.state != SpecialHandStates.broke && player.cards.Count >= 5)
                {
                    return player;
                }
                if (dealerHand.state != SpecialHandStates.broke && dealer.cards.Count >= 5)
                {
                    return dealer;
                }
                if (dealerHand.state == SpecialHandStates.blackjack && playerHand.state == SpecialHandStates.blackjack)
                {
                    //doesn't give the dealer the win when both have blackjack
                    return null;
                }
                if (dealerHand.state == SpecialHandStates.blackjack)
                {
                    return dealer;
                }
                else if (playerHand.state == SpecialHandStates.blackjack)
                {
                    return player;
                }
                else if (dealerHand.value <= 21 && playerHand.value <= 21)
                {
                    if (dealerHand.value > playerHand.value)
                    {
                        return dealer;
                    }
                    else if (dealerHand.value < playerHand.value)
                    {
                        return player;
                    }
                    return null;
                }
                else if (dealerHand.state == SpecialHandStates.broke)
                {
                    if (playerHand.state == SpecialHandStates.broke)
                    {
                        //doesn't give the dealer a win when both break
                        return null;
                    }
                    else
                    {
                        return player;
                    }
                }
                else
                {
                    return dealer;
                }
            }
            else
            {
                BlackJackHandState dealerHand = new BlackJackHandState(dealer);
                BlackJackHandState playerHand = new BlackJackHandState(player);
                if (playerHand.state != SpecialHandStates.broke && player.cards.Count >= 5)
                {
                    return player;
                }
                if (dealerHand.state != SpecialHandStates.broke && dealer.cards.Count >= 5)
                {
                    return dealer;
                }
                if (dealerHand.state == SpecialHandStates.blackjack && playerHand.state == SpecialHandStates.blackjack)
                {
                    //return null;
                }
                if (dealerHand.state == SpecialHandStates.blackjack)
                {
                    return dealer;
                }
                else if (playerHand.state == SpecialHandStates.blackjack)
                {
                    return player;
                }
                else if (dealerHand.value <= 21 && playerHand.value <= 21)
                {
                    if (dealerHand.value > playerHand.value)
                    {
                        return dealer;
                    }
                    else if (dealerHand.value < playerHand.value)
                    {
                        return player;
                    }
                    return null;
                }
                else if (dealerHand.state == SpecialHandStates.broke)
                {
                    if (playerHand.state == SpecialHandStates.broke)
                    {
                        return dealer;
                    }
                    else
                    {
                        return player;
                    }
                }
                else
                {
                    return dealer;
                }
            }
        }
        public static (Card,bool) DrawCard(Deck deck, Deck discard)
        {
            if(deck.cards.Count >= 1)
            {
                Card card = deck.DrawNext();
                card.faceUp = true;
                return (card,false);
            }
            else
            {
                deck.cards.AddRange(discard.cards);
                discard.cards.Clear();
                deck.Shuffle();
                deck.SetFlip(false);
                Card card = deck.DrawNext();
                card.faceUp = true;
                return (card, true);
            }
        }
        public static void PrintState(BlackJackPlayer dealer, BlackJackPlayer player, int gameNumber,  int gameCount)
        {
            if(gameNumber % 100 != 0)
            {
                //return;
            }
            string defaultColor = DEFAULT_COLOR.ToString() + DEFAULT_BACKGROUND.ToBackgroundString();
            Console.SetCursorPosition(0, 0);
            //Console.Clear();
            string output = "";
            /*
            output += "   deck:     discard:\n";
            if (discard.cards.Count >= 1 && deck.cards.Count >= 1)
            {
                Deck display = new Deck(false);
                display.AddCard(deck.cards[deck.cards.Count - 1]);
                display.AddCard(discard.cards[discard.cards.Count - 1]);
                output += display.ToASCIIString(true) + "\n";
            }
            else if (deck.cards.Count >= 1)
            {
                Card card = deck.cards[deck.cards.Count - 1];
                output += card.GetAsciiString();
            }
            else if (discard.cards.Count >= 1)
            {
                string card = discard.cards[discard.cards.Count - 1].GetAsciiString();
                card = card.Replace("\n|", "\n           |");
                card = card.Replace(" -", "            -");
                output += card;
            }
            //*/
            output += "totalGames: " + gameNumber + " " + Math.Round((float)gameNumber / gameCount * 1000) / 10 + "%\n";
            string state = "unknown value";
            if (dealer.hand.cards.Count != 0 && dealer.hand.cards[0].faceUp)
            {
                state = new BlackJackHandState(dealer.hand).ToString();
            }
            output += "(wins: " + dealer.wins + ") " + dealer.name + "'s cards (" + state + "): \n";
            output += "dealer has ";
            if (dealer.chips < 0) output += "lost ";
            else output += "gained ";
            output += Math.Abs(dealer.chips) + " chips\n";
            output += dealer.hand.ToASCIIString(true) + "\n";
            float percent = ((float)player.wins / dealer.wins) - 1;
            string betterWorse;
            if(percent > 0) betterWorse = "better";
            else betterWorse = "worse";
            percent = (float)Math.Round(Math.Abs(percent) * 10000) / 100;
            output += "(wins: " + player.wins + ") (" + Math.Round((float)player.wins / gameNumber * 1000) / 10 + "% win, " +
                Math.Round((float)(gameNumber - (dealer.wins + player.wins)) / gameNumber * 1000) / 10 + "% tie, " +
                Math.Round((float)dealer.wins / gameNumber * 1000) / 10 + "% loss (" +
                percent + "% " + betterWorse + " than the dealer))";
            output += player.name + "'s cards (" + new BlackJackHandState(player.hand) + "): \n";
            output += "player has " + player.chips + " chips\n";
            output += player.hand.ToASCIIString(true) + "\n";
            Color c = DEFAULT_BACKGROUND;
            output = output.Replace("\n", c.ToString() + c.ToBackgroundString() +  "                                                 \n" + defaultColor);
            Console.WriteLine(output);
        }
    }
}
