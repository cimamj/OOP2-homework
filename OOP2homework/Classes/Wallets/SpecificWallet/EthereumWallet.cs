using OOP2homework.Classes.Assets.SpecificAsset;
using OOP2homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Wallets.SpecificWallet
{
    public class EthereumWallet : Wallet, ISupportsNonFungible
    {
        private static readonly List<Guid> _supportedNonFungibleAssets = new List<Guid>();
        public static IReadOnlyList<Guid> SupportedNonFungibleAssets => _supportedNonFungibleAssets.AsReadOnly();

        private  readonly List<Guid> _nonFungibleAssetsOwned = new List<Guid>(); //mozda ovo dodati u interface prije jer ovo ima i solana i eth
        public  IReadOnlyList<Guid> NonFungibleAssetsOwned => _nonFungibleAssetsOwned.AsReadOnly();

        private static readonly List<Guid> _supportedFungibleAssets = new List<Guid>();

        public static IReadOnlyList<Guid> SupportedFungibleAssets => _supportedFungibleAssets.AsReadOnly();

        public EthereumWallet() : base() { } 
        public void AddSupportedNonFungibleAssets(Guid supportedNonFungibleAsset)
        {
            _supportedNonFungibleAssets.Add(supportedNonFungibleAsset);
        }

        public void AddNonFungibleAssetsOwned(Guid nonFungibleAssetsOwned)
        {
            _nonFungibleAssetsOwned.Add(nonFungibleAssetsOwned);
        }

        public override void AddSupportedFungibleAssets(Guid supportedFungibleAssets)
        {
            _supportedFungibleAssets.Add(supportedFungibleAssets);
        }

        public override string GetWalletType()
        {
            return "ETHEREUM";
        }

    }
}
