using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Terminal.Gui.Graphs.BarSeries;

namespace _21an
{
    public class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>();

            // fill deck with cards
            var suites = Enum.GetValues<CardSuite>();
            foreach (var suite in suites)
            {
                var values = Enum.GetValues<CardValue>();
                foreach (var value in values)
                {
                    _cards.Add(new Card(suite, value));
                }
            }
        }

        public Card Draw()
        {
            var random = new Random();
            var randomCardIndex = random.Next(0, _cards.Count);

            var card = _cards[randomCardIndex];
            _cards.RemoveAt(randomCardIndex);

            return card;
        }
    }

    public class Card
    {
        public CardValue Value { get; }
        public CardSuite Suite { get; }

        public Card(CardSuite suite, CardValue value)
        {
            this.Suite = suite;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.Suite} {this.Value}";
        }
    }

    public enum CardValue
    {
        ess = 1,
        två = 2,
        tre = 3,
        fyra = 4,
        fem = 5,
        sex = 6,
        sju = 7,
        åtta = 8,
        nio = 9,
        tio = 10,
        knekt = 10,
        dam = 10,
        kung = 10
    }

    public enum CardSuite
    {
        Klöver,
        Spader,
        Ruter,
        Hjärter
    }

    internal class Cards
    {
        string? Name { get; set; }
        int? Points { get; set; }

        private int RandomCardColor()
        {
            Random random = new Random();
            int randomCard = random.Next(0, 3);
            return randomCard;
        }

        private string GetCardColor()
        {
            string[] CardColor =
            {
                "Klöver",
                "Spader",
                "Ruter",
                "Hjärter"
            };
            return CardColor[RandomCardColor()];
        }
    }
}