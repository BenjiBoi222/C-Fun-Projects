using System;
using System.Collections.Generic;
using System.Text;

namespace DevInitializer
{
    internal class Functions
    {
        public static void showMenu()
        {
            string[] menuOptions =
            {
                "Download and install developer tools"
            };
            int chosenIndex = SystemSettings.ShowMenu(menuOptions, "Dev Initializer");


            switch(chosenIndex)
            {
                case 0:
                    SystemSettings.ToolInstallerMenu();
                    break;
            }
        }




    }
}
