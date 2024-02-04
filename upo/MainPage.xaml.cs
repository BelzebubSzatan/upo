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
        Card middleCard=new Card();
        public MainPage() {
            InitializeComponent();
            playerCards = deck.GeneratePlayerCards(3);
            computerCards = deck.GeneratePlayerCards(3);
            RenderCards();
        }
        public void RenderCards() {
            if (win)
                return;
            PlayerCards.Children.Clear();
            playerCards = playerCards.OrderBy(x => x.Color.ToString()).ToList();
            foreach (Card card in playerCards) {
                Button button = card.RenderCard(card);
                button.Clicked += CardClicked;
                button.BindingContext = card;
                if(playerAction) {
                    if (card.Color == middleCard.Color || card.Value == middleCard.Value || card.Special == Special.color) {
                        button.BorderColor = Color.LightGreen;
                        button.BorderWidth = 3;
                    }
                }
                PlayerCards.Children.Add(button);
            }

        }

        private void CardClicked(object sender, EventArgs e) {
            if(win) return;
            Card card = ((Button)sender).BindingContext as Card;
            if (((Button)sender).BorderColor == Color.LightGreen) {
                SetLastCard(card);
                if (card.Special != Special.normal) {
                    SpecialCards(computerCards,card);
                }
                playerCards.Remove(card);
                playerAction = false;
                //WinCheck();
                RenderCards();
                //ComputerMove();
            }
        }

        private void Button_Clicked(object sender, EventArgs e) {

        }
    }
}
