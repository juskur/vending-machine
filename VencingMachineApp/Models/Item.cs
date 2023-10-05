namespace VencingMachineApp.Models
{
    ///<summary>
    ///Item class represents and item description in vending machine
    ///</summary>
    public class Item
    {
        public string Name { get; }

        public int Price { get; }

        ///<summary>
        ///Item constructor
        ///</summary>
        ///<param name="name">Iten name</param>
        ///<param name="price">Item price</param>
        public Item(string name, int price)
        {
            Name = name;
            Price = price;
        }

    }
}
