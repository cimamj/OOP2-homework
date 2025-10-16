using System;
using System.Collections.Generic;
using OOP2homework.Classes.Wallets;
using OOP2homework.Classes;
using OOP2homework.Classes.Wallets.SpecificWallet;
using OOP2homework.Classes.Assets.SpecificAsset;
using OOP2homework.Interfaces;
using OOP2homework.Classes.Assets;

List<Wallet> wallets = new List<Wallet>();
List<FungibleAsset> fungibleAssets = new List<FungibleAsset>();
List<NonfungibleAsset> nonFungibleAssets = new List<NonfungibleAsset>();

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
        wallets.Add(wallet);
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

    ChooseWallet();

}


void ChooseWallet()
{
    Console.WriteLine("Enter wallet address you want to entry: ");
    string choice = Console.ReadLine()?.Trim();

    if(!Guid.TryParse(choice, out Guid selectedAddress))
    {
        Console.WriteLine("Nevažeći format adrese. Molimo unesite ispravan GUID.");
        return;
    }

    var selectedWallet = wallets.Find(a=>a.Address == selectedAddress);
    if (selectedWallet == null) {
        Console.WriteLine("Wallet s tom adresom nije pronađen.");
        return;
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

}