using System;
using System.Collections.Generic;
using OOP2homework.Classes.Wallets;
using OOP2homework.Classes;
using OOP2homework.Classes.Wallets.SpecificWallet;
using OOP2homework.Classes.Assets.SpecificAsset;
using OOP2homework.Interfaces;
using OOP2homework.Classes.Assets;
using System.ComponentModel.Design;
using OOP2homework.Classes.Transactions.SpecificTransactions;
//using System.Transactions;
using OOP2homework.Classes.Transactions; //ovo je jebeno bitno, kad je linija povise bila, nisam mogao ukljuciti u Transaction listu transackije specificne tipa Fungible nonfungible

List<Wallet> wallets = new List<Wallet>();
List<FungibleAsset> fungibleAssets = new List<FungibleAsset>();
List<NonfungibleAsset> nonFungibleAssets = new List<NonfungibleAsset>();
List<FungibleTransaction> fungibleTransactions = new List<FungibleTransaction>();
List<NonFungibleTransaction> nonFungibleTransactions = new List<NonFungibleTransaction>(); //zasto nemogu stvoriti listu transactions u koje cu spremati i jedan i drugi tip mogu li spremmiti u parent listu subove a za wallet radi spremanje subova
List<Transaction> transactions = new List<Transaction>();


InitializeFungibleAssets();
    InitializeNonFungibleAssets();
    InitializeWallets();
while (true)
{
    Console.WriteLine("CRYPTO WALLET MENU");
    Console.WriteLine("1. Kreiraj wallet");
    Console.WriteLine("2. Pristupi walletu");
    Console.WriteLine("0. Izlaz");
    Console.Write("Odaberi opciju: ");

    string izbor = Console.ReadLine();

    switch(izbor)
    {
        case "1":
            CreateWallet();
            break;

        case "2":
            AccessWallet();
            break;

        case "0":
            Console.WriteLine("\nHvala na korištenju aplikacije!");
            return;

        default:
            Console.WriteLine("Nevažeći unos. Pokušaj ponovo.");
            break;
    }
}

void CreateWallet()
{

    while (true)
    {
        Console.WriteLine("CRREATE WALLET");
        Console.WriteLine("1. Bitcoin wallet");
        Console.WriteLine("2. Ethereum wallet");
        Console.WriteLine("3. Solana wallet");
        Console.WriteLine("0. Izlaz");
        Console.Write("Odaberi opciju: ");

        string izbor = Console.ReadLine()?.Trim();
        switch (izbor) 
        {
            case "1": 
                CreateBitcoinWallet();
                break;
            case "2":
                //CreateEthereumWallet();
                break;
            case "3":
                //CreateSolanaWallet();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Nevažeći unos. Pokušaj ponovo.");
                break;
        }
    }
}

void InitializeFungibleAssets()
{
    var newBTCASSET = new FungibleAsset("Bitcoin", "BTC", 60000m);
    fungibleAssets.Add(newBTCASSET);
    fungibleAssets.Add(new FungibleAsset("Ethereum", "ETH", 2500m));
    fungibleAssets.Add(new FungibleAsset("Tether", "USDT", 1m));
    fungibleAssets.Add(new FungibleAsset("Binance Coin", "BNB", 400m));
    fungibleAssets.Add(new FungibleAsset("Cardano", "ADA", 0.35m));
    fungibleAssets.Add(new FungibleAsset("Solana", "SOL", 150m));
    fungibleAssets.Add(new FungibleAsset("Ripple", "XRP", 0.5m));
    fungibleAssets.Add(new FungibleAsset("Polkadot", "DOT", 6m));
    fungibleAssets.Add(new FungibleAsset("Dogecoin", "DOGE", 0.2m));
    fungibleAssets.Add(new FungibleAsset("Shiba Inu", "SHIB", 0.00002m));
    //newBTCASSET.ChangeUSDValue();
}

