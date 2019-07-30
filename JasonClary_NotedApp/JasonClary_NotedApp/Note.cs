using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonClary_NotedApp
{
    class Note
    {
        static public int UserId { get; set; }
        static public string Password { get; set; }
        static public string Firstname { get; set; }
        static public string Lastname { get; set; }

        public int _noteId;
        public string _priority;
        public string _backgroundColor;
        public DateTime _noteChanged;
        public DateTime _noteDate;
        public DateTime _noteReminder;
        public string _noteText;
        public string _noteTitle;

        public Note(int nId, string priority, string bColor, DateTime nChanged, DateTime nDate, DateTime nRemind, string nText, string nTitle)
        {
            _noteId = nId;
            _priority = priority;
            _backgroundColor = bColor;
            _noteChanged = nChanged;
            _noteDate = nDate;
            _noteReminder = nRemind;
            _noteText = nText;
            _noteTitle = nTitle;
        }

        static public int PriorityId(string priority)
        {
            switch (priority)
            {
                case "Normal":
                    return 1;
                case "Important":
                    return 2;
                case "High":
                    return 3;
                case "Hot":
                    return 4;
                default:
                    return 0;
            }
        }

        static public int BackgroundId(string color)
        {
            switch (color)
            {
                case "Blue":
                    return 1;
                case "Cyan":
                    return 2;
                case "Gray":
                    return 3;
                case "Green":
                    return 4;
                case "Magenta":
                    return 5;
                case "Red":
                    return 6;
                case "White":
                    return 7;
                case "Yellow":
                    return 8;
                case "Black":
                    return 9;
                default:
                    return 0;
            }
        }
    }
}
