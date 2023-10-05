namespace VencingMachineApp.Models
{
    ///<summary>
    ///CoinTary class represents coin tray with many coin compartments.
    ///</summary>
    public class CoinTray
    {
        public static readonly int[] DEFAULT_SORTED_COIN_NOMINALS = new int[] { 1, 2, 5, 10, 20, 50 };
        public const int COINS_IN_COMPARTMENTS_AT_THE_START_OF_THE_DAY = 100;

        private readonly Dictionary<int, CoinCompartment> CointCompartments = new();
        private readonly int[] SortedCoinNominals;

        ///<summary>
        ///CoinCompartment constructor. Creates coin tray with default coin nominals
        ///</summary>
        public CoinTray()
        {
            SortedCoinNominals = DEFAULT_SORTED_COIN_NOMINALS;
            CreateCoinCompartments();
        }

        ///<summary>
        ///CoinCompartment constructor
        ///</summary>
        ///<param name="sortedCoinNominals">Array of coin nominals sorted in ascending order</param>
        public CoinTray(int[] sortedCoinNominals)
        {
            ValidateCoinNominals(sortedCoinNominals);
            SortedCoinNominals = sortedCoinNominals;
            CreateCoinCompartments();
        }

        ///<summary>
        ///Calculated and try to get coins from this tray to cover provided amount
        ///</summary>
        ///<param name="ammount">Money amount to be covered</param>
        ///<returns>As much coins as possible to cover the ammount</returns>
        public Change GetChange(int ammount)
        {
            var change = new Change();
            if (ammount <= 0) return change;
            var ammountLeft = ammount;
            for (int i = SortedCoinNominals.Length - 1; i >= 0; i--)
            {
                var coinCompartment = CointCompartments[SortedCoinNominals[i]];
                var actualCoins = coinCompartment.GetCoinsForAmmount(ammountLeft);
                if (actualCoins > 0)
                {
                    ammountLeft = ammountLeft - (actualCoins * coinCompartment.Nominal);
                    change.AddCoins(coinCompartment.Nominal, actualCoins);
                }
            }
            if (ammountLeft > 0)
            {
                throw new ArgumentException("There was not enough coins to give change. " +
                    $"Please contact vending machine operator to pay you missing {ammountLeft}");
            }
            return change;
        }

        public void LoadAtTheStartOfTheDay(int coinsInCompartments = COINS_IN_COMPARTMENTS_AT_THE_START_OF_THE_DAY)
        {
            foreach (var compartment in CointCompartments.Values)
            {
                compartment.Load(coinsInCompartments);
            }
        }

        private static void ValidateCoinNominals(int[] sortedCoinNominals)
        {
            ValidateCoinNominalsNotNull(sortedCoinNominals);
            ValidateCoinNominalsPositiveNumbers(sortedCoinNominals);
        }

        private static void ValidateCoinNominalsPositiveNumbers(int[] coinNominals)
        {
            foreach (var coinNominal in coinNominals)
            {
                if (coinNominal <= 0)
                {
                    throw new ArgumentException($"Coin nominal can not be less than 1: {coinNominal}");
                }
            }
        }

        private static void ValidateCoinNominalsNotNull(int[] coinNominals)
        {
            if (coinNominals == null)
            {
                throw new ArgumentException("Array of coin nominals can not be null");
            }
        }

        private void CreateCoinCompartments()
        {
            foreach (var coinNominal in SortedCoinNominals)
            {
                CointCompartments.Add(coinNominal, new CoinCompartment(coinNominal));
            }
        }

        internal void AddCoin(int coinNominal)
        {
            ValidateCoinNominal(coinNominal);
            CointCompartments[coinNominal].Add(1);
        }

        private void ValidateCoinNominal(int coinNominal)
        {
            if (!SortedCoinNominals.Contains(coinNominal))
            {
                throw new ArgumentException($"Unknow coin nominal {coinNominal}", nameof(coinNominal));
            }
        }
    }
}