void InitializeNonFungibleAssets()
{
    var btcAsset = fungibleAssets.Find(a => a.Symbol == "BTC");
    var ethAsset = fungibleAssets.Find(a => a.Symbol == "ETH");
    if (btcAsset == null || ethAsset == null) return;

    for (int i = 1; i <= 10; i++)
    {
        nonFungibleAssets.Add(new NonfungibleAsset($"NFT BTC #{i}", 0.1m * i, btcAsset.Address)); //vrijednost svakog se povecava
        nonFungibleAssets.Add(new NonfungibleAsset($"NFT ETH #{i}", 2m * i, ethAsset.Address));
    }
}

void InitializeWallets()
{
    Random rand = new Random();
    var btcAsset = fungibleAssets.Find(a => a.Symbol == "BTC");
    var ethAsset = fungibleAssets.Find(a => a.Symbol == "ETH");
    var solAsset = fungibleAssets.Find(a => a.Symbol == "SOL");
    var usdtAsset = fungibleAssets.Find(a => a.Symbol == "USDT");
    if (btcAsset == null || ethAsset == null || solAsset == null || usdtAsset == null) return;

    // Kreiraj 3 Bitcoin walleta
    for (int i = 0; i < 3; i++)
    {
        var wallet = new BitcoinWallet();
        wallet.AddSupportedFungibleAssets(btcAsset.Address);
        wallet.AddSupportedFungibleAssets(usdtAsset.Address);
        wallet.UpdateBalance(btcAsset.Address, (decimal)(rand.NextDouble() * 5)); // 0-5 BTC
        wallet.UpdateBalance(usdtAsset.Address, (decimal)(rand.NextDouble() * 1000)); // 0-1000 USDT
        wallets.Add(wallet);//a ode radi
    }

    // Kreiraj 3 Ethereum walleta
    for (int i = 0; i < 3; i++)
    {
        var wallet = new EthereumWallet();
        wallet.AddSupportedFungibleAssets(ethAsset.Address);
        wallet.AddSupportedFungibleAssets(usdtAsset.Address);
        wallet.UpdateBalance(ethAsset.Address, (decimal)(rand.NextDouble() * 50)); // 0-50 ETH
        wallet.UpdateBalance(usdtAsset.Address, (decimal)(rand.NextDouble() * 1000)); // 0-1000 USDT
                                                                                      // Dodaj 2-3 random NFT-a
        for (int j = 0; j < rand.Next(2, 4); j++)
        {
            var nft = nonFungibleAssets[rand.Next(nonFungibleAssets.Count)];
            wallet.AddSupportedNonFungibleAssets(nft.Address);
            wallet.AddNonFungibleAssetsOwned(nft.Address);
        }
        wallets.Add(wallet);
    }

    // Kreiraj 3 Solana walleta
    for (int i = 0; i < 3; i++)
    {
        var wallet = new SolanaWallet();
        wallet.AddSupportedFungibleAssets(solAsset.Address);
        wallet.AddSupportedFungibleAssets(usdtAsset.Address);
        wallet.UpdateBalance(solAsset.Address, (decimal)(rand.NextDouble() * 100)); // 0-100 SOL
        wallet.UpdateBalance(usdtAsset.Address, (decimal)(rand.NextDouble() * 1000)); // 0-1000 USDT
                                                                                      // Dodaj 2-3 random NFT-a
        for (int j = 0; j < rand.Next(2, 4); j++)
        {
            var nft = nonFungibleAssets[rand.Next(nonFungibleAssets.Count)];
            wallet.AddSupportedNonFungibleAssets(nft.Address);
            wallet.AddNonFungibleAssetsOwned(nft.Address);
        }
        wallets.Add(wallet);
    }

    Console.WriteLine("Inicijalizirano 9 wallet-a, 10 fungible i 20 non-fungible asset-a.");
}
void CreateBitcoinWallet()
{
    var newBitcoinWallet = new BitcoinWallet();
    wallets.Add(newBitcoinWallet);
    //prvo nadi taj asset u predefiniramim assetima jer njega stvara, od istog ces uzeti adresu
    Console.WriteLine("Unesite label fungible asseta kojeg zelite dodati : (BTC,ETH...) ");
    string label = Console.ReadLine()?.Trim().ToUpper();
    var btcAsset = fungibleAssets.Find(a => a.Symbol == label);
    if (btcAsset == null)
    {
        Console.WriteLine("Greška: BTC asset nije pronađen.");
        return;
    }


    //pozivas preko imena klase a ne preko imena objekta jer nije instacirana static je u pitanju
    if (!BitcoinWallet.SupportedFungibleAssets.Contains(btcAsset.Address)) 
    {
        newBitcoinWallet.AddSupportedFungibleAssets(btcAsset.Address);
    }


    decimal amount;
    while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
    {
        Console.Write("Neispravan unos. Unesi brojčanu vrijednost veću od 0: ");
    }
    newBitcoinWallet.UpdateBalance(btcAsset.Address, amount);
    //ispis u usd
    decimal usdValue = amount * btcAsset.UsdValue;
    Console.WriteLine($"Kreiran Bitcoin wallet s adresom {newBitcoinWallet.Address} i balansom {amount} {btcAsset.Symbol} (vrijednost: {usdValue:F2} USD).");
}

