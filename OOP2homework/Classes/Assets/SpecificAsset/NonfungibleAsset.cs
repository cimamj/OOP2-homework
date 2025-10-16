using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Assets.SpecificAsset
{
    public class NonfungibleAsset : Asset
    {
        public Guid FungibleAssetAddress { get; }
        public decimal Value { get; } //npr 0.1BTC
        public NonfungibleAsset(string name, decimal value, Guid fungibleAssetAddress) : base(name) 
        { 
            Value = value;
            FungibleAssetAddress = fungibleAssetAddress;
        }
        
        public decimal GetUSDValue (List<FungibleAsset> fungibleAssets)
        {
            var relatedAsset = fungibleAssets.Find(a => a.Address == FungibleAssetAddress);
            if (relatedAsset == null)
                throw new InvalidOperationException("Povezani fungible asset nije pronađen.");
            return Value * relatedAsset.UsdValue;
        }
    }
}
