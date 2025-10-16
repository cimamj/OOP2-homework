using System;
using System.Collections.Generic;
using OOP2homework.Classes.Wallets;
using OOP2homework.Classes;
using OOP2homework.Classes.Wallets.SpecificWallet;
using OOP2homework.Classes.Assets.SpecificAsset;

List<Wallet> wallets = new List<Wallet>();
List<FungibleAsset> fungibleAssets = new List<FungibleAsset>();

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
            //JoinWallet();
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

    InitializeFungibleAssets();
    while (true)
    {
        Console.WriteLine("CRREATE WALLET");
        Console.WriteLine("1. Bitcoin wallet");
        Console.WriteLine("2. Ethereum wallet");
        Console.WriteLine("3. Solana wallet");
        Console.WriteLine("0. Izlaz");
        Console.Write("Odaberi opciju: ");

        string izbor = Console.ReadLine();
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
    fungibleAssets.Add(new FungibleAsset("Bitcoin", 60000m, "BTC",20000));
    //fungibleAssets.Add(new FungibleAsset("Ethereum", "ETH", 2500m));
    //fungibleAssets.Add(new FungibleAsset("Tether", "USDT", 1m));
    //fungibleAssets.Add(new FungibleAsset("Binance Coin", "BNB", 400m));
    //fungibleAssets.Add(new FungibleAsset("Cardano", "ADA", 0.35m));
    //fungibleAssets.Add(new FungibleAsset("Solana", "SOL", 150m));
    //fungibleAssets.Add(new FungibleAsset("Ripple", "XRP", 0.5m));
    //fungibleAssets.Add(new FungibleAsset("Polkadot", "DOT", 6m));
    //fungibleAssets.Add(new FungibleAsset("Dogecoin", "DOGE", 0.2m));
    //fungibleAssets.Add(new FungibleAsset("Shiba Inu", "SHIB", 0.00002m));
}



void CreateBitcoinWallet()
{
    var newBitcoinWallet = new BitcoinWallet();
    wallets.Add(newBitcoinWallet);
    //prilikom punjenja metoda novog walleta trebas inicijalizrati i asset(za balans) i transaction
    //Koristi adrese predefiniranih fungible asseta sa njima barataj u smislu SuportedASsets, npr u bitcoin wallet 
    //prvo nadi taj asset u predefiniramim assetima jer njega stvara, od istog ces uzeti adresu
    var btcAsset = fungibleAssets.Find(a => a.Label == "BTC");
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
 //ali prije dodavanja jel postoji

    //dodaj mu iznos i vrijednost u dolarima
    Console.Write("Unesi vrijednost u dolarima : ");
    decimal value;
    while(!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
    {
            Console.Write("Neispravan unos. Unesi brojčanu vrijednost veću od 0: ");
    }
    Console.Write("Unesi iznos : ");
    decimal amount;
    while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
    {
        Console.Write("Neispravan unos. Unesi brojčanu vrijednost veću od 0: ");
    }
   //kako da sad dodam amount i vrijednost
    fungibleAssets.Add(newFungibleAsset);
    //sad idemo dalje sa walletom
    //newBitcoinWallet.UpdateBalance(newFungibleAsset.Address, value);

}