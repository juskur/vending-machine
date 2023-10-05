namespace VencingMachineApp.Models
{
    ///<summary>
    ///CoinCompartment class represents limited size compartment of coins of one nominal.
    ///</summary>
    public class CoinCompartment
    {
        public const int MAX_SIZE = 150;
        public const int COUNT_EMPTY_COMPARTMENT = 0;
        public int Nominal { get; }
        public int Count { get; private set; }

        ///<summary>
        ///CoinCompartment constructor
        ///</summary>
        ///<param name="nominal">Nominal of coins in coin compartment</param>
        public CoinCompartment(int nominal)
        {
            ValidateNominal(nominal); this.Nominal = nominal;
            this.Count = COUNT_EMPTY_COMPARTMENT;
        }

        ///<summary>
        ///Load provided count of coins into compartment
        ///</summary>
        ///<param name="count">Count of coins to be loaded</param>
        public void Load(int count)
        {
            ValidateCount(count); this.Count = count;
        }


        ///<summary>
        ///Add provided count of coins into compartment validating if there is till space in it.
        ///</summary>
        ///<param name="count">Count of coins to be added</param>
        public void Add(int count)
        {
            ValidateCountMoreThanZero(count);
            ValidateAddition(count);
            Count += count;
        }

        ///<summary>
        ///Calculated and try to get coins from this tray to cover provided amount
        ///</summary>
        ///<param name="ammount">Money amount to be covered</param>
        ///<returns>As much coins as possible to cover the ammount</returns>
        public int GetCoinsForAmmount(int ammount)
        {
            if (Nominal > ammount) return 0;
            var coinsForNominal = ammount / Nominal;
            var actualCoins = TryRetrieve(coinsForNominal);
            return actualCoins;

        }

        private int TryRetrieve(int count)
        {
            ValidateCountMoreThanZero(count);
            var actualRetrieved = 0;
            if (Count >= count)
            {
                actualRetrieved = count;
                Count -= count;
            }
            else
            {
                actualRetrieved = count - Count;
                Count = 0;
            }
            return actualRetrieved;
        }

        private void ValidateAddition(int count)
        {
            if (Count + count > MAX_SIZE)
            {
                throw new ArgumentException($"There is place to add {count} coins to compartment",
                                      nameof(count));
            }
        }

        private static void ValidateCount(int count)
        {
            ValidateCountMoreThanZero(count);
            if (count > MAX_SIZE)
            {
                throw new ArgumentException($"Count {count} should not exceed {MAX_SIZE}",
                                      nameof(count));

            }
        }

        private static void ValidateCountMoreThanZero(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException($"Count {count} should be greater than zero",
                                      nameof(count));
            }
        }

        private static void ValidateNominal(int nominal)
        {
            if (nominal <= 0)
            {
                throw new ArgumentException($"Nominal {0} should be greater than zero",
                                      nameof(nominal));
            }
        }
    }
}
