using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    class Validation
    {
        public static int ValidateInt(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            int i;
            while (!int.TryParse(response, out i))
            {
                Console.WriteLine("Please enter a valid integer...");
                response = Console.ReadLine();
            }
            return i;
        }

        public static int ValidateInt(string s, int min, int max)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            int i;

            while (!int.TryParse(response, out i) || i < min || i > max)
            {
                Console.WriteLine("Please enter a valid integer within specified range");
                response = Console.ReadLine();
            }
            return i;
        }
        public static decimal ValidateDecimal(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            decimal d;
            while (!decimal.TryParse(response, out d))
            {
                Console.WriteLine("Please enter a valid decimal number...");
                response = Console.ReadLine();
            }
            return d;
        }
        public static double ValidateDouble(string s)
        {
            Console.WriteLine();
            string response = Console.ReadLine();
            double d;
            while (!double.TryParse(response, out d))
            {
                Console.WriteLine("Please enter a valid number(double)...");
                response = Console.ReadLine();
            }
            return d;
        }
        public static float ValidateFloat(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            float f;
            while (!float.TryParse(response, out f))
            {
                Console.WriteLine("Please enter a valid number(float)...");
                response = Console.ReadLine();
            }
            return f;
        }
        public static string ValidateString(string s)
        {
            Console.WriteLine(s);
            string response = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("Please do not leave this empty...");
                response = Console.ReadLine();
            }
            return response;
        }
    }
}
