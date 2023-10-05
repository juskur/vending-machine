using NUnit.Framework;

namespace VencingMachineApp.Models
{
    [TestFixture]
    public class CoinCompartmentTest
    {

        [Test]
        public void CanNotCreateWithIncorrectNominal()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var actual = new CoinCompartment(0);
            });
        }

        [Test]
        public void CanCreateWithCorrectNominal()
        {
            Assert.DoesNotThrow(() =>
            {
                var actual = new CoinCompartment(50);
            });
        }

        [Test]
        public void CanNotLoadWithIncorrectCount()
        {
            var actual = new CoinCompartment(50);
            Assert.Throws<ArgumentException>(() =>
            {
                actual.Load(-1);
            });
        }

        [Test]
        public void CanNotLoadWithTooHighCount()
        {
            var actual = new CoinCompartment(50);
            Assert.Throws<ArgumentException>(() =>
            {
                actual.Load(151);
            });
        }

        [Test]
        public void CanLoadWithCorrectCount()
        {
            var actual = new CoinCompartment(50);
            Assert.DoesNotThrow(() =>
            {
                actual.Load(100);
            });
        }

        [Test]
        public void CanAddWhenThereIsStillSpace()
        {
            var actual = new CoinCompartment(50);
            actual.Load(149);
            Assert.DoesNotThrow(() =>
            {
                actual.Add(1);
            });

        }

        [Test]
        public void CanNotAddWhenThereIsNoSpace()
        {
            var actual = new CoinCompartment(50);
            actual.Load(149);
            Assert.Throws<ArgumentException>(() =>
            {
                actual.Add(2);
            });
        }

        [Test]
        public void DoesNotRetrieveCoinsIfAmountIsLessThanNominal()
        {
            var coinCompartment = new CoinCompartment(10);
            coinCompartment.Load(1);
            var actual = coinCompartment.GetCoinsForAmmount(5);
            Assert.IsTrue(actual == 0, "Coin retrieved though amount is less than nominal");
        }

        [Test]
        public void CanRetrieveOneCoinForDoubleAmountIfOneCoinOnly()
        {
            var coinCompartment = new CoinCompartment(10);
            coinCompartment.Load(1);
            var actual = coinCompartment.GetCoinsForAmmount(20);
            Assert.IsTrue(actual == 1, "Not one coin retrieved though it was only one coin in compartment");
        }

        [Test]
        public void CanRetrieveTwoCoinsForDoubleAmount()
        {
            var coinCompartment = new CoinCompartment(10);
            coinCompartment.Load(2);
            var actual = coinCompartment.GetCoinsForAmmount(20);
            Assert.IsTrue(actual == 2, "Not two coins retrieved for double amount");
        }
    }
}
