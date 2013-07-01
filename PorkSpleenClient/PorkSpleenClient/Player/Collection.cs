using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PorkSpleenClient
{
    class Collection
    {
        Hashtable collection;

        public Hashtable getSetCollection
        {
            get { return collection; }
            set { collection = value; }
        }

        public Collection(Player owner)
        {
            collection = new Hashtable();
        }

        public void FillCollection(string collectionString)
        {
            string[] cards = collectionString.Split('%');
            Card newCard = null;
            for (int i = 0; i < cards.Length; i++)
            {
                newCard.createCard(cards[i]);
                collection.Add(newCard.CardId, newCard);
            }
        }
    }
}
