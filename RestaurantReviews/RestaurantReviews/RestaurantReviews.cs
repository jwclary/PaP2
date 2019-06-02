using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    class RestaurantReviews
    {
        public string _name;
        public int _reviewScore;

        public RestaurantReviews(string name, int reviewScore)
        {
            _name = name;
            _reviewScore = reviewScore;
        }
    }
}
