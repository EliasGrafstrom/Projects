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
    internal class Cards
    {
        string? Name { get; set; }
        int? Points { get; set; }
        
        public object GetCard()
        {
            if (IsCardUsed())
            {
                //ta ett nytt kort
            }
            //kortet kan tas
        }

        private int RandomCardColor()
        {
            Random random = new Random();
            int randomCard = random.Next(0, 3);
            return randomCard;
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
                knekt = 11,
                dam = 12,
                kung = 13
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

        public string SavedCard()
        {
            
        }   

        private bool IsCardUsed()
        {
            string[] ExistingCards = {};

            if (ExistingCards.Any(SavedCard().Contains))
            {
                return false;   
            }
            ExistingCards = new List<string>(ExistingCards) { SavedCard() }.ToArray();
            
            return true;
        }
    }
}