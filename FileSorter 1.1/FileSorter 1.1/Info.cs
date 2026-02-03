using System;
using System.Collections.Generic;
using System.Text;

namespace FileSorter_1._1
{
    class Info
    {
        public static void ShowManual()
        {
            while (true)
            {
                string[] menuOptions =
                {
                    "FileSorter Manual",
                    "Server Manual",
                    "Main menu"
                };

                string menuName = "Manual options";
                Program.ShowMenuHelper(menuName, menuOptions, out int option, ">");
                Settings.MenuSelectUI(menuName, menuOptions, ">", option);

                switch(option)
                {
                    case 0: FileInfo.ShowManual(); break;
                    case 1: ServerInfo.ShowManual(); break;
                    case 2: return;
                }
            }
        }
    }
}
