using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Project
{
    public class ActorConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double StartingHitpoints { get; set; }
        public double MaxHitpoints { get; set; }
        public Item StartingWeapon { get; set; }
        public float StartingCurrency { get; set; }
        public Inventory StartingInventory { get; set; }
    }
    
    public class Actor
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Hitpoints { get; protected set; }
        public double MaxHitpoints { get; init; }
        public float Coins { get; protected set; }
        public Item PrimaryWeapon { get; set; }
        public Inventory Inventory { get; set; }

        public Actor(ActorConfig config)
        {
            Hitpoints = config.StartingHitpoints;
            MaxHitpoints = config.MaxHitpoints;
            Name = config.Name;
            Description = config.Description;
            PrimaryWeapon = config.StartingWeapon;
            Coins = config.StartingCurrency;
            Inventory = config.StartingInventory ?? new Inventory(new List<Item>());
        }
        
        /// <summary>
        /// Displays table of items in Actor's inventory
        /// </summary>
        public void DisplayInventory()
        {
            Console.WriteLine($"-----{Name}'s Inventory-----");
            Console.WriteLine(Inventory);
        }
        
        public void Heal(double args)
        {
            Hitpoints += args;

            if (Hitpoints > MaxHitpoints)
                Hitpoints = MaxHitpoints;
        }
        
        public void TakeDamage(double args)
        {
            Hitpoints += args;

            if (Hitpoints < 0)
                Hitpoints = 0;
        }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                   $"Desc: {Description}\n" +
                   $"Hitpoints: {Hitpoints} / {MaxHitpoints}\n";
        }

        public void AddCoins(float args)
        {
            Coins += args;
        }

        public void SubtractCoins(float args)
        {
            Coins -= args;
        }
    }
}
