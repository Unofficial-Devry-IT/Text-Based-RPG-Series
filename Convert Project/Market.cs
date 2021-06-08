using System;
using System.Collections.Generic;
using System.Linq;
using Convert_Project.Exceptions;

namespace Convert_Project
{
    public enum MarketOptions
    {
        SHOW_GOODS = 1,
        BUY_GOODS = 2,
        SELL_GOODS = 3,
        DISPLAY_PLAYER_INVENTORY = 4,
        EXIT = 5
    }
    
    public class Market : Area
    {
        public string Name { get; init; }
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Displays area entrance message
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="previous">Previous area visited</param>
        public override void Enter(Player player, Area previous = null)
        {
            Player = player;
            PreviousArea = previous;
            
            Console.WriteLine($"Welcome to {Name}! Here's a list of goodies...");
            DisplayItems();
        }
        
        /// <summary>
        /// Displays area exit message and sets leaving flag
        /// </summary>
        public override void Exit()
        {
            Console.WriteLine("Thanks for stopping by! Please come again soon!");
            IsPlayerLeaving = true;
        }

        /// <summary>
        /// Area game loop containing menu
        /// </summary>
        public override void GameLoop()
        {
            // 1 - show goods
            // 2 - buy
            // 3 - sell
            // 4 - Display Player Inventory
            // 5 - exit
            Console.WriteLine("[1] Show Goods\n" +
                              "[2] Buy Goods\n" +
                              "[3] Sell Goods\n" +
                              "[4] Lets see what's in your backpack\n" +
                              "[5] Exit\n");
            
            MarketOptions option = (MarketOptions) Util.GetUserInput("Pick a value between 1 - 5", 1, 5);

            switch (option)
            {
                case MarketOptions.SHOW_GOODS:
                    DisplayItems();
                    break;
                case MarketOptions.BUY_GOODS:
                    BuyGoods();
                    break;
                case MarketOptions.SELL_GOODS:
                    SellGoods();
                    break;
                case MarketOptions.DISPLAY_PLAYER_INVENTORY:
                    Player.DisplayInventory();
                    break;
                case MarketOptions.EXIT:
                    Exit();
                    break;
            }
        }

        /// <summary>
        /// Permits purchasing of goods from market
        /// </summary>
        private void BuyGoods()
        {
            
            string input = Util.GetUserInput<string>($"Ye want to buy something eh? Well the format is as follows\n" +
                                                            $"\tITEM_NAME QUANTITY",
                (x) =>
                {
                    if (string.IsNullOrEmpty(x))
                        return false;

                    string[] split = x.Split(" ");
                    if (split.Count() != 2)
                        return false;

                    if (!int.TryParse(split[1], out int _))
                        return false;

                    return true;
                });

            string[] split = input.Split(" ");
            int quantity = int.Parse(split[1]);

            try
            {
                Item item = Inventory.RemoveItem(split[0], quantity);

                if (Player.Coins < (item.Price * item.ItemCount))
                {
                    Console.WriteLine(
                        $"Sorry {Player.Name}, your too broke for that......try a different product or lesser quantity\n");
                    return;
                }

                Player.Inventory.AddItem(item);
                Player.SubtractCoins(item.Price * item.ItemCount);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Can you read? We don't have '{ex.ItemName}' in our market!");
            }
            catch (ExceedsItemStackSizeException ex)
            {
                Console.WriteLine($"{ex.Amount} exceeds the stack size limit for '{ex.ItemName}'");
            }
            
        }

        /// <summary>
        /// Permits selling of goods back to market
        /// </summary>
        private void SellGoods()
        {

            string input = Util.GetUserInput<string>($"Whatcha got for me today? I only accept items in the following format:\n" +
                                                            $"\tITEM_NAME QUANTITY",
                (x) =>
                {
                    if (string.IsNullOrEmpty(x))
                        return false;

                    string[] split = x.Split(" ");
                    if (split.Count() != 2)
                        return false;

                    if (!int.TryParse(split[1], out int _))
                        return false;

                    return true;
                });

            string[] split = input.Split(" ");
            int quantity = int.Parse(split[1]);

            try
            {
                Item item = Player.Inventory.RemoveItem(split[0], quantity);
                Player.AddCoins(item.Price * item.ItemCount);
                Inventory.AddItem(item);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Umm... it doesn't look like you have '{ex.ItemName}' in your inventory");
            }
            catch (ExceedsItemStackSizeException ex)
            {
                Console.WriteLine($"Well... hate to break it to ya... but {ex.Amount} exceeds {ex.ItemName}'s stack size. Try picking a smaller amount next time...");
            }
        }

        /// <summary>
        /// Displays table of properties for items in market, along with current player's coin count
        /// </summary>
        private void DisplayItems()
        {
            Console.WriteLine(Inventory);
            Console.WriteLine($"\nYou currently have ${Player.Coins}");
        }
    }
}