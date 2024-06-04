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
            chips = 5000;
            name = "you";
            hand = new Deck(false);
        }
        public override int GetBet()
        {
            Console.WriteLine("what's your bet? (" + Program.MIN_BET + " - " + (chips - 1) + ")");
            while (true)
            {
                string answer = Console.ReadLine();
                try
                {
                    int num = int.Parse(answer);
                    if(num >= Program.MIN_BET && num < chips)
                    {
                        return num;
                    }
                    Console.WriteLine("give an answer within the range");
                }
                catch(Exception e)
                {
                    Console.WriteLine("give a valid answer");
                }
            }
        }
        public override bool WillDoubleDown(Card dealerShowing)
        {
            Console.WriteLine("double down?");
            while (true)
            {
                char answer = Console.ReadKey().KeyChar;
                if (answer == 'Y' || answer == 'y')
                {
                    return true;
                }
                else if (answer == 'N' || answer == 'n')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
        public override bool WillHit(Card dealerShowing)
        {
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
                    Console.WriteLine();
                }
            }
        }
        public override void AddWin()
        {
            base.AddWin();
            Console.WriteLine(name + " won                       ");
        }
    }
}
