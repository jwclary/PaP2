using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading;

namespace JasonClary_TimeTrackerApp
{
    class Task
    {
        //Database Location
#if false
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_MDV229_Database;port=8889";//Turn in conn
#else
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_MDV229_Database;port=3306";//Test conn
#endif
        // Declare a MySQL Connection
        MySqlConnection conn = null;

        // Fields to hold Data
        public List<String> ActivityDescriptions { get; set; }
        
        public double ActivityTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }

        // Task variables
        public int _taskId;
        public int _dayNum;
        public int _calendarDate;
        public int _dayName;
        public int _categoryDescription;
        public int _activityDescriptions;
        public int _activityTime;

        public Task(int dayNum, int calDate, int dayName, int catDesc, int actDesc, int actTime)
        {
            _dayNum = dayNum;
            _calendarDate = calDate;
            _dayName = dayName;
            _categoryDescription = catDesc;
            _activityDescriptions = actDesc;
            _activityTime = actTime;
        }

        public Task()
        {
            // -----------------------Gets the categories-----------------------
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement

                string stm2 = "SELECT activity_description " +
                              "FROM activity_descriptions;";

                

                string nameSTM = "SELECT user_firstname, user_lastname " +
                                 "FROM time_tracker_users " +
                                 "WHERE user_id = @UID;";

                // Prepare SQL Statement

                MySqlCommand cmd2 = new MySqlCommand(stm2, conn);
                MySqlCommand nameCMD = new MySqlCommand(nameSTM, conn);
                nameCMD.Parameters.AddWithValue("@UID", UserId);

                // Execute SQL Statement and Convert Results to a String

                MySqlDataReader rdr2 = cmd2.ExecuteReader();             
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

        public void NewUser()
        {
            string stm = "INSERT INTO time_tracker_users (user_id, user_password, user_firstname, user_lastname) " +
                         "VALUES (@UID, @password, @fName, @lName);";


            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@UID", UserId);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@fName", FirstName);
            cmd.Parameters.AddWithValue("@lName", LastName);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void NewTask(Task newTask)
        {
            string stm = "INSERT INTO time_tracker_users (user_id, calendar_day, calendar_date, day_name, category_description, activity_description, time_spent_on_activity) " +
                         "VALUES (@UID, @day, @date, @dayOfWeek, @catDesc, @actDesc, @actTime);";


            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@UID", newTask.UserId);
            cmd.Parameters.AddWithValue("@day", newTask._dayNum);
            cmd.Parameters.AddWithValue("@date", newTask._calendarDate);
            cmd.Parameters.AddWithValue("@dayOfWeek", newTask._dayName);
            cmd.Parameters.AddWithValue("@catDesc", newTask._categoryDescription);
            cmd.Parameters.AddWithValue("@actDesc", newTask._activityDescriptions);
            cmd.Parameters.AddWithValue("@actTime", newTask._activityTime);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        //public bool LogIn()
        //{
        //    try
        //    {
        //    string stm = "SELECT user_id, user_password, user_firstname, user_lastname " +
        //                 "FROM time_tracker_users " +
        //                 "WHERE user_id = @UID AND user_password = @password";

        //    MySqlCommand cmd = new MySqlCommand(stm, conn);

        //    cmd.Parameters.AddWithValue("@uName", UserId);
        //    cmd.Parameters.AddWithValue("@password", Password);

        //    MySqlDataReader rdr = cmd.ExecuteReader();

        //    if (rdr.HasRows)
        //    {
        //        rdr.Read();

        //        UserId = rdr["user_id"].ToString();
        //        Password = rdr["user_password"].ToString();
        //        FirstName = rdr["user_firstname"].ToString();
        //        LastName = rdr["user_lastname"].ToString();

        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("\nLogin Successful!");
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //        Thread.Sleep(500);

        //        return true;
        //    }
        //    else
        //    {  
        //        return false;
        //    }
        //    }
        //    catch (Exception)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\nUserId and/or Password is Wrong!");
        //        Console.ForegroundColor = ConsoleColor.Gray;
        //        Thread.Sleep(500);
        //    }
        //}
    }
}
