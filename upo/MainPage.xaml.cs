using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upo.models;
using Xamarin.Forms;

namespace upo {
    public partial class MainPage : ContentPage {
        Deck deck = new Deck();
        List<Card> playerCards = new List<Card>();
        List<Card> computerCards = new List<Card>();
        bool playerAction = true;
        bool win = false;
        Card middleCard = null;
        public MainPage()
        {
            InitializeComponent();
            playerCards = deck.GeneratePlayerCards(3);
            computerCards = deck.GeneratePlayerCards(3);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
        void SetLastCard(Card c = null)
        {
            if (middleCard == null)
            {
                middleCard = deck.deckCards[0];
                deck.deckCards.RemoveAt(0);
            }
            else
                middleCard = c;
            LastCardText.Text = middleCard.Value.ToString();
            LastCardStack.BackgroundColor = middleCard.Color;
        }
    }
}
