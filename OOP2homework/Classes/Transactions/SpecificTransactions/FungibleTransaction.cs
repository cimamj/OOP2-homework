using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2homework.Classes.Wallets; //bitno

namespace OOP2homework.Classes.Transactions.SpecificTransactions
{
    public class FungibleTransaction : Transaction
    {
        public decimal SenderInitialBalance { get; }
        public decimal SenderFinalBalance { get; }
        public decimal ReceiverInitialBalance { get; }
        public decimal ReceiverFinalBalance { get; }
        public FungibleTransaction(Guid fungibleAssetAddress, DateTime date, Guid senderWalletAddress, Guid receiverWalletAddress, Wallet senderWallet, Wallet receiverWallet, decimal amountTransferred, bool isRevoked) : base(fungibleAssetAddress, date, senderWalletAddress, receiverWalletAddress, isRevoked)
        {
            if (senderWallet == null || receiverWallet == null)
            {
                Console.WriteLine("Sender or receiver wallet cannot be null");
                return; //neka ne ide dalje
            }

            if (senderWallet.FungibleBalances.ContainsKey(AssetAddress))
            {
                SenderInitialBalance = senderWallet.FungibleBalances[AssetAddress];
                SenderFinalBalance = SenderInitialBalance - amountTransferred;
            }
                
            else SenderInitialBalance = 0m;

            if (receiverWallet.FungibleBalances.ContainsKey(AssetAddress))
            {
                ReceiverInitialBalance = receiverWallet.FungibleBalances[AssetAddress];
                ReceiverFinalBalance = ReceiverInitialBalance + amountTransferred;
            }

            else ReceiverInitialBalance = 0m;

            if (!isRevoked)
            {
                senderWallet.UpdateBalance(fungibleAssetAddress, SenderFinalBalance);
                receiverWallet.UpdateBalance(fungibleAssetAddress, ReceiverFinalBalance);
            }
        }

        
        
    }
}
