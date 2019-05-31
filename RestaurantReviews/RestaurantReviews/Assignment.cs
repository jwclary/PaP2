using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;

namespace RestaurantReviews
{
    class Assignment
    {
        //Database Location
        //string cs = @"server= 127.0.0.1;userid=root;password=root;database=RestaurantReviews_Database;port=8889";
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=RestaurantReviews_Database;port=3306";

        //Output Location	
        private string _directory = @"..\..\output\";
        private string _file = @"info.json";

        public Menu _myMenu;
        public List<RestaurantProfile> _profiles = new List<RestaurantProfile>();

        public Assignment()
        {
           /*Directory.CreateDirectory(_directory);
            if (!File.Exists(_file))
            {
                File.Create(_directory + _file).Dispose();
            }
            else
            {

            }*/

            _myMenu = new Menu("Hello Admin, What Would You Like To Do Today?", 
                               "Convert The Restaurant Reviews Database From SQL To JSON",
                               "Showcase Our 5 Star Rating System",
                               "Showcase Our Animated Bar Graph Review System",
                               "Play A Card Game", 
                               "Exit");
            _myMenu.Display();
            Selection();

            Directory.CreateDirectory(_directory);
            if (!File.Exists(_file))
            {
                File.Create(_directory + _file).Dispose();
            }
            else
            {

            }
        }

        private void Selection()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    SQLtoJSON();
                    break;
                case 2:
                    RateSystem();
                    break;
                case 3:
                    AnimatedBarGraph();
                    break;
                case 4:
                    CardGame();
                    break;
                case 5:
                    Exit();
                    break;
                default:
                    break;
            }
        }

        private void SQLtoJSON()
        {
            //----------------SQL to Program----------------
            // Declare a MySQL Connection
            MySqlConnection conn = null;
            string stm;
            MySqlDataReader rdr;
            MySqlCommand cmd;

            if (_profiles.Count == 0)
            {
                try
                {
                    // Open a connection to MySQL
                    conn = new MySqlConnection(cs);
                    conn.Open();

                    // Form SQL Statement
                    stm = "SELECT * " +
                          "FROM restaurantprofiles;";

                    // Prepare SQL Statement
                    cmd = new MySqlCommand(stm, conn);

                    // Execute SQL Statement and Convert Results to a String
                    rdr = cmd.ExecuteReader();

                    // Output Results
                    while (rdr.Read())
                    {
                        string name;
                        string address;
                        string phone;
                        string hop;
                        string price;
                        string city;
                        string cuisine;
                        decimal fRating;
                        decimal sRating;
                        decimal aRating;
                        decimal vRating;
                        decimal oRating;

                        RestaurantProfile profile;
                        if (!(decimal.TryParse(rdr["FoodRating"].ToString(), out fRating)))
                        {
                            name = rdr["RestaurantName"].ToString();
                            address = rdr["Address"].ToString();
                            phone = rdr["Phone"].ToString();
                            hop = rdr["HoursOfOperation"].ToString();
                            price = rdr["Price"].ToString();
                            city = rdr["USACityLocation"].ToString();
                            cuisine = rdr["Cuisine"].ToString();

                            profile = new RestaurantProfile(name, address, phone, hop, price, city, cuisine,
                                                            0, 0, 0, 0, 0);
                        }
                        else
                        {
                            name = rdr["RestaurantName"].ToString();
                            address = rdr["Address"].ToString();
                            phone = rdr["Phone"].ToString();
                            hop = rdr["HoursOfOperation"].ToString();
                            price = rdr["Price"].ToString();
                            city = rdr["USACityLocation"].ToString();
                            cuisine = rdr["Cuisine"].ToString();
                            fRating = Convert.ToDecimal(rdr["FoodRating"]);
                            sRating = decimal.Parse(rdr["ServiceRating"].ToString());
                            aRating = decimal.Parse(rdr["AmbienceRating"].ToString());
                            vRating = decimal.Parse(rdr["ValueRating"].ToString());
                            oRating = decimal.Parse(rdr["OverallRating"].ToString());

                            profile = new RestaurantProfile(name, address, phone, hop, price, city, cuisine,
                                                                          fRating, sRating, aRating, vRating, oRating);
                        }

                        _profiles.Add(profile);
                    }
                    rdr.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }               
            //----------------Program to JSON----------------
            int index = 0;
            Console.Clear();
            using (StreamWriter sw = new StreamWriter(_directory + _file))
            {
                
                sw.WriteLine("[");
                foreach (RestaurantProfile item in _profiles)
                {
                    sw.WriteLine("{");
                    sw.WriteLine($"\"name\": \"{item._name}\",");
                    sw.WriteLine($"\"address\": \"{item._address}\",");
                    sw.WriteLine($"\"phone\": \"{item._phone}\",");
                    sw.WriteLine($"\"hoursOfOperation\": \"{item._hoursOpen}\",");
                    sw.WriteLine($"\"price\": \"{item._price}\",");
                    sw.WriteLine($"\"city\": \"{item._city}\",");
                    sw.WriteLine($"\"cuisine\": \"{item._cuisine}\",");
                    sw.WriteLine($"\"foodRating\": \"{item._foodRate}\",");
                    sw.WriteLine($"\"serviceRating\": \"{item._serviceRate}\",");
                    sw.WriteLine($"\"ambienceRating\": \"{item._ambienceRate}\",");
                    sw.WriteLine($"\"valueRating\": \"{item._valueRate}\",");
                    sw.WriteLine($"\"overallRating\": \"{item._overallRate}\"");
                    if (index == _profiles.Count - 1)
                    {
                        sw.WriteLine("}");
                    }
                    else
                    {
                        sw.WriteLine("},");
                    }
                    index++;
                }
                sw.WriteLine("]");
                sw.Close();
                //----------------Animation----------------
                string text;
                for (int i = 0; i < 2; i++)
                {
                    text = "Converting";
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length/2, Console.WindowHeight / 2);
                  
                    Console.Write(text);
                    Thread.Sleep(500);
                    Console.Write(".");
                    Thread.Sleep(500);
                    Console.Write(".");
                    Thread.Sleep(500);
                    Console.Write(".");
                    Thread.Sleep(500);

                    Console.Clear();
                }
                text = "SQl converted to info.JSON file";
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length/2, Console.WindowHeight / 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.Gray;

                text = "Press return to go back to menu...";
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 + 1);
                Console.Write(text);
                Console.ReadKey();
                Console.Clear();
                _myMenu.Display();
                Selection();
            }
        }

        private void RateSystem()
        {

        }

        private void AnimatedBarGraph()
        {

        }

        private void CardGame()
        {

        }

        private void Exit()
        {

        }
    }
}
