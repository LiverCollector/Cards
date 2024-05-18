namespace cardShuffler
{
    enum suits { clubs, diamonds, hearts, spades }
    enum values { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
    internal class card
    {
        public suits suit { get; set; }
        public values value { get; set; }
        public string name { get; set; }

        //constructors
        public card(suits suit, values value)
        {
            char[] suitChars = new char[] { '♣', '♦', '♥', '♠' };
            this.suit = suit;
            this.value = value;
            this.name = "the " + value + " of " + suitChars[(int)suit]; //could probably do override toString instead but the project requires name so I didn't
        }

        public void Print()
        {
            char[] suitChars = new char[] { '♣', '♦', '♥', '♠' };
            Console.Write("the " + value + " of ");
            switch (suit)
            {
                case suits.clubs:
                case suits.spades:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case suits.diamonds:
                case suits.hearts:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(suitChars[(int)suit]);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    internal class Program
    {
        static List<card> shuffle(List<card> deck)
        {
            Random r = new Random();
            for (int i = 1; i < deck.Count; i++)
            {
                int randi = r.Next(deck.Count - i);
                card temp = deck[deck.Count - i];
                deck[deck.Count - i] = deck[randi];
                deck[randi] = temp;
            }
            return deck;
        }
        static void Main(string[] args)
        {
            //generates deck
            List<card> deck = new List<card> { };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    deck.Add(new card((suits)i, (values)j));
                }
            }

            //shuffles deck
            deck = shuffle(deck);

            //deals hands
            List<List<card>> hands = new List<List<card>> { new List<card>(13), new List<card>(13), new List<card>(13), new List<card>(13) };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    hands[i].Add(deck[(i * 13) + j]);
                }
            }

            //prints hands
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("hand #" + (i + 1));
                for (int j = 0; j < 13; j++)
                {
                    Console.Write("\t");
                    hands[i][j].Print();
                }
            }
        }
    }
}