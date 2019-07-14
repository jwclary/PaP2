using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews2
{
    class RestaurantProfile
    {
        public string _name;
        public string _address;
        public string _phone;
        public string _hoursOpen;
        public string _price;
        public string _city;
        public string _cuisine;
        public decimal _foodRate;
        public decimal _serviceRate;
        public decimal _ambienceRate;
        public decimal _valueRate;
        public decimal _overallRate;

        public RestaurantProfile(string name, string address, string phone, string hoursOpen, string price, string city, string cuisine,
                                 decimal foodRate, decimal serviceRate, decimal ambienceRate, decimal valueRate, decimal overallRate)
        {
            _name = name;
            _address = address;
            _phone = phone;
            _hoursOpen = hoursOpen;
            _price = price;
            _city = city;
            _cuisine = cuisine;
            _foodRate = foodRate;
            _serviceRate = serviceRate;
            _ambienceRate = ambienceRate;
            _valueRate = valueRate;
            _overallRate = overallRate;
        }
    }
}
