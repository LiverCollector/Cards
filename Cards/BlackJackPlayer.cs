using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal abstract class BlackJackPlayer
    {
        public float wins;
        public int chips;
        public Deck hand;
        public string name;
        public virtual bool WillDoubleDown(Card dealerShowing)
        {
            return false;
        }
        public virtual int GetBet()
        {
            return Math.Min(Math.Max((int)Math.Round((float)chips / 2000) * 100, Program.MIN_BET),chips);
        }
        public abstract bool WillHit(Card dealerShowing);
        public virtual void NotifyDraw(Card card)
        {

        }
        public virtual void NotifyDiscard(Card card)
        {

        }
        public virtual void NotifyShuffle()
        {

        }
        public virtual void AddWin()
        {
            wins++;
        }
        public virtual void AddHalfWin()
        {
            wins += 0.5f;
        }
    }
}
