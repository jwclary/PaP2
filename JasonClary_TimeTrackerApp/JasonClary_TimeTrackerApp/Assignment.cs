using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;

namespace JasonClary_TimeTrackerApp
{
    class Assignment
    {
        //Database Location
#if false
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_MDV229_Database;port=8889";//Turn in conn
#else
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_MDV229_Database;port=3306";//Test conn
#endif
        // Declare a MySQL Connection
        MySqlConnection conn = null;
        Task taskManager = new Task();
        public Menu _menu;
        List<Task> TaskList = new List<Task>();
        List<string> ActivityCategories = new List<string>();
        List<string> ActivityDescriptions = new List<string>();
        List<DateTime> ActivityDates = new List<DateTime>();
        // Task variables
        private int dayNum;
        private DateTime calendarDate;
        private string dayName;
        private string categoryDescription;
        private string activityDescription;
        private double activityTime;

        public Assignment()
        {
            //taskManager = new Task();
            //_menu = new Menu("Login Menu", 
            //                 "Login", 
            //                 "Create New Account",
            //                 "Exit");
            //_menu.Display();
            //LoginSelect();

            //----------------SQL to Program----------------
            // Declare a MySQL Connection
            MySqlConnection conn = null;
            string stm;
            MySqlDataReader rdr;
            MySqlCommand cmd;

            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT time_tracker_users.user_id, calendar_day, tracked_calendar_dates.calendar_date, days_of_week.day_name, " +
                             "activity_categories.category_description, activity_descriptions.activity_description, activity_times.time_spent_on_activity " +
                      "FROM activity_log " +
                      "JOIN time_tracker_users ON activity_log.user_id = time_tracker_users.user_id " +
                      "JOIN tracked_calendar_days ON activity_log.calendar_day = tracked_calendar_days.calendar_day_id " +
                      "JOIN tracked_calendar_dates ON activity_log.calendar_date = tracked_calendar_dates.calendar_date_id " +
                      "JOIN days_of_week ON activity_log.day_name = days_of_week.day_id " +
                      "JOIN activity_categories ON activity_log.category_description = activity_categories.activity_category_id " +
                      "JOIN activity_descriptions ON activity_log.activity_description = activity_descriptions.activity_description_id " +
                      "JOIN activity_times ON activity_log.time_spent_on_activity = activity_times.activity_time_id;";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL Statement and Convert Results to a String
                rdr = cmd.ExecuteReader();

                // Output Results
                while (rdr.Read())
                {
                    dayNum = int.Parse(rdr[1].ToString());
                    calendarDate = DateTime.Parse(rdr[2].ToString());
                    dayName = rdr[3].ToString();
                    categoryDescription = rdr[4].ToString();
                    activityDescription = rdr[5].ToString();
                    activityTime = double.Parse(rdr[6].ToString());

                    taskManager = new Task(dayNum, calendarDate, dayName, categoryDescription, activityDescription, activityTime);
                    TaskList.Add(taskManager);
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

            // Menu shown to the user
            _menu = new Menu($"Hello {taskManager.FirstName} {taskManager.LastName}, What Would You Like To Do Today?",
                           "Enter Activity",
                           "View Tracked Data",
                           "Run Calculations",
                           "Exit");
            _menu.Display();
            Selection();
        }

        //private void LoginSelect()
        //{
        //    int selection = Validation.ValidateInt("Make a selection");

        //    switch (selection)
        //    {
        //        case 1:
        //            Console.Clear();
        //            taskManager.UserId = (Validation.ValidateInt("What is your User Id?").ToString());
        //            taskManager.Password = Validation.ValidateString("/nWhat is your Password?");
        //            if (taskManager.LogIn() == false)
        //            {
        //                Console.Clear();
        //                _menu.Display();
        //                LoginSelect();
        //            }
        //            break;
        //        case 2:
        //            Console.Clear();
        //            TrackedData();
        //            break;
        //        case 3:
        //            Exit();
        //            break;
        //        default:
        //            Console.Clear();
        //            _menu.Display();
        //            LoginSelect();
        //            break;
        //    }
        //}

        private void Selection()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    Activity();
                    break;
                case 2:
                    Console.Clear();
                    TrackedData();
                    break;
                case 3:
                    Console.Clear();
                    Calculations();
                    break;
                case 4:
                    Exit();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    Selection();
                    break;
            }
        }

        public void Activity()
        {
            ActivityCategories.Clear();
            CategoryList();

            // Array to hold the Category List
            ActivityCategories.Add("Back");
            string[] catArray = ActivityCategories.ToArray();
            // Sub-Menu
            _menu = new Menu("Pick A Category Of Activity:", catArray);
            _menu.Display();
            CategorySelect();
        }
        public void CategorySelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    categoryDescription = ActivityCategories[selection - 1];

                    // New Sub-Menu
                    Console.Clear();
                    ActivityDescriptions.Clear();
                    ActivityList();

