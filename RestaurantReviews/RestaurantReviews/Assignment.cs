using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Timers;

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

        public Menu _menu;
        public List<RestaurantProfile> _profiles = new List<RestaurantProfile>();
        public List<RestaurantReviews> _scores = new List<RestaurantReviews>();

        public Assignment()
        {
           Directory.CreateDirectory(_directory);
            if (!File.Exists(_file))
            {
                File.Create(_directory + _file).Dispose();
            }
            else
            {

            }

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

            _menu = new Menu("Hello Admin, What Would You Like To Do Today?", 
                               "Convert The Restaurant Reviews Database From SQL To JSON",
                               "Showcase Our 5 Star Rating System",
                               "Showcase Our Animated Bar Graph Review System",
                               "Play A Card Game", 
                               "Exit");
            _menu.Display();
            Selection();
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
                    Console.Clear();
                    RateSystem();
                    break;
                case 3:
                    Console.Clear();
                    AnimatedBarGraph();
                    break;
                case 4:
                    CardGame();
                    break;
                case 5:
                    Exit();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    Selection();
                    break;
            }
        }

        private void SQLtoJSON()
        {   
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
                _menu.Display();
                Selection();
            }
        }

        private void RateSystem()
        {
            if (_profiles.Count == 0)
            {
                Console.Clear();
                string text = @"Select the 'Convert SQL Database to JSON' option first. Press 'return' key to return to menu.";
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2);
                Console.Write(text);
                Console.ReadKey();

                _menu.Display();
                Selection();
            }
            else
            {
                _menu = new Menu("Hello Admin, How would you like to sort the data:",
                             "List Restaurants Alphabetically",
                             "List Restaurants in Reverse Alphabetical",
                             "Sort Restaurants From Best/Most Stars to Worst",
                             "Sort Restaurants From Worst/Least Stars to Best",
                             "Show Only X and Up",
                             "Exit");

                _menu.Display();
                RateSelection();
            }
        }

        private void RateSelection()
        {
                int selection = Validation.ValidateInt("Make a selection");

                switch (selection)
                {
                    case 1:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");
                    
                    foreach (RestaurantProfile item in _profiles)
                    {
                        Console.Write($"{item._name,-50}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(RateDisplay(item._overallRate));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("----------------------------------------------------------");

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    RateSelection();
                    break;
                    case 2:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews(Reverse)");
                    Console.WriteLine("==========================================================");

                    _profiles.Reverse();
                    foreach (RestaurantProfile item in _profiles)
                    {
                        Console.Write($"{item._name,-50}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(RateDisplay(item._overallRate));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("----------------------------------------------------------");

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    RateSelection();
                    break;
                    case 3:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews(Best to Worst)");
                    Console.WriteLine("==========================================================");

                    List<RestaurantProfile> temp = _profiles.OrderBy(_profiles => _profiles._overallRate).ToList();
                    temp.Reverse();
                    foreach (RestaurantProfile item in temp)
                    {
                        Console.Write($"{item._name,-50}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(RateDisplay(item._overallRate));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("----------------------------------------------------------");

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    RateSelection();
                    break;
                    case 4:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews(Worst to Best)");
                    Console.WriteLine("==========================================================");

                    List<RestaurantProfile> temp2 = _profiles.OrderBy(_profiles => _profiles._overallRate).ToList();
                    foreach (RestaurantProfile item in temp2)
                    {
                        Console.Write($"{item._name,-50}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(RateDisplay(item._overallRate));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("----------------------------------------------------------");

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    RateSelection();
                    break;
                    case 5:
                        Console.Clear();
                    _menu = new Menu("Show Only X and Up",
                                     "Show the Best (5 Stars)",
                                     "Show 4 Stars and Up",
                                     "Show 3 Stars and Up",
                                     "Show the Worst (1 Stars)",
                                     "Show Unrated",
                                     "Back");
                    _menu.Display();
                    StarSelect();
                        break;
                    case 6:
                        Console.Clear();
                    _menu = new Menu("Hello Admin, What Would You Like To Do Today?",
                              "Convert The Restaurant Reviews Database From SQL To JSON",
                              "Showcase Our 5 Star Rating System",
                              "Showcase Our Animated Bar Graph Review System",
                              "Play A Card Game",
                              "Exit");
                    _menu.Display();
                    Selection();
                    break;
                    default:
                        Console.Clear();
                        _menu.Display();
                        RateSelection();
                        break;
                }
        }

        private string RateDisplay(decimal stars)
        {
            if (stars >= 0.5m && stars < 1.5m)
            {
                return "*";
            }
            else if (stars >= 1.5m && stars < 2.5m)
            {
                return "**";
            }
            else if (stars >= 2.5m && stars < 3.5m)
            {
                return "***";
            }
            else if (stars >= 3.5m && stars < 4.5m)
            {
                return "****";
            }
            else if (stars >= 4.5m)
            {
                return "*****";
            }
            else
            {
                return "NO RATINGS";
            }
        }
        private void StarSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantProfile item in _profiles)
                    {
                        if(item._overallRate > 4.4m)
                        {
                            Console.Write($"{item._name,-50}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(RateDisplay(item._overallRate));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("----------------------------------------------------------");
                        }

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantProfile item in _profiles)
                    {
                        if (item._overallRate > 3.4m)
                        {
                            Console.Write($"{item._name,-50}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(RateDisplay(item._overallRate));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("----------------------------------------------------------");
                        }

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantProfile item in _profiles)
                    {
                        if (item._overallRate > 2.4m)
                        {
                            Console.Write($"{item._name,-50}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(RateDisplay(item._overallRate));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("----------------------------------------------------------");
                        }

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantProfile item in _profiles)
                    {
                        if (item._overallRate > 0.4m && item._overallRate < 1.5m)
                        {
                            Console.Write($"{item._name,-50}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(RateDisplay(item._overallRate));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("----------------------------------------------------------");
                        }

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine($"\tRestaurants Overall Reviews");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantProfile item in _profiles)
                    {
                        if (item._overallRate == 0)
                        {
                            Console.Write($"{item._name,-50}");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(RateDisplay(item._overallRate));
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("----------------------------------------------------------");
                        }

                    }

                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
                case 6:
                    Console.Clear();
                    _menu = new Menu("Hello Admin, How would you like to sort the data:",
                             "List Restaurants Alphabetically (Show Rating Next To Name)",
                             "List Restaurants in Reverse Alphabetical (Show Rating Next To Name)",
                             "Sort Restaurants From Best/Most Stars to Worst (Show Rating Next To Name)",
                             "Sort Restaurants From Worst/Least Stars to Best (Show Rating Next To Name)",
                             "Show Only X and Up",
                             "Exit");

                    _menu.Display();
                    RateSelection();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    StarSelect();
                    break;
            }
        }

        private void AnimatedBarGraph()
        {
            //----------------SQL to Program----------------
            // Declare a MySQL Connection
            MySqlConnection conn = null;
            string stm;
            MySqlDataReader rdr;
            MySqlCommand cmd;

            if (_scores.Count == 0)
            {
                try
                {
                    // Open a connection to MySQL
                    conn = new MySqlConnection(cs);
                    conn.Open();

                    // Form SQL Statement
                    stm = "SELECT RestaurantName, AVG(ReviewScore) AS ReviewScore " +
                          "FROM restaurantreviews " +
                          "JOIN restaurantProfiles ON restaurantprofiles.id = restaurantreviews.RestaurantId " +
                          "GROUP BY RestaurantName;";

                    // Prepare SQL Statement
                    cmd = new MySqlCommand(stm, conn);

                    // Execute SQL Statement and Convert Results to a String
                    rdr = cmd.ExecuteReader();

                    // Output Results
                    while (rdr.Read())
                    {
                        string name;
                        int reviewScore;

                        RestaurantReviews review;
                        try
                        {
                            name = rdr["RestaurantName"].ToString();
                            reviewScore = Convert.ToInt32(rdr["ReviewScore"]);
                            review = new RestaurantReviews(name, reviewScore);
                        }
                        catch
                        {
                            name = rdr["RestaurantName"].ToString();
                            review = new RestaurantReviews(name, 0);
                        }
                        _scores.Add(review);
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

            _menu = new Menu("How would you like to sort the data:",
                             "Show Average of Reviews for Restaurants",
                             "Dinner Spinner (Selects Random Restaurant)",
                             "Top 10 Restaurants",
                             "Back To Main Menu");
            _menu.Display();
            BarGraphSelect();
        }

        public System.Timers.Timer myAnimationTimer;
        public int myTimerCounter = 0;
        int score;
        private void BarGraphSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    
                    Console.WriteLine($"\tRestaurants Review Score");
                    Console.WriteLine("==========================================================");

                    foreach (RestaurantReviews item in _scores)
                    {
                        myTimerCounter = 50;
                        score = item._reviewScore;
                        Console.WriteLine($"{item._name}: {score}");
                        //--------------------------------
                        SetTimer();
                        Console.CursorVisible = false;
                        Thread.Sleep(150);
                        Console.WriteLine();
                        //--------------------------------
                        Console.WriteLine("----------------------------------------------------------");
                    }
                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    BarGraphSelect();
                    break;
                case 2:
                    Console.Clear();                    
                    Console.WriteLine($"\tRestaurant Review Score(Random)");
                    Console.WriteLine("==========================================================");
                    Random rnd = new Random();
                    int rndNum = rnd.Next(0, 100);

                    for (int i = 0; i < 1; i++)
                    {
                        myTimerCounter = 1;
                        score = _scores[rndNum]._reviewScore;
                        Console.WriteLine($"{_scores[rndNum]._name}: {score}");
                        //--------------------------------
                        SetTimer();
                        Console.CursorVisible = false;
                        Thread.Sleep(3500);
                        Console.WriteLine();
                        //--------------------------------
                        Console.WriteLine("----------------------------------------------------------");
                    }
                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    BarGraphSelect();
                    break;
                case 3:
                    Console.Clear();
                    
                    Console.WriteLine($"\tRestaurants Review Score(Top 10)");
                    Console.WriteLine("==========================================================");

                    List<RestaurantReviews> temp = _scores.OrderBy(_scores => _scores._reviewScore).ToList();
                    temp.Reverse();
                    for (int i = 0; i < 10; i++)
                    {
                        myTimerCounter = 25;
                        score = temp[i]._reviewScore;
                        Console.WriteLine($"{temp[i]._name}: {score}");
                        //--------------------------------
                        SetTimer();
                        Console.CursorVisible = false;
                        Thread.Sleep(2000);
                        Console.WriteLine();
                        //--------------------------------
                        Console.WriteLine("----------------------------------------------------------");
                    }
                    Console.Write("\nPress 'return' key to return to menu.");
                    Console.ReadKey();
                    Console.Clear();
                    _menu.Display();
                    BarGraphSelect();
                    break;
                case 4:
                    Console.Clear();
                    _menu = new Menu("Hello Admin, What Would You Like To Do Today?",
                                     "Convert The Restaurant Reviews Database From SQL To JSON",
                                     "Showcase Our 5 Star Rating System",
                                     "Showcase Our Animated Bar Graph Review System",
                                     "Play A Card Game",
                                     "Exit");
                    _menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    BarGraphSelect();
                    break;
            }
        }

        private void SetTimer()
        {
            myAnimationTimer = new System.Timers.Timer(50);
            myAnimationTimer.Elapsed += OnTimedEvent;
            myAnimationTimer.AutoReset = true;
            myAnimationTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var myBackgroundColor = ConsoleColor.DarkGray;
            var myBarGraphColor = ConsoleColor.Blue;

            Random myRandomNumber = new Random();
            var theRating = myRandomNumber.Next(0, 101);

            if (myTimerCounter < 50)
            {
                myTimerCounter++;

                if (theRating <= 30)
                {
                    myBarGraphColor = ConsoleColor.Red;
                }
                else if (theRating > 30 && theRating < 70)
                {
                    myBarGraphColor = ConsoleColor.Yellow;
                }
                else
                {
                    myBarGraphColor = ConsoleColor.Green;
                }

                Console.BackgroundColor = myBarGraphColor;
                for (int ii = 0; ii <= theRating; ii++)
                {
                    Console.Write(" ");
                }

                int myTotalNumber = 100;
                Console.BackgroundColor = myBackgroundColor;
                for (int iii = theRating; iii <= myTotalNumber; iii++)
                {
                    Console.Write(" ");
                }
            }
            else
            {
                Console.WriteLine("error");
            }
            
            Console.CursorLeft = 0;
            if (myTimerCounter == 50)
            {
                myAnimationTimer.Stop();
                if (score <= 30)
                {
                    myBarGraphColor = ConsoleColor.Red;
                }
                else if (score > 30 && score < 70)
                {
                    myBarGraphColor = ConsoleColor.Yellow;
                }
                else
                {
                    myBarGraphColor = ConsoleColor.Green;
                }

                Console.BackgroundColor = myBarGraphColor;
                for (int ii = 0; ii <= score; ii++)
                {
                    Console.Write(" ");
                }

                int myTotalNumber = 100;
                Console.BackgroundColor = myBackgroundColor;
                for (int iii = score; iii <= myTotalNumber; iii++)
                {
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;

                Console.Write("");
                Console.CursorVisible = false;
            }       
        }

            private void CardGame()
        {

        }

        private void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("\nExiting");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
        }
    }
}
