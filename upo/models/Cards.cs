using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace upo.models {
    public class Card {
        public string Value { get; set; }
        public System.Drawing.Color Color { get; set; }
        public Special Special { get; set; }
        public Button RenderCard(Card c) {
            return new Button() {
                Text = c.Value,
                BackgroundColor = c.Color,
                TextColor = System.Drawing.Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 150,
                HeightRequest = 80,
                Margin=new Thickness(5),
            };
        }
    }
}
