using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class Animals
    {
        ///<summary>Name of the dog</summary>
        public string Name { get; set; }

        ///<summary>The amount of days the dog needs to be in the hotel</summary>
        public int AmountOfDaysLeft { get; set; }

        ///<summary>1-small;2-medium;3-large</summary>
        public int AnimalSize { get; set; }

        ///<summary>The amount of money the user will recieve after the dog leaves the hotel</summary>
        public int MoneyForAnimal { get; set; }

        ///<summary>A public string that contains the type of the animal like "cat" or "dog" or anything</summary>
        public string AnimalType { get; set; }

        //These three will determen if the animal needs any essential, and these fields will act for all the animal
        ///<!--This is a random event that might happen any day-->
        public bool NeedsWalk { get; set; } = false;
        //More random events in the future
        
        ///<!--These two are mandatory each day!-->
        public bool NeedsFood { get; set; } = false;
        public bool NeedsWater { get; set; } = false;

        ///<summary>What type of food the animal will need uniquely</summary>
        public string NeededFoodType { get; set; }
        public int AmountOfFoodPerDay { get; set; }
    }
}