//eth i solana walleti

void AccessWallet()
{
    if (wallets.Count == 0)
    {
        Console.WriteLine("Nema dostupnih wallet-a.");
        return;
    }

    foreach (var asset in fungibleAssets)
    {
        asset.ChangeUSDValue();
    }//simulacija promjene stope

    Console.WriteLine("Dostupni walleti : ");
    foreach (var wallet in wallets)
    {
        decimal totalUSDValue = wallet.GetTotalUSDValue(fungibleAssets, nonFungibleAssets);
        decimal percentageChange = wallet.GetUSDValueChangePercente(fungibleAssets, nonFungibleAssets);
        Console.Write($"Tip: {wallet.GetWalletType()}, Adresa: {wallet.Address}, Ukupna USD vrijednost: {totalUSDValue:F2}$, Postotak promjene {percentageChange:F2}\n"); //je li ovo samo baca - i+
    }



    Menu();
 

}


Wallet ChooseWallet()
{
    Console.WriteLine("Enter wallet address you want to entry: ");
    string choice = Console.ReadLine()?.Trim();

    if(!Guid.TryParse(choice, out Guid selectedAddress))
    {
        Console.WriteLine("Nevažeći format adrese. Molimo unesite ispravan GUID.");
        return null; 
    }

    var selectedWallet = wallets.Find(a=>a.Address == selectedAddress);
    if (selectedWallet == null) {
        Console.WriteLine("Wallet s tom adresom nije pronađen.");
        return null;
    }

    decimal totalUSDValue = selectedWallet.GetTotalUSDValue(fungibleAssets, nonFungibleAssets);
    Console.WriteLine($"\nPristup walletu: {selectedWallet.GetWalletType()}, Adresa: {selectedWallet.Address}");
    Console.WriteLine($"Ukupna USD vrijednost walleta: {totalUSDValue:F2} USD\n");

    //detalji o assetima
    Console.WriteLine("Fungible asseti:");
    bool hasAssets = false;
    foreach (var balance in selectedWallet.FungibleBalances) 
    {
        var asset = fungibleAssets.Find(a=>a.Address == balance.Key);
        if (asset != null)
        {
            decimal usdValue = balance.Value * asset.UsdValue;
            decimal percentageChange = selectedWallet.GetAssetUSDValueChangePercentage(asset.Address, usdValue);
            Console.WriteLine($"Adresa {asset.Address}, Ime {asset.Name}, Oznaka {asset.Symbol}, Vrijednost {balance.Value:F8}, Vrijednost u USD {usdValue:F2}$, Stopa promjene USD {percentageChange}$ \n");
            hasAssets = true;
        }
    }

    Console.WriteLine("NonFungible asseti:");
    if (selectedWallet is ISupportsNonFungible nftWallet)
    {
        Console.WriteLine("Non-fungible asseti:");
        foreach(var nftAddress in nftWallet.NonFungibleAssetsOwned)
        {
            var nft = nonFungibleAssets.Find(a => a.Address == nftAddress);
            if (nft != null) 
            {
                var relatedAsset = fungibleAssets.Find(a=>a.Address == nft.FungibleAssetAddress);
                decimal usdValue = nft.GetUSDValue(fungibleAssets);
                decimal percentageChange = selectedWallet.GetAssetUSDValueChangePercentage(nft.Address, usdValue);
                Console.WriteLine($"Adresa: {nft.Address}");
                Console.WriteLine($"Ime asseta: {nft.Name}");
                Console.WriteLine($"Oznaka asseta: N/A");
                Console.WriteLine($"Vrijednost u fungible assetu: {nft.Value:F8} {(relatedAsset != null ? relatedAsset.Symbol : "N/A")}");
                Console.WriteLine($"Ukupna USD vrijednost: {usdValue:F2} USD");
                Console.WriteLine($"Postotak promjene: {percentageChange:F2}%");
                hasAssets = true;

            }
        }
    }
    if (!hasAssets)
    {
        Console.WriteLine("Ovaj wallet nema asseta.");
    }

    return selectedWallet;

}


