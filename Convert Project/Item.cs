namespace Convert_Project
{
    public class Item
    {
        public string Name { get; init; } = "Default Name";
        public float Weight { get; init; } = 0.0f;
        public string Description { get; init; } = "";
        public bool IsStackable { get; init; } = false;
        public int ItemCount { get; set; } = 1;
        public int StackSize { get; init; } = 1;
        public float Price { get; init; } = 0.01f;

        public bool IsMaxed => ItemCount == StackSize;
        
        public Item()
        {
            ItemCount = StackSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Increase <see cref="ItemCount"/> by this amount</param>
        public void IncreaseStack(int amount = 1)
        {
            ItemCount += amount;

            if (ItemCount > StackSize)
                ItemCount = StackSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Decreases <see cref="ItemCount"/> by this amount</param>
        public void DecreaseStack(int amount = 1)
        {
            ItemCount -= amount;

            if (ItemCount < 0)
                ItemCount = 0;
        }
    }
}