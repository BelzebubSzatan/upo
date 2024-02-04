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
            Card middleCard=new Card();
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
        void SpecialCards(List<Card> target, Card card)
        {
            if (deck.deckCards.Count < 10)
                deck.GenerateCards();
            if(card.Special == Special.addTwo)
            {
                for (int i = 0; i < 2; i++)
                {
                    target.Add(deck.deckCards[i]);
                    deck.deckCards.RemoveAt(i);
                }
            }
            if(card.Special == Special.addFour)
            {
                for (int i = 0; i < 4; i++)
                {
                    target.Add(deck.deckCards[i]);
                    deck.deckCards.RemoveAt(i);
                }
            }
        }
        async void WinCheck()
        {
            if (playerCards.Count == 0)
            {
                await DisplayAlert("Wygrana", "Wygral gracz!", "Ok");
                win = true;
            }    
            if(computerCards.Count == 0)
            {
                await DisplayAlert("Wygrana", "Wygral komputer", "Ok");
                win = true;
            }
        }
        async void ComputerMove()
        {
            if (win == true) return;

            List<Card> possibleMoves = computerCards.Where(e =>
                    (e.Color.ToString() == middleCard.Color.ToString() ||
                    e.Value == middleCard.Value) ||
                    (e.Color.ToString() != middleCard.Color.ToString() && e.Value == "kolor")
            ).ToList();


            if (possibleMoves.Count > 0)
            {
                Random r = new Random();
                int n = r.Next(possibleMoves.Count - 1);
                await Task.Delay(1000);
                SetLastCard(possibleMoves[n]);
                SpecialCards(playerCards, possibleMoves[n]);
                ComputerAction.Text = "Komputer dal " + possibleMoves[n].Value + " " + possibleMoves[n].Color.ToString();
                computerCards.Remove(possibleMoves[n]);
            }
            else
            {
                ComputerAction.Text = "Komputer Dobral";
                computerCards.Add(deck.deckCards[0]);
                deck.deckCards.RemoveAt(0);
            }

            playerAction = true;
            WinCheck();
            RenderCards();
        }
    }
}
