using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Interfaces
{
    public interface ISupportsNonFungible
    {
        IReadOnlyList<Guid> NonFungibleAssetsOwned { get; } //eth i solana moraju implementirati
        IReadOnlyList<Guid> GetSupportedNonFungibleAssets(); //dodano takoder
        void AddSupportedNonFungibleAssets(Guid supportedNonFungibleAsset);
        void AddNonFungibleAssetsOwned(Guid nonFungibleAssetsOwned);

        void RemoveNonFungibleAssetsOwned(Guid nonFungibleAssetsOwned);
    }
}
