using NUnit.Framework;
using VencingMachineApp.Models;

namespace VendingMachineTest.Models
{
    [TestFixture]
    internal class VendingMachineTest
    {
        public static readonly Dictionary<string, Item> MINIMAL_ITEMS_DICTIONARY = new()
        {
            { "Gum", new Item("Gum", 11) }
        };
        public static readonly int[] MINIMAL_SORTED_COIN_NOMINALS = new int[] { 1, 2, 5 };
        public static readonly CoinTray MINIMAL_COIN_TRAY = new CoinTray(MINIMAL_SORTED_COIN_NOMINALS);

        [Test]
        public void CanCreateMinimalVendingMachine()
        {
            Assert.DoesNotThrow(() =>
            {
                var minimalVendingMachine = new VendingMachine(MINIMAL_COIN_TRAY, 1, MINIMAL_ITEMS_DICTIONARY);
            });
        }

        [Test]
        public void CanAddCredit()
        {
            var minimalVendingMachine = new VendingMachine(MINIMAL_COIN_TRAY, 1, MINIMAL_ITEMS_DICTIONARY);
            Assert.DoesNotThrow(() =>
            {
                minimalVendingMachine.AddCoin(5);
            });
        }

        [Test]
        public void CanNotAddCoinOfInvalidNominal()
        {
            var minimalVendingMachine = new VendingMachine(MINIMAL_COIN_TRAY, 1, MINIMAL_ITEMS_DICTIONARY);
            Assert.Throws<ArgumentException>(() =>
            {
                minimalVendingMachine.AddCoin(10);
            });
        }

        [Test]
        public void CanGetChangeEnoughCoins()
        {
            var minimalVendingMachine = new VendingMachine(MINIMAL_COIN_TRAY, 1, MINIMAL_ITEMS_DICTIONARY);
            minimalVendingMachine.AddCoin(5);
            minimalVendingMachine.AddCoin(5);
            minimalVendingMachine.AddCoin(2);
            minimalVendingMachine.AddCoin(2);
            var actualChange = minimalVendingMachine.BuyItem("Gum");
            Assert.IsTrue("Your change: 1 coins of 2 nominal. 1 coins of 1 nominal. ".Equals(actualChange.ToString()));
        }
        [Test]
        public void CanNotGetChangeWhenEnoughCoins()
        {
            var minimalVendingMachine = new VendingMachine(MINIMAL_COIN_TRAY, 1, MINIMAL_ITEMS_DICTIONARY);
            minimalVendingMachine.AddCoin(5);
            minimalVendingMachine.AddCoin(5);
            minimalVendingMachine.AddCoin(5);
            minimalVendingMachine.AddCoin(5);
            var expectedException = Assert.Throws<ArgumentException>(() =>
            {
                var actualChange = minimalVendingMachine.BuyItem("Gum");
            });
            Assert.IsTrue("There was not enough coins to give change. Please contact vending machine operator to pay you missing 1".Equals(expectedException == null ? "" : expectedException.Message));
        }
    }
}
