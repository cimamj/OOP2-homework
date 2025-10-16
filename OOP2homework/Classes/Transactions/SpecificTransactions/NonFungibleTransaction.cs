using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2homework.Classes.Transactions.SpecificTransactions
{
    public class NonFungibleTransaction : Transaction
    {
        public NonFungibleTransaction(Guid nonFungibleAssetAddress, DateTime date, Guid senderWalletAddress, Guid receiverWalletAddress, bool isRevoked) : base(nonFungibleAssetAddress, date, senderWalletAddress, receiverWalletAddress, isRevoked) { } 
    }
}
