namespace VencingMachineApp.Models
{
    ///<summary>
    ///VencingMachine class represent vending machine which can accept coins to build credit and then buy item and receive change.
    ///</summary>
    public class VendingMachine
    {

        public static readonly Dictionary<string, Item> DEFAULT_ITEMS_DICTIONARY = new()
        {
            {"Juice", new Item("Juice", 70)},
            {"Ice Coffee", new Item("Ice Coffee", 72)},
            { "Lemonade", new Item("Lemonade", 59) }
        };

        private Dictionary<string, Item> ItemsDictionary;
        private CoinTray CoinTray;
        private PurchaseContext? PurchaseContext;

        ///<summary>
        ///Default vending machine constructor. Creates vending machine with coin tray with default coin nominals and 
        ///default list of items
        ///</summary>
        public VendingMachine()
        {
            CoinTray = new CoinTray();
            CoinTray.LoadAtTheStartOfTheDay();
            ItemsDictionary = DEFAULT_ITEMS_DICTIONARY;
        }

        ///<summary>
        ///Vending machine constructor. Creates vending machine with provided coin tray and 
        ///default list of items
        ///</summary>
        ///<param name="coinTray">Coin tray with different from default coin nominals</param>
        public VendingMachine(CoinTray coinTray)
        {
            ValidateCoinTray(coinTray); CoinTray = coinTray;
            CoinTray.LoadAtTheStartOfTheDay();
            ItemsDictionary = DEFAULT_ITEMS_DICTIONARY;
        }

        ///<summary>
        ///Vending machine constructor. Creates vending machine with provided coin tray and 
        ///proided list of items
        ///</summary>
        ///<param name="coinTray">Coin tray with different from default coin nominals</param>
        ///<param name="coinsInCompartments">Number of coins in departments loaded at machine start</param>
        ///<param name="itemsDictionary">List of item descriptions</param>
        public VendingMachine(CoinTray coinTray, int coinsInCompartments, Dictionary<string, Item> itemsDictionary)
        {
            ValidateCoinTray(coinTray); CoinTray = coinTray;
            CoinTray.LoadAtTheStartOfTheDay(coinsInCompartments);
            ValidateItemsDictionary(itemsDictionary); ItemsDictionary = itemsDictionary;
        }

        ///<summary>
        ///Add coin and get credit - the ammount of money already added to the vending machine
        ///</summary>
        ///<param name="coinNominal">Nominal of one single coin being added</param>
        ///<returns>Total number of money already added</returns>
        public int AddCoin(int coinNominal)
        {
            CoinTray.AddCoin(coinNominal);
            var purchaseContext = GetOrCreatePurchaseContext();
            purchaseContext.AddCredit(coinNominal);
            return purchaseContext.Credit;
        }

        ///<summary>
        ///Get credit - the ammount of money already added to the vending machine
        ///</summary>
        ///<returns>Total number of money already added</returns>
        public int GetCredit()
        {
            var purchaseContext = GetOrCreatePurchaseContext(true);
            return purchaseContext.Credit;
        }

        ///<summary>
        ///Bye item by selecting it's name from item dictionary and get the change. Coins should be added in advance.
        ///</summary>
        ///<param name="itemName">Name of item being purchased</param>
        ///<returns>Change</returns>
        public Change BuyItem(string itemName)
        {
            var purchaseContext = GetOrCreatePurchaseContext(true);
            var item = GetItem(itemName);
            ValidatePriceAgainstCredit(item.Price, purchaseContext.Credit);

            Console.WriteLine($"Bying {item.Name} for price {item.Price}");

            var changeAmount = purchaseContext.Credit - item.Price;

            Console.WriteLine($"Change is {changeAmount}");

            var change = CalculateChange(changeAmount);
            ResetPurchaseContext();
            return change;
        }

        private static void ValidateItemsDictionary(Dictionary<string, Item> itemsDictionary)
        {
            if (itemsDictionary == null || itemsDictionary.Count <= 0)
            {
                throw new ArgumentException("Items dictionary is null or empty");
            }
        }

        private static void ValidateCoinTray(CoinTray coinTray)
        {
            if (coinTray == null)
            {
                throw new ArgumentException("Coin tray can not be null", nameof(coinTray));
            }
        }

        private static void ValidatePriceAgainstCredit(int price, int credit)
        {
            if (price > credit)
            {
                throw new ArgumentException($"Credit {credit} is to low for item price {price}. " +
                    "Please add more coins.");
            }
        }

        private Item GetItem(string itemName)
        {
            return ItemsDictionary[itemName];
        }

        private PurchaseContext GetOrCreatePurchaseContext(Boolean validate = false)
        {
            if (validate) { ValidatePurchaseContext(); }
            PurchaseContext ??= new PurchaseContext();
            return PurchaseContext;
        }

        private void ValidatePurchaseContext()
        {
            if (PurchaseContext == null) { throw new ArgumentException("Credit is 0. Please add more coins."); }
        }

        private void ResetPurchaseContext()
        {
            PurchaseContext = null;
        }

        private Change CalculateChange(int changeAmount)
        {
            return CoinTray.GetChange(changeAmount);
        }
    }

    internal class PurchaseContext
    {
        public int Credit { get; private set; }

        public PurchaseContext()
        {
            Credit = 0;
        }

        public void AddCredit(int credit)
        {
            Credit += credit;
        }
    }
}
