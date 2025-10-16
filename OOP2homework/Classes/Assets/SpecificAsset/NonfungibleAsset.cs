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
        public NonfungibleAsset(string name, decimal valueToFungibleAsset, Guid fungibleAssetAddress) : base(name, valueToFungibleAsset) 
        { 
            FungibleAssetAddress = fungibleAssetAddress;
        }
        
    }
}
