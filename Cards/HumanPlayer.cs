using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class HumanPlayer : BlackJackPlayer
    {
        public HumanPlayer()
        {
            name = "you";
            hand = new Deck(false);
        }
        public override bool WillHit()
        {
            Console.WriteLine("will you hit?");
            while (true)
            {
                char answer = Console.ReadKey().KeyChar;
                if(answer == 'Y' || answer == 'y')
                {
                    return true;
                }
                else if(answer == 'N' || answer == 'n')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine(answer + " isn't a valid answer, try again.");
                }
            }
        }
    }
}