                    ActivityDescriptions.Add("Back");
                    string[] actDescArray = ActivityDescriptions.ToArray();
                    _menu = new Menu($"Pick an Activity Description", actDescArray);
                    _menu.Display();
                    ActivitySelect();
                    break;
                case 25:
                    Console.Clear();
                    _menu = new Menu($"Hello {taskManager.FirstName} {taskManager.LastName}, What Would You Like To Do Today?",
                               "Enter Activity",
                               "View Tracked Data",
                               "Run Calculations",
                               "Exit");
                    _menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    CategorySelect();
                    break;
            }
        }

        private void ActivitySelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    activityDescription = ActivityDescriptions[selection - 1];

                    // New Sub-Menu
                    Console.Clear();
                    ActivityDates.Clear();
                    DateList();

                    List<string> tempDates = new List<string>();
                    foreach (DateTime item in ActivityDates)
                    {
                        tempDates.Add(item.ToShortDateString());
                    }
                    tempDates.Add("Back");
                    string[] dateArray = tempDates.ToArray();

                    _menu = new Menu($"What Date Did You Perform Activity?", dateArray);
                    _menu.Display();
                    DateSelect();
                    break;
                case 25:
                    Console.Clear();
                    string[] catArray = ActivityCategories.ToArray();
                    // Sub-Menu
                    _menu = new Menu("Pick A Category Of Activity:", catArray);
                    _menu.Display();
                    CategorySelect();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    ActivitySelect();
                    break;
            }
        }

        private void DateSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                    calendarDate = ActivityDates[selection - 1];

                    // New Sub-Menu
                    Console.Clear();
                    _menu = new Menu("Would You Like To Select Time Of Activity Or Go Back?", "Selection Time", "Back");
                    _menu.Display();
                    TimeSelect();
                    break;
                case 27:
                    Console.Clear();
                    string[] actDescArray = ActivityDescriptions.ToArray();
                    _menu = new Menu($"Pick an Activity Description", actDescArray);
                    _menu.Display();
                    ActivitySelect();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    DateSelect();
                    break;
            }
        }

        private void TimeSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    double time = Validation.ValidateDouble("How Long Did You Perform That Activity? " +
                                "\n(Keep in mind every 15 minutes is represented as a 0.25): (Format: 0.00) Between .25 and 24 hours",
                                (double)0.25, (double)24.00);
                    activityTime = Math.Round(time * 4, MidpointRounding.ToEven) / 4;

                    // Final Menu
                    Console.Clear();
                    dayNum = (int.Parse(calendarDate.ToString("dd")) - 7);
                    dayName = calendarDate.ToString("dddd");
                    Task newTask = new Task(dayNum, calendarDate, dayName, categoryDescription, activityDescription, activityTime);
                    TaskList.Add(newTask);

                    Console.WriteLine($"Day Number: {dayNum,-24}\n" +
                                      $"Calendar Date: {calendarDate.ToShortDateString(),-24}\n" +
                                      $"Day Of Week; {dayName,-24}\n" +
                                      $"Category: {categoryDescription,-24}\n" +
                                      $"Activity: {activityDescription,-24}\n" +
                                      $"Time: {activityTime,-24}\n" +
                                      $"-----------------------------");
                    _menu = new Menu("Activity Entered!", "Enter Another Activity", "Back To Main Menu");
                    _menu.Display();
                    LastSelect();
                    break; ;
                case 2:
                    Console.Clear();
                    List<string> tempDates = new List<string>();
                    foreach (DateTime item in ActivityDates)
                    {
                        tempDates.Add(item.ToShortDateString());
                    }
                    tempDates.Add("Back");
                    string[] dateArray = tempDates.ToArray();

                    _menu = new Menu($"What Date Did You Perform Activity?", dateArray);
                    _menu.Display();
                    DateSelect();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    TimeSelect();
                    break;
            }
        }

        private void LastSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    Activity();
                    break;
                case 2:
                    Console.Clear();
                    _menu = new Menu($"Hello {taskManager.FirstName} {taskManager.LastName}, What Would You Like To Do Today?",
                              "Enter Activity",
                              "View Tracked Data",
                              "Run Calculations",
                              "Exit");
                    _menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    _menu.Display();
                    LastSelect();
                    break;
            }
        }

        public void TrackedData()
        {

        }

        public void Calculations()
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

        public void ActivityList()
        {
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "SELECT activity_description " +
                         "FROM activity_descriptions;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                ActivityDescriptions.Add(rdr["activity_description"].ToString());
            }
            rdr.Close();
        }

        public void CategoryList()
        {
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "SELECT category_description " +
                         "FROM activity_categories;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                ActivityCategories.Add(rdr["category_description"].ToString());
            }
            rdr.Close();
        }

        public void DateList()
        {
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "SELECT calendar_date " +
                         "FROM tracked_calendar_dates;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                ActivityDates.Add(DateTime.Parse(rdr["calendar_date"].ToString()));
            }
            rdr.Close();
        }
    }
}
