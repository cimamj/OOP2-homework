using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Assets
{
    public abstract class Asset
    {
        public Guid Address { get; }
        public string Name { get; }
        //public decimal Value { get; private set;  }

        //public decimal Amount { get; private set; }

        protected Asset(string name) //ili prima value sa $ dakle (Fun), ili sa (nonFun) 
        {
            Name = name;    
            Address = Guid.NewGuid();
        }
    }
}