void Menu()
{
    Wallet selectedWallet = null;
    Console.WriteLine("\nOdaberite opciju:");
    Console.WriteLine("1. Wallet and asset details");
    Console.WriteLine("2. Izlaz ");
    Console.Write("Odaberi opciju: ");

    string izbor = Console.ReadLine();

    switch (izbor)
    {
        case "1":
            selectedWallet = ChooseWallet();
            if (selectedWallet == null) 
                break;
            while(true)
            {

                Console.WriteLine($"\nTrenutno gledate wallet: {selectedWallet.Address}");
                Console.WriteLine("1. Transfer asseta");
                Console.WriteLine("2. Transaction history");
                Console.WriteLine("3. Opozovi transakciju");
                Console.WriteLine("4. Povratak u glavni meni");
                Console.Write("Odaberi opciju: ");
                string subChoice = Console.ReadLine();
                if (subChoice == "1")
                {
                    Transfer(selectedWallet);
                }
                else if(subChoice == "2")
                {
                    TransactionHistory(selectedWallet);
                }
                else if(subChoice == "3")
                {
                    RevokeTransaction(selectedWallet);
                }
                else
                    return;
            }
        case "2":
            return;
        default:
            Console.WriteLine("Nevažeći unos. Pokušaj ponovo.");
            break;

    }
}

