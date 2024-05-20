using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public enum suits
    {
        diamonds,
        hearts,
        clubs,
        spades
    }
    /*
     * Diamond:
     * /^\
     * \v/
     * 
     * Heart:
     * ∩ ∩
     * \ /
     * 
     * Spades:
     * /^\
     * _╨_
     * 
     * Clubs
     *  *
     * *|*
     * 
     */
    public enum values
    {
        ace,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen,
        king,
    }
    internal class Card
    {
        //instance
        public suits suit;
        public values value;
        public bool faceUp = true;
        public Card(suits suit, values value)
        {
            this.suit = suit;
            this.value = value;
        }
        public int GetBlackJackValue()
        {
            if ((int)value >= (int)values.jack)
            {
                return 10;
            }
            else
            {
                return (int)value + 1;
            }
        }
        public override string ToString()
        {
            return Program.DEFAULT_COLOR + value.ToString() + " of " + GetColor(suit) + suit.ToString();
        }
        public string ToShortString()
        {
            return GetColor(suit).ToString() + GetChar(suit) + Program.DEFAULT_COLOR + GetString(value);
        }
        public string GetAsciiString()
        {
            if (faceUp)
            {
                string c1 = BACKGROUND.ToBackgroundString() + FOREGROUND;
                string c2 = Program.DEFAULT_BACKGROUND.ToBackgroundString() + Program.DEFAULT_COLOR;
                string output =
                            c2 + " --------- " + "\n" +
                            "|" + c1 + "      val" + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "val      " + c2 + "|\n" +
                            c2 + " --------- " + "\n";
                switch (value)
                {
                    case values.ace:
                        output =
                            c2 + " --------- " + "\n" +
                            "|" + c1 + "      ace" + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "   top   " + c2 + "|\n" +
                            "|" + c1 + "   bot   " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "         " + c2 + "|\n" +
                            "|" + c1 + "ace      " + c2 + "|\n" +
                            c2 + " --------- " + "\n";
                        string top;
                        string bottom;
                        switch (suit)
                        {
                            case suits.diamonds:
                                top = "/^\\";
                                bottom = "\\v/";
                                break;
                            case suits.hearts:
                                top = "∩ ∩";
                                bottom = "\\ /";
                                break;
                            case suits.spades:
                                top = "/^\\";
                                bottom = "_╨_";
                                break;
                            default:
                                top = " * ";
                                bottom = "*|*";
                                break;
                        }
                        top = GetColor(suit) + top + FOREGROUND;
                        bottom = GetColor(suit) + bottom + FOREGROUND;
                        output = output.Replace("top", top);
                        output = output.Replace("bot", bottom);
                        break;
                    case values.two:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        2" + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "2        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.three:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        3" + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "3        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.four:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        4" + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "4        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.five:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        5" + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "5        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.six:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        6" + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + "  s   s  " + c2 + "|\n" +
                           "|" + c1 + "6        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.seven:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        7" + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + "7        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.eight:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        8" + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "8        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.nine:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "        9" + c2 + "|\n" +
                           "|" + c1 + " s  s  s " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + " s  s  s " + c2 + "|\n" +
                           "|" + c1 + "         " + c2 + "|\n" +
                           "|" + c1 + " s  s  s " + c2 + "|\n" +
                           "|" + c1 + "9        " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.ten:
                        output =
                           c2 + " --------- " + "\n" +
                           "|" + c1 + "    s  " + c1 + "10" + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "    s    " + c2 + "|\n" +
                           "|" + c1 + " s     s " + c2 + "|\n" +
                           "|" + c1 + "10  s    " + c2 + "|\n" +
                           c2 + " --------- " + "\n";
                        break;
                    case values.jack:
                        output = output.Replace("val ", "jack");
                        output = output.Replace(" val", "jack");
                        break;
                    case values.queen:
                        output = output.Replace("val  ", "queen");
                        output = output.Replace("  val", "queen");
                        break;
                    case values.king:
                        output = output.Replace("val ", "king");
                        output = output.Replace(" val", "king");
                        break;
                    default:
                        output = output.Replace("val ", ((int)value + 1) + "   ");
                        output = output.Replace(" val", "   " + ((int)value + 1));
                        break;
                }
                output = output.Replace("s", GetColor(suit).ToString() + GetChar(suit));
                return output;
            }
            else
            {
                //face down
                string c1 = BACK_BLUE.ToBackgroundString() + BLUE;
                string c2 = Program.DEFAULT_BACKGROUND.ToBackgroundString() + Program.DEFAULT_COLOR;
                string output =
                               c2 + " --------- " + "\n" +
                               "|" + c1 + "/\\/\\/\\/\\/" + c2 + "|\n" +
                               "|" + c1 + "\\/\\/\\/\\/\\" + c2 + "|\n" +
                               "|" + c1 + "/\\/\\/\\/\\/" + c2 + "|\n" +
                               "|" + c1 + "\\/\\/\\/\\/\\" + c2 + "|\n" +
                               "|" + c1 + "/\\/\\/\\/\\/" + c2 + "|\n" +
                               "|" + c1 + "\\/\\/\\/\\/\\" + c2 + "|\n" +
                               "|" + c1 + "/\\/\\/\\/\\/" + c2 + "|\n" +
                               c2 + " --------- " + "\n";
                return output;
            }
        }
        //static
        public static Color RED = new Color(255, 0, 0);
        public static Color BLACK = new Color(0, 0, 0);
        public static Color BLUE = new Color(0, 0, 128);
        public static Color BACK_BLUE = new Color(0, 0, 64);
        public static Color BACKGROUND = new Color(192, 192, 192);
        public static Color FOREGROUND = new Color(64, 64, 64);
        public static Color GetColor(suits suit)
        {
            if (IsRed(suit))
            {
                return RED;
            }
            return BLACK;
        }
        public static bool IsRed(suits suit)
        {
            return (suit == suits.diamonds || suit == suits.hearts);
        }
        public static char GetChar(suits suit)
        {
            //https://www.alt-codes.net/suit-cards.php
            switch (suit)
            {
                case suits.spades:
                    //return 'S';
                    return '♠';
                case suits.hearts:
                    //return 'H';
                    return '♥';
                case suits.clubs:
                    //return 'C';
                    return '♣';
                default:
                    //return 'D';
                    return '♦';
            }
        }
        public static string GetString(values value)
        {
            switch (value)
            {
                case values.ace:
                    return "A";
                case values.jack:
                    return "J";
                case values.queen:
                    return "Q";
                case values.king:
                    return "K";
                default:
                    return ((int)value + 1).ToString();
            }
        }
    }
}
