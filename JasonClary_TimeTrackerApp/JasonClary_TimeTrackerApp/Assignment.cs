using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Timers;

namespace JasonClary_TimeTrackerApp
{
    class Assignment
    {
        //Database Location
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=exampleDatabase;port=8889";//Turn in conn
        //string cs = @"server= 127.0.0.1;userid=root;password=root;database=exampleDatabase;port=3306";//Test conn

        public Menu _menu;

        public Assignment()
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
    }
}
