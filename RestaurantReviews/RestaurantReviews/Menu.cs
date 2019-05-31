using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    class Menu
    {
        public string Title { get; set; }
        private List<string> _items;
        public Menu()
        {

            Title = "Application";
            _items = new List<string>();
        }
        public Menu(string input, params string[] items)
        {
            Title = input;
            _items = items.ToList();
        }
        public void AddMenuItem(string item)
        {
            _items.Add(item);
        }
        public void Display()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Title);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=============================");
            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {_items[i]}");
            }
        }
    }
}