void Transfer(Wallet senderWallet)
{
    Console.WriteLine("Unesite adresu walleta kojem šaljete asset: ");
    string walletAddressChoice = Console.ReadLine()?.Trim();
  
    if (!Guid.TryParse(walletAddressChoice, out Guid selectedReceiverWalletAddress))
    {
        Console.WriteLine("Nevažeći format adrese walleta. Molimo unesite ispravan GUID.");
        return;
    }
    var receiverWallet = wallets.Find(a => a.Address == selectedReceiverWalletAddress);
    if (receiverWallet == null) 
    {
        Console.WriteLine("Wallet primatelja nije pronađen.");
        return;
    }

    Console.WriteLine("Unesite adresu asseta kojeg šaljete: ");
    string assetAddressChoice = Console.ReadLine()?.Trim();

    if (!Guid.TryParse(assetAddressChoice, out Guid selectedAssetAddress))
    {
        Console.WriteLine("Nevažeći format adrese asseta. Molimo unesite ispravan GUID.");
        return;
    }


    var fungibleAsset = fungibleAssets.Find(a => a.Address == selectedAssetAddress);
    var nonFungibleAsset = nonFungibleAssets.Find(a => a.Address == selectedAssetAddress);
    decimal amount;

    if (fungibleAsset == null && nonFungibleAsset == null)
    {
        Console.WriteLine("Asset nije pronađen.");
        return;
    }

    if (fungibleAsset != null)
    {
        if(!senderWallet.GetSupportedFungibleAssets().Contains(selectedAssetAddress) || !receiverWallet.GetSupportedFungibleAssets().Contains(selectedAssetAddress))
        {
            Console.WriteLine("Sender ili receiver ne podržava ovaj fungible asset.");
            return;
        }

        Console.WriteLine($"Unesite količinu {fungibleAsset.Symbol} fungible asseta: ");

        if (!Decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0) //provjera za zero
        {
            Console.WriteLine("Nevazeći unos \n");
        }

        //ima li sender para na ovome racunu
        if (!senderWallet.FungibleBalances.ContainsKey(selectedAssetAddress) || senderWallet.FungibleBalances[selectedAssetAddress] < amount)
        {
            Console.WriteLine("Nemas bele \n");
            return;
        }

        var transaction = new FungibleTransaction(selectedAssetAddress, DateTime.Now, senderWallet.Address, receiverWallet.Address, senderWallet, receiverWallet, amount, false);
        transactions.Add(transaction);
        senderWallet.UpdateTransactionsAddresses(transaction.Id);
        receiverWallet.UpdateTransactionsAddresses(transaction.Id);
        fungibleAsset.ChangeUSDValue();
        Console.WriteLine($"Uspješno preneseno {amount} {fungibleAsset.Symbol} na wallet {receiverWallet.Address}.");
    }
    //pokazi  sto sve moze ponuditi od asseta za prijenos
    //nonfungible
    else
    {
        if(!(senderWallet is ISupportsNonFungible senderNFTwallet) || !(receiverWallet is ISupportsNonFungible receiverNFTwallet))
        {
            Console.WriteLine("Sender ili receiver ne podržava non-fungible assete.");
            return;
        }

        if (!senderNFTwallet.NonFungibleAssetsOwned.Contains(selectedAssetAddress))
            {
            Console.WriteLine("Sender ne posjeduje ovaj NFT.");
            return;
        }

        if (!receiverNFTwallet.GetSupportedNonFungibleAssets().Contains(selectedAssetAddress)) //Suported se podrazumijeva
        {
            Console.WriteLine("Receiver ne podržava ovaj NFT.");
            return;
        }

        var transaction = new NonFungibleTransaction(selectedAssetAddress, DateTime.Now, senderWallet.Address, receiverWallet.Address, false);
        transactions.Add(transaction);
        receiverNFTwallet.AddNonFungibleAssetsOwned(selectedAssetAddress); //tipa Wallet receiverWallet ne radi nego kad ga castas preko is u NFTwallet, on ima ovu metodu
        senderNFTwallet.RemoveNonFungibleAssetsOwned(selectedAssetAddress);
        

        senderWallet.UpdateTransactionsAddresses(transaction.Id);
        receiverWallet.UpdateTransactionsAddresses(transaction.Id);
        
        //senderu makni , recevieru primi, dvi nove metode

        var relatedFungibleAsset = fungibleAssets.Find(a=>a.Address == nonFungibleAsset.FungibleAssetAddress);
        if (relatedFungibleAsset != null)
        {
            relatedFungibleAsset.ChangeUSDValue();
        }

        Console.WriteLine($"Uspješno prenesen NFT {nonFungibleAsset.Name} na wallet {receiverWallet.Address}.");
    }

}


