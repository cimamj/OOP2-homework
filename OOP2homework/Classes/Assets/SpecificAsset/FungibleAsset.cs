using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Assets.SpecificAsset
{
    public class FungibleAsset : Asset
    {
        public string Label { get; }
        public decimal Amount { get; }
        public FungibleAsset (string name, decimal valueToDollar, string label, decimal amount) : base (name, valueToDollar) { Label = label; Amount = amount; }
    }
}
