using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews2
{
    class Card
    {
        public string Suite { get; set; }
        public int Value { get; set; }
        public int Points { get; set; }

        public Card(int v, int s)
        {
            Value = v;

            switch (s)
            {
                case 1:

                    Suite = "C";
                    break;
                case 2:

                    Suite = "D";
                    break;
                case 3:

                    Suite = "H";
                    break;
                case 4:

                    Suite = "S";
                    break;
            }

            if (Value > 10)
            {
                Points = 12;
            }
            else if (Value == 1)
            {
                Points = 15;
            }
            else
            {
                Points = Value;
            }
        }
    }
}
