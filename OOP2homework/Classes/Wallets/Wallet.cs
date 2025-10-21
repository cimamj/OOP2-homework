using OOP2homework.Classes.Assets.SpecificAsset;
using OOP2homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Wallets
{
    public abstract class Wallet
    {
        public Guid Address { get; }
        public IReadOnlyDictionary<Guid, decimal> FungibleBalances { get; } //ako iza nemas => nego u konstrukotru,  dodaj get 
        public IReadOnlyList<Guid> TransactionAddresses { get; }



        private readonly Dictionary<Guid, decimal> _fungibleBalances = new Dictionary<Guid, decimal>(); //cuva se referenca, nema mijenjanja, nema set onda
        private readonly List<Guid> _transactionAddresses = new List<Guid>();

        //dodatno
        private decimal _previousUSDValue; //azurira se nakon svakog poziva GetTotalUSDValue
        public decimal PreviousUSDValue => _previousUSDValue;
        //postotak promjene pojedinog asseta
        private readonly Dictionary<Guid, decimal> _previousAssetUSDValues = new Dictionary<Guid, decimal>();
        protected Wallet() //public ?? ili protected
        {
            Address = Guid.NewGuid();
            FungibleBalances = _fungibleBalances.AsReadOnly();
            TransactionAddresses = _transactionAddresses.AsReadOnly();
            _previousUSDValue = 0;
        }

        public void UpdateBalance(Guid assetAddress, decimal balance) //da se nebi izvana referenca mogla mijenjati
        {
            _fungibleBalances[assetAddress] = balance;
        }

        public void UpdateTransactionsAddresses(Guid address)
        {
            _transactionAddresses.Add(address);
        }

        //ode dodati metodu za podrzane fungible assets  koju svi moraju implementirati ali drukcije, svi sa svojom posebnom STATIC listom 
        public abstract void AddSupportedFungibleAssets(Guid supportedFungibleAssets);

        public abstract string GetWalletType();

        public virtual decimal GetTotalUSDValue(List<FungibleAsset> fungibleAssets, List<NonfungibleAsset> nonFungibleAssets)
        {
            decimal total = 0;
            foreach (var balance in FungibleBalances)
            {
                var asset = fungibleAssets.Find(a => a.Address == balance.Key);
                if (asset != null)
                    total += balance.Value * asset.UsdValue;
            }

            //za sve nonfungible, za bitcoin se nece primjeniti
            if (this is ISupportsNonFungible nftWallet)
            {
                foreach (var nftAddress in nftWallet.NonFungibleAssetsOwned)
                {
                    var nft = nonFungibleAssets.Find(a => a.Address == nftAddress);
                    if (nft != null)
                        total += nft.GetUSDValue(fungibleAssets);
                }
            }
            return total;
        }

        public decimal GetUSDValueChangePercente(List<FungibleAsset> fungibleAssets, List<NonfungibleAsset> nonFungibleAssets)
        {
            decimal currentValue = GetTotalUSDValue(fungibleAssets, nonFungibleAssets);
            decimal percentageChange = 0;

            if (_previousUSDValue != 0)
            {
                percentageChange = ((currentValue - _previousUSDValue) / _previousUSDValue) * 100;
            }
            _previousUSDValue = currentValue;
            return percentageChange;
        }

        //jaka metoda
        public decimal GetAssetUSDValueChangePercentage(Guid assetAddress, decimal currentUSDValue)
        {
            if (!_previousAssetUSDValues.ContainsKey(assetAddress))
            {
                _previousAssetUSDValues[assetAddress] = currentUSDValue;
                return 0;
            }
            decimal previousUSDValue = _previousAssetUSDValues[assetAddress];
            decimal percentageChange = previousUSDValue == 0 ? 0 : ((currentUSDValue - previousUSDValue) / Math.Abs(previousUSDValue)) * 100;
            _previousAssetUSDValues[assetAddress] = currentUSDValue;
            return percentageChange;
        }

        public abstract IReadOnlyList<Guid> GetSupportedFungibleAssets();

        //public virtual IReadOnlyList<Guid> GetSupportedNonFungibleAssets() => new List<Guid>().AsReadOnly(); ovo je za interface
    }
}


