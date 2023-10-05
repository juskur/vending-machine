namespace VencingMachineApp.Models
{
    ///<summary>
    ///Change class represents information about how many coins of what nominal are returned to buyer
    ///</summary>
    public class Change
    {
        private Dictionary<int, int> nominalCountMap = new Dictionary<int, int>();

        ///<summary>
        ///Add coins to change
        ///</summary>
        ///<param name="nominal">Nominal of coins</param>
        ///<param name="coins">Number of coins of provided nominal</param>
        public void AddCoins(int nominal, int coins)
        {
            nominalCountMap.Add(nominal, coins);
        }

        ///<summary>
        ///Get change description.
        ///</summary>
        ///<returns>Returns string describing how many coins of what nominal are in change.</returns>
        public override string ToString()
        {
            var changeDescription = "";
            foreach (var nominal in nominalCountMap.Keys)
            {
                var coinCount = nominalCountMap[nominal];
                if (coinCount > 0)
                {
                    changeDescription += $"{coinCount} coins of {nominal} nominal. ";
                }
            }
            if (changeDescription.Length == 0)
            {
                return "No change.";
            }
            return $"Your change: {changeDescription}";
        }
    }
}
