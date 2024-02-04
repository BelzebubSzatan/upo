using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace upo.models {
    public class Deck {
        List<Card> deckCards = new List<Card>();
        List<System.Drawing.Color> colors = new List<System.Drawing.Color>() {
            System.Drawing.Color.Red,
            System.Drawing.Color.Green,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Yellow,
        };
        List<string> Values = new List<string>() {
            "1","2","3","4","5","6","7","8","9","+2","+4","kolor"
        };
        public void GenerateCards() {
            foreach (System.Drawing.Color color in colors)
                foreach (string value in Values)
                    deckCards.Add(new Card() {
                        Color = color,
                        Value = value,
                        Special= GetSpecial(value)
                    });
            Shuffle();
        }
        public Deck()
        {
            GenerateCards();
        }
        public Special GetSpecial(string value) {
            switch (value) {
                case "+2":
                    return Special.addTwo;
                case "+4":
                    return Special.addFour;
                case "kolor":
                    return Special.color;
                default: 
                    return Special.normal;
            }
        }
        public void Shuffle() {
            Random r=new Random();
            deckCards=deckCards.OrderBy(x => r.Next()).ToList();
        }
        public List<Card> GeneratePlayerCards(int i) {
            List<Card> cards = new List<Card>();
            cards = deckCards.GetRange(0, i);
            deckCards.RemoveRange(0, i);
            return cards;
        }
    }
}