void TransactionHistory(Wallet selectedWallet)
{
    Console.WriteLine($"Sve transakcije {selectedWallet.Address} su sljedece: ");


    var showTransactions = transactions
        .Where(t => t.SenderWalletAddress == selectedWallet.Address || t.ReceiverWalletAddress == selectedWallet.Address)
        .OrderByDescending(t => t.Date)
        .ToList();

    if (showTransactions.Count() == 0)
    {
        Console.WriteLine("Nema transakcija za ovaj wallet.");
        return;
    }

    foreach (var transaction in showTransactions)
    {
        Console.WriteLine($"ID: {transaction.Id}");
        Console.WriteLine($"Datum i vrijeme: {transaction.Date:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"Pošiljatelj: {transaction.SenderWalletAddress}");
        Console.WriteLine($"Primatelj: {transaction.ReceiverWalletAddress}");
        //fungible ili nonfungible, interface ili postojecu listu asseta usporediti s adresama

        Asset asset = fungibleAssets.Find(a => a.Address == transaction.AssetAddress);
        if (asset != null && asset is FungibleAsset fungibleAsset)
        {
            Console.WriteLine($"Asset: {fungibleAsset.Name} ({fungibleAsset.Symbol})");

            //{ Console.WriteLine($"Amount: {selectedWallet.FungibleBalances[fungibleAsset.Address]}"); }  ovaj amount se mijenja
            if (transaction is FungibleTransaction ft) //mozda ova provjera viska, jer ako je asset fungible, a usporeden je s adresom iz ove transkacije, i ona je fungible
            {
                Console.WriteLine($"Amount: {ft.Amount:F8}");
            }

        }
        else
        { 
        asset = nonFungibleAssets.Find(a => a.Address == transaction.AssetAddress);
        Console.WriteLine($"Asset: {asset.Name}");
        }
        Console.WriteLine($"Is revoked: {transaction.IsRevoked}");
    }
    
}



void RevokeTransaction(Wallet selectedWallet)
{
    Console.WriteLine("Enter transaction id you want to revoke ");

    if(!Guid.TryParse(Console.ReadLine(), out Guid transactionId))
      {
        Console.WriteLine("Nevažeći format adrese. Molimo unesite ispravan GUID.");
        return;
      }

    var revokeTransaction = transactions.Find(t=>t.Id ==  transactionId);
    if (revokeTransaction == null)
    {
        Console.WriteLine("Transakcija s navedenim ID-om nije pronađena.");
        return;
    }
    // Provjera je li selectedWallet pošiljatelj
    if (revokeTransaction.SenderWalletAddress != selectedWallet.Address)
    {
        Console.WriteLine("Samo pošiljatelj transakcije može je opozvati.");
        return;
    }
    //var senderWallet = wallets.Find(w => w.Address == revokeTransaction.SenderWalletAddress); selectedWallet je senderWallet
    var senderWallet = selectedWallet;
    var receiverWallet = wallets.Find(w => w.Address == revokeTransaction.ReceiverWalletAddress);
    //if (senderWallet == null || receiverWallet == null)
    //{
    //    Console.WriteLine("Wallet pošiljatelja ili primatelja nije pronađen.");
    //    return;
    //} nepotrebno ima projvere vec da je null nebi bilo transakcije

 
    if (revokeTransaction is FungibleTransaction fungibleTransaction)
    {
        //rjesavanje balansa
        senderWallet.UpdateBalance(fungibleTransaction.AssetAddress, fungibleTransaction.SenderInitialBalance);
        receiverWallet.UpdateBalance(fungibleTransaction.AssetAddress, fungibleTransaction.ReceiverInitialBalance);
    }
    else
    {
        if (senderWallet is ISupportsNonFungible nftWallet && receiverWallet is ISupportsNonFungible nftWalletRec)
        {
            nftWallet.AddNonFungibleAssetsOwned(revokeTransaction.AssetAddress);
            nftWalletRec.RemoveNonFungibleAssetsOwned(revokeTransaction.AssetAddress);
        }
    }
    revokeTransaction.Revoke();
    Console.WriteLine($"Transakcija {revokeTransaction.Id} uspješno opozvana.");
}