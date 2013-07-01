using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorkSpleenClient
{
    class Card:GameObject
    {
        int cardId;
        string name;
        enum CardType
        {
            Spell, Action
        }
        CardType type;

        public Card()
        {
        }

        internal void createCard(string p)
        {
            string[] cardAtt = p.Split(',');

        }

        public int CardId {
            get {return cardId; }
            set {cardId = value; }
        }
    }
}
