using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;

namespace JasonClary_NotedApp
{
    class Assignment
    {
        //-----------------Database Location-----------------
#if false
        //Turn in
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_Noted_Database;port=8889";
#else
        //Test conn
        string cs = @"server= 127.0.0.1;userid=root;password=root;database=JasonClary_Noted_Database;port=3306";
#endif

        public Menu menu;
        public List<Note> Notes = new List<Note>();
        public List<Note> Trash = new List<Note>();
        // Declare a MySQL Connection
        MySqlConnection conn = null;

        public Assignment()
        {
            //----------------SQL to Program----------------
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                AddNotes(conn);
                AddTrash(conn);

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
            //----------------------------------------------

            //Display Existing notes
            DisplayNotes();

            // Menu shown to the user
            menu = new Menu("MENU",
                               "View Note",
                               "New Note",
                               "Delete Note",
                               "Trash Bin",
                               "Exit");
            menu.Display();
            Selection();
        }

        private void Selection()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    if (Notes.Count == 0)
                    {
                        Console.WriteLine("There are no notes to view");
                        Thread.Sleep(1000);
                        Console.Clear();
                        menu.Display();
                        Selection();
                    }
                    else
                    {
                        int input = 0;
                        while (input > Notes.Count || input < 1)
                        {
                            input = Validation.ValidateInt("\nWhich note would you like to view?");
                        }
                        Console.Clear();
                        ViewNote(Notes[input - 1]);
                        menu = new Menu("MENU", "Change Note", "Go Back");
                        menu.Display();
                        ViewSelect(Notes[input - 1]);

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }                   
                    break;
                case 2:
                    Console.Clear();
                    NewNote();
                    break;
                case 3:
                    DeleteNote();
                    break;
                case 4:
                    if (Trash.Count == 0)
                    {
                        Console.WriteLine("Trash Bin is Empty");
                        Thread.Sleep(1000);
                        Console.Clear();
                        menu.Display();
                        Selection();
                    }
                    else
                    {
                        Console.Clear();
                        TrashBin();
                    }
                    break;
                case 5:
                    Exit();
                    break;
                default:
                    Console.Clear();
                    menu.Display();
                    Selection();
                    break;
            }
        }

        public void ViewSelect(Note note)
        {
            int choice = Validation.ValidateInt("Make a selection");
            switch (choice)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    menu = new Menu("Which note feature would you like to change?", 
                                    "Priority", 
                                    "Background Color", 
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text", 
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 2:
                    Console.Clear();
                    //Display Existing notes
                    DisplayNotes();

                    // Menu shown to the user
                    menu = new Menu("MENU",
                                       "View Note",
                                       "New Note",
                                       "Delete Note",
                                       "Trash Bin",
                                       "Exit");
                    menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    menu.Display();
                    ViewSelect(note);
                    break;
            }
        }

        private void ChangeSelect(Note note)
        {            
            int choice = Validation.ValidateInt("Make a selection");
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Gets the note priority
                    menu = new Menu("PRIORITY", "Normal", "Important", "High", "Hot");
                    menu.Display();
                    note._priority = PrioritySelect();

                    Console.Clear();
                    menu = new Menu("Which note feature would you like to change?",
                                    "Priority",
                                    "Background Color",
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text",
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 2:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Gets the Background Color
                    menu = new Menu("BACKGROUND COLOR", "Blue", "Cyan", "Gray", "Green", "Magenta", "Red", "White", "Yellow", "Black");
                    menu.Display();
                    note._backgroundColor = BackgroundSelect();

                    Console.Clear();
                    menu = new Menu("Which note feature would you like to change?",
                                    "Priority",
                                    "Background Color",
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text",
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 3:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Gets the Note reminder if the user chooses
                    string input = "";
                    while (input.ToLower() != "yes" && input.ToLower() != "no" && input.ToLower() != "y" && input.ToLower() != "n")
                    {
                        Console.Clear();
                        input = Validation.ValidateString("Would You Like to set a reminder? (Y/N)");
                    }

                    if (input.ToLower() == "yes" || input.ToLower() == "y")
                    {
                        DateTime date = note._noteDate;
                        string reminder = "";
                        while (date <= note._noteDate || !(DateTime.TryParse(reminder, out date)))
                        {
                            Console.Clear();
                            reminder = Validation.ValidateString("What date and time would you like to have a reminder? FORMAT(MM/DD/YY 00:00:00 PM or AM)\n" +
                                                                 "-make the reminder date after the current datetime");
                            DateTime.TryParse(reminder, out date);
                        }
                        note._noteReminder = date;
                    }

                    Console.Clear();
                    menu = new Menu("Which note feature would you like to change?",
                                    "Priority",
                                    "Background Color",
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text",
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 4:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Gets the note title
                    string title = "";
                    if (title.Length > 45 || string.IsNullOrWhiteSpace(title))
                    {
                        Console.Clear();
                        title = Validation.ValidateString("Please type in your note. LIMIT: 45 characters");
                    }
                    note._noteTitle = title;

                    Console.Clear();
                    menu = new Menu("Which note feature would you like to change?",
                                    "Priority",
                                    "Background Color",
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text",
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 5:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Gets the note text
                    string text = "";
                    if (text.Length > 500 || string.IsNullOrWhiteSpace(text))
                    {
                        Console.Clear();
                        text = Validation.ValidateString("Please type in your note. LIMIT: 500 characters");
                    }
                    note._noteText = text;

                    Console.Clear();
                    menu = new Menu("Which note feature would you like to change?",
                                    "Priority",
                                    "Background Color",
                                    "Add/Change Reminder",
                                    "Title",
                                    "Note Text",
                                    "Go Back");
                    menu.Display();
                    ChangeSelect(note);
                    break;
                case 6:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;

                    note._noteChanged = DateTime.Now;
                    UpdateNote(conn, note);

                    Console.Clear();
                    //Display Existing notes
                    DisplayNotes();

                    // Menu shown to the user
                    menu = new Menu("MENU",
                                       "View Note",
                                       "New Note",
                                       "Delete Note",
                                       "Trash Bin",
                                       "Exit");
                    menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    menu.Display();
                    ChangeSelect(note);
                    break;
            }
        }

        public void NewNote()
        {
            int noteId = Notes.Count + 1;
            string priority;
            string backgroundColor;
            DateTime noteDate = DateTime.Now;
            DateTime noteChanged = noteDate;
            DateTime noteReminder = noteDate;
            string noteText;
            string noteTitle;

            // Gets the note priority
            menu = new Menu("PRIORITY", "Normal", "Important", "High", "Hot");
            menu.Display();
            priority = PrioritySelect();

            Console.Clear();

            // Gets the Background Color
            menu = new Menu("BACKGROUND COLOR", "Blue", "Cyan", "Gray", "Green", "Magenta", "Red", "White", "Yellow", "Black");
            menu.Display();
            backgroundColor = BackgroundSelect();

            Console.Clear();

            // Gets the Note reminder if the user chooses
            string input = "";
            while (input.ToLower() != "yes" && input.ToLower() != "no" && input.ToLower() != "y" && input.ToLower() != "n")
            {
                Console.Clear();
                input = Validation.ValidateString("Would You Like to set a reminder? (Y/N)");
            }

            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                DateTime date = noteDate;
                string reminder = "";
                while (date <= noteDate || !(DateTime.TryParse(reminder, out date)))
                {
                    Console.Clear();
                    reminder = Validation.ValidateString("What date and time would you like to have a reminder? FORMAT(MM/DD/YY 00:00:00 PM or AM)\n" +
                                                         "-make the reminder date after the current datetime");
                    DateTime.TryParse(reminder, out date);
                }
                noteReminder = date;
            }

            Console.Clear();

            // Gets the note text
            string text = "";
            if (text.Length > 500 || string.IsNullOrWhiteSpace(text))
            {
                Console.Clear();
                text = Validation.ValidateString("Please type in your note. LIMIT: 500 characters");
            }
            noteText = text;

            Console.Clear();

            // Gets the note title
            string title = "";
            if (title.Length > 45 || string.IsNullOrWhiteSpace(title))
            {
                Console.Clear();
                title = Validation.ValidateString("Please type in your note. LIMIT: 45 characters");
            }
            noteTitle = title;

            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Add the note to the the list and database
            Note newNote = new Note(noteId, priority, backgroundColor, noteDate, noteChanged, noteReminder, noteText, noteTitle);
            Notes.Add(newNote);
            AddToNotesDB(conn, newNote);

            Console.Clear();

            //Display Existing notes
            DisplayNotes();

            // Menu shown to the user
            menu = new Menu("MENU",
                               "Sort By",
                               "Order",
                               "Trash",
                               "New Note",
                               "Delete Note",
                               "Exit");
            menu.Display();
            Selection();
        }

        public void DeleteNote()
        {
            int input = 0;
            while (input > Notes.Count || input < 1)
            {
                input = Validation.ValidateInt("\nWhich note would you like to delete?");
            }
            // Delete from database
            DeleteFromNotesDB(conn, Notes[input - 1]);
            AddToTrashDB(conn, Notes[input - 1]);

            // Removes from notes and adds to the trash list
            Trash.Add(Notes[input - 1]);
            Notes.Remove(Notes[input - 1]);

            
            Console.Clear();

            //Display Existing notes
            DisplayNotes();

            // Menu shown to the user
            menu = new Menu("MENU",
                               "Sort By",
                               "Order",
                               "Trash",
                               "New Note",
                               "Delete Note",
                               "Exit");
            menu.Display();
            Selection();
        }

        public void TrashBin()
        {
            DisplayTrash();
            menu = new Menu("MENU", "View Note", "Delete All", "Go Back");
            menu.Display();
            TrashSelect();
        }

        public void TrashSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");
            switch (selection)
            {
                case 1:
                    int input = 0;
                    while (input > Notes.Count || input < 1)
                    {
                        input = Validation.ValidateInt("\nWhich note would you like to view?");
                    }
                    Console.Clear();
                    ViewNote(Trash[input - 1]);

                    menu = new Menu("MENU", "Add to notes", "Go back");
                    int choice = Validation.ValidateInt("Make a selection");
                    switch (choice)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;

                            Notes.Add(Trash[input - 1]);
                            Trash.Remove(Trash[input - 1]);
                            Console.Clear();
                            DisplayTrash();
                            menu = new Menu("MENU", "View Note", "Delete All", "Go Back");
                            menu.Display();
                            TrashSelect();
                            break;
                        case 2:
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;

                            DisplayTrash();
                            menu = new Menu("MENU", "View Note", "Delete All", "Go Back");
                            menu.Display();
                            TrashSelect();
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    // Open a connection to MySQL
                    conn = new MySqlConnection(cs);
                    conn.Open();

                    DeleteFromTrashDB(conn);
                    break;
                case 3:
                    Console.Clear();
                    //Display Existing notes
                    DisplayNotes();

                    // Menu shown to the user
                    menu = new Menu("MENU",
                                       "View Note",
                                       "New Note",
                                       "Delete Note",
                                       "Trash Bin",
                                       "Exit");
                    menu.Display();
                    Selection();
                    break;
                default:
                    Console.Clear();
                    menu.Display();
                    TrashSelect();
                    break;
            }
        }

        public string PrioritySelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    return "Normal";
                case 2:
                    return "Important";
                case 3:
                    return "High";
                case 4:
                    return "Hot";
                default:
                    Console.Clear();
                    menu.Display();
                    PrioritySelect();
                    return null;
            }
        }

        public string BackgroundSelect()
        {
            int selection = Validation.ValidateInt("Make a selection");

            switch (selection)
            {
                case 1:
                    return "Blue";
                case 2:
                    return "Cyan";
                case 3:
                    return "Gray";
                case 4:
                    return "Green";
                case 5:
                    return "Magenta";
                case 6:
                    return "Red";
                case 7:
                    return "White";
                case 8:
                    return "Yellow";
                case 9:
                    return "Black";
                default:
                    Console.Clear();
                    menu.Display();
                    BackgroundSelect();
                    return null;
            }
        }

        public void UserInfo(MySqlConnection conn)
        {
            // Form SQL Statement
            string stm = "SELECT user_firstname, user_lastname " +
                         "FROM noted_users " +
                         "WHERE user_id = @uId;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@uId", Note.UserId);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                Note.Firstname = rdr["user_firstname"].ToString();
                Note.Lastname = rdr["user_lastname"].ToString();
            }
            rdr.Close();
        }

        public void AddNotes(MySqlConnection conn)
        {
            // Note Variables
            int noteId;
            string priority;
            string backgroundColor;
            DateTime noteChanged;
            DateTime noteDate;
            DateTime noteReminder;
            string noteText;
            string noteTitle;

            // Form SQL Statement
            string stm = "SELECT id, priority, color, noteChanged, noteDate, noteReminder, noteText, noteTitle " +
                         "FROM notes " +
                         "JOIN priority ON notes.priority_id = priority.priority_id " +
                         "JOIN backgroundcolor ON notes.backgroundColor_id = backgroundcolor.backgroundColor_id;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                Note note;

                noteId = int.Parse(rdr["id"].ToString());
                priority = rdr["priority"].ToString();
                backgroundColor = rdr["color"].ToString();
                noteDate = DateTime.Parse(rdr["noteDate"].ToString());
                noteText = rdr["noteText"].ToString();
                noteTitle = rdr["noteTitle"].ToString();

                // If user didnt choose a noteChanged
                try
                {
                    noteChanged = DateTime.Parse(rdr["noteChanged"].ToString());
                }
                catch (Exception)
                {
                    noteChanged = noteDate;
                }

                // If user didnt choose a notereminder
                try
                {
                    noteReminder = DateTime.Parse(rdr["noteReminder"].ToString());
                }
                catch (Exception)
                {
                    noteReminder = noteDate;
                }

                note = new Note(noteId, priority, backgroundColor, noteChanged, noteDate, noteReminder, noteText, noteTitle);
                Notes.Add(note);
            }
            rdr.Close();
        }

        public void AddTrash(MySqlConnection conn)
        {
            // Note Variables
            int noteId;
            string priority;
            string backgroundColor;
            DateTime noteChanged;
            DateTime noteDate;
            DateTime noteReminder;
            string noteText;
            string noteTitle;

            // Form SQL Statement
            string stm = "SELECT note_id, priority, color, noteChanged, noteDate, noteReminder, noteText, noteTitle " +
                         "FROM trash " +
                         "JOIN priority ON trash.priority_id = priority.priority_id " +
                         "JOIN backgroundcolor ON trash.backgroundColor_id = backgroundcolor.backgroundColor_id;";

            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            // Execute SQL Statement and Convert Results to a String
            MySqlDataReader rdr = cmd.ExecuteReader();

            // Output Results
            while (rdr.Read())
            {
                Note note;

                noteId = int.Parse(rdr["note_id"].ToString());
                priority = rdr["priority"].ToString();
                backgroundColor = rdr["color"].ToString();
                noteDate = DateTime.Parse(rdr["noteDate"].ToString());
                noteText = rdr["noteText"].ToString();
                noteTitle = rdr["noteTitle"].ToString();

                // If user didnt choose a noteChanged
                try
                {
                    noteChanged = DateTime.Parse(rdr["noteChanged"].ToString());
                }
                catch (Exception)
                {
                    noteChanged = noteDate;
                }

                // If user didnt choose a notereminder
                try
                {
                    noteReminder = DateTime.Parse(rdr["noteReminder"].ToString());
                }
                catch (Exception)
                {
                    noteReminder = noteDate;
                }

                note = new Note(noteId, priority, backgroundColor, noteChanged, noteDate, noteReminder, noteText, noteTitle);
                Trash.Add(note);
            }
            rdr.Close();
        }

        public void AddToNotesDB(MySqlConnection conn, Note note)
        {
            string stm = "INSERT INTO notes (id, user_id, priority_id, backgroundColor_id, noteChanged, noteDate, " +
                                             "noteReminder, noteText, noteTitle) " +
                         "VALUES (@nId, @uId, @pId, @cId, @nChange, @nDate, @nRemind, @nText, @nTitle);";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@nId", Note.UserId);
            cmd.Parameters.AddWithValue("@uId", note._noteId);
            cmd.Parameters.AddWithValue("@pId", Note.PriorityId(note._priority));
            cmd.Parameters.AddWithValue("@cId", Note.BackgroundId(note._backgroundColor));
            cmd.Parameters.AddWithValue("@nChange", note._noteChanged);
            cmd.Parameters.AddWithValue("@nDate", note._noteDate);
            cmd.Parameters.AddWithValue("@nRemind", note._noteReminder);
            cmd.Parameters.AddWithValue("@nText", note._noteText);
            cmd.Parameters.AddWithValue("@nTitle", note._noteTitle);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void DeleteFromNotesDB(MySqlConnection conn, Note note)
        {
            // Open connection
            conn.Open();

            string stm = "DELETE FROM notes " +
                         "WHERE id = @nId;";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@nId", note._noteId);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void AddToTrashDB(MySqlConnection conn, Note note)
        {
            // Open connection
            conn.Open();

            string stm = "INSERT INTO trash (note_id, user_id, priority_id, backgroundColor_id, noteChanged, noteDate, " +
                                             "noteReminder, noteText, noteTitle) " +
                         "VALUES (@nId, @uId, @pId, @cId, @nChange, @nDate, @nRemind, @nText, @nTitle);";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@nId", note._noteId);
            cmd.Parameters.AddWithValue("@uId", Note.UserId);
            cmd.Parameters.AddWithValue("@pId", Note.PriorityId(note._priority));
            cmd.Parameters.AddWithValue("@cId", Note.BackgroundId(note._backgroundColor));
            cmd.Parameters.AddWithValue("@nChange", note._noteChanged);
            cmd.Parameters.AddWithValue("@nDate", note._noteDate);
            cmd.Parameters.AddWithValue("@nRemind", note._noteReminder);
            cmd.Parameters.AddWithValue("@nText", note._noteText);
            cmd.Parameters.AddWithValue("@nTitle", note._noteTitle);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void DeleteFromTrashDB(MySqlConnection conn)
        {
            // Open connection
            conn.Open();

            string stm = "DELETE FROM trash " +
                         "WHERE user_id = @uId;";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@uId", Note.UserId);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void UpdateNote(MySqlConnection conn, Note note)
        {
            // Open connection
            conn.Open();

            string stm = "UPDATE notes " +
                         "SET priority_id = @pId, backgroundColor_id = @cId, noteChanged = @nChange, noteReminder = @nRemind, noteText = @nText, noteTitle = @nTitle " +
                         "WHERE id = @nId;";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@nId", note._noteId);
            cmd.Parameters.AddWithValue("@pId", Note.PriorityId(note._priority));
            cmd.Parameters.AddWithValue("@cId", Note.BackgroundId(note._backgroundColor));
            cmd.Parameters.AddWithValue("@nChange", note._noteChanged);
            cmd.Parameters.AddWithValue("@nRemind", note._noteReminder);
            cmd.Parameters.AddWithValue("@nText", note._noteText);
            cmd.Parameters.AddWithValue("@nTitle", note._noteTitle);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        public void DisplayNotes()
        {
            int noteIndex = 1;
            Console.WriteLine("----------------------------NOTES----------------------------");
            foreach (Note item in Notes)
            {
                NoteFeatureFilter(item);

                Console.WriteLine($"{noteIndex}) Title: \"{item._noteTitle}\"\n");
                noteIndex++;
            }
            Console.WriteLine("-------------------------------------------------------------");
        }

        public void DisplayTrash()
        {
            int noteIndex = 1;
            Console.WriteLine("----------------------------TRASH----------------------------");
            foreach (Note item in Trash)
            {
                if (item._noteDate == item._noteChanged && item._noteDate == item._noteReminder)
                {
                    Console.WriteLine($"{$"Priority: {item._priority}",-20}{$"NoteDate: {item._noteDate.ToShortDateString()}",-20}");
                }
                else if (item._noteDate == item._noteChanged)
                {
                    Console.WriteLine($"{$"Priority: {item._priority}",-20}{$"NoteDate: {item._noteDate.ToShortDateString()}",-20}" +
                                      $"{$"NoteReminder: {item._noteReminder}",-20}");
                }
                else if (item._noteDate == item._noteReminder)
                {
                    Console.WriteLine($"{$"Priority: {item._priority}",-20}{$"NoteDate: {item._noteDate.ToShortDateString()}",-20}" +
                                      $"{$"NoteChanged: {item._noteChanged}",-20}");
                }
                else
                {
                    Console.WriteLine($"{$"Priority: {item._priority}",-20}{$"NoteDate: {item._noteDate.ToShortDateString()}",-20}" +
                                      $"{$"NoteChanged: {item._noteChanged}",-20}{$"Notereminder: {item._noteReminder}",-20}");
                }

                Console.WriteLine($"{noteIndex}) Title: \"{item._noteTitle}\"\n");
                noteIndex++;
            }
            Console.WriteLine("------------------------------------------------------------");
        }

        public void ViewNote(Note note)
        {
            string color = note._backgroundColor;
            switch (color)
            {
                case "Blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Cyan":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Green":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Magenta":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Red":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "White":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    break;
            }

            NoteFeatureFilter(note);
            Console.WriteLine($"Title: \"{note._noteTitle}\"\n");
            Console.WriteLine($"Note:\n{note._noteText}\n");
        }

        public void NoteFeatureFilter(Note note)
        {
            if (note._noteDate == note._noteChanged && note._noteDate == note._noteReminder)
            {
                Console.WriteLine($"{$"Priority: {note._priority}",-20}{$"NoteDate: {note._noteDate.ToShortDateString()}",-20}");
            }
            else if (note._noteDate == note._noteChanged)
            {
                Console.WriteLine($"{$"Priority: {note._priority}",-20}{$"NoteDate: {note._noteDate.ToShortDateString()}",-20}" +
                                  $"{$"NoteReminder: {note._noteReminder}",-20}");
            }
            else if (note._noteDate == note._noteReminder)
            {
                Console.WriteLine($"{$"Priority: {note._priority}",-20}{$"NoteDate: {note._noteDate.ToShortDateString()}",-20}" +
                                  $"{$"NoteChanged: {note._noteChanged}",-20}");
            }
            else
            {
                Console.WriteLine($"{$"Priority: {note._priority}",-20}{$"NoteDate: {note._noteDate.ToShortDateString()}",-20}" +
                                  $"{$"NoteChanged: {note._noteChanged}",-20}{$"Notereminder: {note._noteReminder}",-20}");
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
    }
}
