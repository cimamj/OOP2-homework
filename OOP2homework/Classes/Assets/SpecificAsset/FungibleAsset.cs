using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Assets.SpecificAsset
{
    public class FungibleAsset : Asset
    {
        public string Symbol { get; }
        private decimal _usdValue;
        public decimal UsdValue => _usdValue;
        public FungibleAsset (string name,string symbol,decimal usdValue) : base (name)
        { 
            Symbol = symbol;
            _usdValue = usdValue;
        }

        public void ChangeUSDValue()
        {
                    Random random = new Random();
                    decimal changePercent = (decimal)(random.NextDouble() * 0.05-0.025);
                    _usdValue *= (1+ changePercent);
        }
    }
}
