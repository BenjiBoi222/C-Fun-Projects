using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaPanzio
{
    class Dogs
    {
        ///<summary>Name of the dog</summary>
        public string Name { get; set; }

        ///<summary>The amount of days the dog needs to be in the hotel</summary>
        public int AmountOfDaysLeft { get; set; }

        ///<summary>1-small;2-medium;3-large</summary>
        public int DogSize { get; set; }

        /// <summary>The amount of money the user will recieve after the dog leaves the hotel</summary>
        public int MoneyForDog { get; set; }
    }
}
