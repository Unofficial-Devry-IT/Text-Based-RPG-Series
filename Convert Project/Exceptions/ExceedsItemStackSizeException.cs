using System;

namespace Convert_Project.Exceptions
{
    /// <summary>
    /// Thrown when amount of items to get exceeds the item's stack size
    /// </summary>
    public class ExceedsItemStackSizeException : Exception
    {
        public string ItemName { get; }
        public int Amount { get; }

        public ExceedsItemStackSizeException(string name, int amount)
        {
            ItemName = name;
            Amount = amount;
        }
    }
}