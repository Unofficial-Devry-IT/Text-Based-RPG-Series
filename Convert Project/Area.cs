using System;

namespace Convert_Project
{
    public abstract class Area
    {
        public string Name { get; set; }
        public Area PreviousArea { get; set; }
        
        protected Player Player;
        public bool IsPlayerLeaving { get; protected set; } = false;

        public virtual void Enter(Player player, Area previous = null)
        {
            Player = player;
            PreviousArea = previous;
            IsPlayerLeaving = false;
            
            Console.WriteLine(player != null 
                ? $"{player.Name} is entering {Name}" 
                : $"Entering {Name}...");
        }

        public abstract void GameLoop();

        public virtual void Exit()
        {
            IsPlayerLeaving = true;
            Console.WriteLine(Player != null 
                ? $"{Player.Name} is leaving {Name}" 
                : $"Leaving {Name}...");
        }
    }
}