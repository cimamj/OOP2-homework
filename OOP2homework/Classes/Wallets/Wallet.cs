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

        protected Wallet() //public ?? ili protected
        {
            Address = Guid.NewGuid();
            FungibleBalances = _fungibleBalances.AsReadOnly();
            TransactionAddresses = _transactionAddresses.AsReadOnly();
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
        
    }
}
