using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOP2homework.Classes.Transactions
{
    public abstract class Transaction
    {
        public Guid Id { get; }
        public Guid AssetAddress { get; }
        public DateTime Date {  get; }          
        public Guid SenderWalletAddress { get; }
        public Guid ReceiverWalletAddress { get; }

        public bool IsRevoked { get; private set;  } = false; //private set da mozes i van konstruktora, unutar klase, mijenjati dakle kroz metode

        protected Transaction(Guid assetAddress, DateTime date, Guid senderWalletAddress, Guid receiverWalletAddress, bool isRevoked)
        { 
            IsRevoked = isRevoked;
            Id = Guid.NewGuid();
            AssetAddress = assetAddress;
            Date = date;
            SenderWalletAddress = senderWalletAddress;
            ReceiverWalletAddress = receiverWalletAddress;
        }
    }
}
