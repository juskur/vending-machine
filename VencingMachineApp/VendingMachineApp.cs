using VencingMachineApp.Models;

namespace VencingMachineApp
{

    ///<summary>
    ///VencingMachineApp class starts vending machine, adds coins and receives change. For more use cases please see
    ///corresponding test class
    ///</summary>
    public class VendingMachineApp
    {
        private static void Main(string[] args)
        {

            var vendingMachine = new VendingMachine();
            vendingMachine.AddCoin(50);
            Console.WriteLine(String.Format("Credit is {0}", vendingMachine.GetCredit()));
            vendingMachine.AddCoin(20);
            Console.WriteLine(String.Format("Credit is {0}", vendingMachine.GetCredit()));
            vendingMachine.AddCoin(20);
            Console.WriteLine(String.Format("Credit is {0}", vendingMachine.GetCredit()));
            var change = vendingMachine.BuyItem("Ice Coffee");
            Console.WriteLine(change);
        }
    }
}