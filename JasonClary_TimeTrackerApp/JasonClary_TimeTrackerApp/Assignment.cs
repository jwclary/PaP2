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
#if true
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
        List<string> DayOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        // Task variables
        private int dayNum;
        private int calendarDate;
        private int dayName;
        private int categoryDescription;
        private int activityDescription;
        private int activityTime;

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
                stm = "SELECT * " +
                      "FROM activity_log;";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL Statement and Convert Results to a String
                rdr = cmd.ExecuteReader();

                // Output Results
                while (rdr.Read())
                {
                    taskManager.UserId = int.Parse(rdr[1].ToString());
                    dayNum = int.Parse(rdr[2].ToString());
                    calendarDate = int.Parse(rdr[3].ToString());
                    dayName = int.Parse(rdr[4].ToString());
                    categoryDescription = int.Parse(rdr[5].ToString());
                    activityDescription = int.Parse(rdr[6].ToString());
                    activityTime = int.Parse(rdr[7].ToString());
                    taskManager = new Task(dayNum, calendarDate, dayName, categoryDescription, activityDescription, activityTime);
                    taskManager._taskId = int.Parse(rdr[0].ToString());
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
                    categoryDescription = selection - 1;

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
                    activityDescription = selection - 1;

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
                    calendarDate = selection - 1;

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
                    activityTime = (int)((Math.Round(time * 4, MidpointRounding.ToEven) / 4) * .25);

                    // Final Menu
                    Console.Clear();
                    dayNum = (int.Parse(calendarDate.ToString("dd")) - 7);
                    for (int i = 0; i < DayOfWeek.Count; i++)
                    {
                        if (DayOfWeek[i] == calendarDate.ToString("dddd"))
                        {
                            dayName = i;
                        }
                    }
                    Task newTask = new Task(dayNum, calendarDate, dayName, categoryDescription, activityDescription, activityTime);
                    TaskList.Add(newTask);

                    Console.WriteLine($"Day Number: {dayNum,-24}\n" +
                                      $"Calendar Date: {ActivityDates[calendarDate].ToShortDateString(),-24}\n" +
                                      $"Day Of Week; {DayOfWeek[dayName],-24}\n" +
                                      $"Category: {ActivityCategories[categoryDescription],-24}\n" +
                                      $"Activity: {ActivityDescriptions[activityDescription],-24}\n" +
                                      $"Time: {(double)activityTime / .25,-24}\n" +
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
            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
            _menu.Display();
            TrackedSelect();
        }

        public void TrackedSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
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

                    _menu = new Menu($"Which date would you like to view?", dateArray);
                    _menu.Display();

                    int selection2 = Validation.ValidateInt("Make a selection");

                    switch (selection2)
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
                            calendarDate = selection2;

                            Console.Clear();
                            Console.WriteLine($"\tAll Date Entries");
                            Console.WriteLine("==========================================================\n");

                            foreach (Task item in TaskList)
                            {
                                if (item._categoryDescription == categoryDescription)
                                {
                                    Console.WriteLine($"Day: {DayOfWeek[item._dayName]}");
                                    Console.WriteLine($"Date: {ActivityDates[item._calendarDate]}");
                                    Console.WriteLine($"Category: {ActivityCategories[item._categoryDescription]}");
                                    Console.WriteLine($"Activity: {ActivityDescriptions[item._activityDescriptions]}");
                                    Console.WriteLine($"time: {(double)item._activityTime / .25} hours\n");
                                    Console.WriteLine("----------------------------------------------------------");
                                }
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        case 27:
                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        default:
                            Console.Clear();
                            _menu.Display();
                            TrackedSelect();
                            break;
                    }
                    break;
                case 2:
                    Console.Clear();
                    ActivityCategories.Clear();
                    CategoryList();

                    // Array to hold the Category List
                    ActivityCategories.Add("Back");
                    string[] catArray = ActivityCategories.ToArray();
                    // Sub-Menu
                    _menu = new Menu("Pick A Category Of Activity:", catArray);
                    _menu.Display();

                    int choose2 = Validation.ValidateInt("Make a selection");

                    switch (choose2)
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
                            categoryDescription = choose2;

                            Console.Clear();
                            Console.WriteLine($"\tAll Category Entries");
                            Console.WriteLine("==========================================================\n");

                            foreach (Task item in TaskList)
                            {
                                if (item._categoryDescription == categoryDescription)
                                {
                                    Console.WriteLine($"Day: {DayOfWeek[item._dayName]}");
                                    Console.WriteLine($"Date: {ActivityDates[item._calendarDate]}");
                                    Console.WriteLine($"Category: {ActivityCategories[item._categoryDescription]}");
                                    Console.WriteLine($"Activity: {ActivityDescriptions[item._activityDescriptions]}");
                                    Console.WriteLine($"time: {(double)item._activityTime / .25} hours\n");
                                    Console.WriteLine("----------------------------------------------------------");
                                }
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        case 25:
                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        default:
                            Console.Clear();
                            _menu.Display();
                            TrackedSelect();
                            break;
                    }
                    break;
                case 3:
                    Console.Clear();
                    ActivityDescriptions.Clear();
                    ActivityList();

                    ActivityDescriptions.Add("Back");
                    string[] actDescArray = ActivityDescriptions.ToArray();
                    _menu = new Menu($"Pick an Activity Description", actDescArray);
                    _menu.Display();

                    int choose = Validation.ValidateInt("Make a selection");

                    switch (choose)
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
                            activityDescription = choose;

                            Console.Clear();
                            Console.WriteLine($"\tAll Activity Entries");
                            Console.WriteLine("==========================================================\n");

                            foreach (Task item in TaskList)
                            {
                                if (item._activityDescriptions == activityDescription)
                                {
                                    Console.WriteLine($"Day: {DayOfWeek[item._dayName]}");
                                    Console.WriteLine($"Date: {ActivityDates[item._calendarDate]}");
                                    Console.WriteLine($"Category: {ActivityCategories[item._categoryDescription]}");
                                    Console.WriteLine($"Activity: {ActivityDescriptions[item._activityDescriptions]}");
                                    Console.WriteLine($"time: {(double)item._activityTime / .25} hours\n");
                                    Console.WriteLine("----------------------------------------------------------");
                                }
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        case 25:
                            Console.Clear();
                            _menu = new Menu("View Tracked Data",
                             "Select By Date",
                             "Select By Category",
                             "Select By Description",
                             "Back");
                            _menu.Display();
                            TrackedSelect();
                            break;
                        default:
                            Console.Clear();
                            _menu.Display();
                            TrackedSelect();
                            break;
                    }
                    break;
                case 4:
                    Console.Clear();
                    // Menu shown to the user
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
                    TrackedSelect();
                    break;
            }
        }

        public void Calculations()
        {
            Console.Clear();
            // Menu shown to the user
            _menu = new Menu($"Look At All Of The Cool Data Collected Over 26 Days:",
                           "Total Time Working on School Work",
                           "Total Time on Personal Time",
                           "Total Time Sleeping",
                           "Total Time on Lecture",
                           "Total Time Driving To School",                           
                           "Percentage of Time on School Work vs Total Month",
                           "Percentage of Free Time vs Total Month",
                           "Percentage of Time on Sleeping vs Total Month",
                           "Percentage of Time on Lecture vs Total Month",
                           "Percentage of Time on Driving vs Total Month",
                           "Back");
            _menu.Display();
            CalcSelect();
        }

        public void CalcSelect()
        {
            double totalTime = 0;
            foreach (Task item in TaskList)
            {
                totalTime += item._activityTime;
            }
            totalTime = totalTime * .25;

            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    double catTime = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription >= 1 && item._categoryDescription <= 19)
                        {
                            catTime += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Total Time Spent On School Work: {catTime * .25}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 2:
                    double freeTime = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 20)
                        {
                            freeTime += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Total Time Spent On Free Time: {freeTime * .25}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 3:
                    double sleepTime = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._activityDescriptions == 17)
                        {
                            sleepTime += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Total Time Spent On Sleeping: {sleepTime * .25}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 4:
                    double lectTime = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 22)
                        {
                            lectTime += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Total Time Spent On Lecture: {lectTime * .25}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 5:
                    double travelTime = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 21)
                        {
                            travelTime += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Total Time Spent On Travel: {travelTime * .25}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 6:
                    double catPerc = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription >= 1 && item._categoryDescription <= 19)
                        {
                            catPerc += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Percent of Total School Work Over Month: {(totalTime / catPerc).ToString("0.00%")}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 7:
                    double freePerc = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 20)
                        {
                            freePerc += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Percent of Total Free Time Over Month: {(totalTime / freePerc).ToString("0.00%")}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 8:
                    double sleepPerc = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._activityDescriptions == 17)
                        {
                            sleepPerc += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Percent of Total Sleeping Over Month: {(totalTime / sleepPerc).ToString("0.00%")}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 9:
                    double lectPerc = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 22)
                        {
                            lectPerc += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Percent of Total Lecture Time Over Month: {(totalTime / lectPerc).ToString("0.00%")}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 10:
                    double travelPerc = 0;
                    foreach (Task item in TaskList)
                    {
                        if (item._categoryDescription == 21)
                        {
                            travelPerc += item._activityTime;
                        }
                    }
                    Console.WriteLine($"Percent of Total Traveling Over Month: {(totalTime / travelPerc).ToString("0.00%")}");
                    Console.ReadKey();

                    Console.Clear();
                    _menu.Display();
                    CalcSelect();
                    break;
                case 11:
                    // Menu shown to the user
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
                    CalcSelect();
                    break;
            }
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

        public void InsertIntoDB(MySqlConnection conn, Task task)
        {
            string stm = "INSERT INTO activity_log (user_id, calendar_day, calendar_date, day_name, category_description, " +
                                             "activity_descriptions, time_spent_on_activity) " +
                         "VALUES (@id, @uId, @day, @date, @dName, @cDescription, @aDescription, @time);";

            MySqlCommand cmd = new MySqlCommand(stm, conn);

            cmd.Parameters.AddWithValue("@uId", 1);
            cmd.Parameters.AddWithValue("@day", task._dayNum + 1);
            cmd.Parameters.AddWithValue("@date", task._calendarDate + 1);
            cmd.Parameters.AddWithValue("@dName", task._dayName + 1);
            cmd.Parameters.AddWithValue("@cDescription", task._categoryDescription + 1);
            cmd.Parameters.AddWithValue("@aDescription", task._activityDescriptions + 1);
            cmd.Parameters.AddWithValue("@time", task._activityTime + 1);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Close();
        }
    }
}
