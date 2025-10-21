using OOP2homework.Classes.Assets.SpecificAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Wallets.SpecificWallet
{
    public class BitcoinWallet : Wallet
    {
        private static readonly List<Guid> _supportedFungibleAssets = new List<Guid>();
        public static IReadOnlyList<Guid> SupportedFungibleAssets => _supportedFungibleAssets.AsReadOnly(); //posto nije u konstruktoru net reba get
        public BitcoinWallet() : base() { }

        public override void AddSupportedFungibleAssets(Guid supportedFungibleAssets)
        {
            _supportedFungibleAssets.Add(supportedFungibleAssets);
        }

        //dodatne metode
        public override string GetWalletType()
        {
            return "BITCOIN";    
        }

        public override IReadOnlyList<Guid> GetSupportedFungibleAssets()
        {
            return SupportedFungibleAssets;
        }
    }
}
