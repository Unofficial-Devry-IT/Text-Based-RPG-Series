using System;

namespace Convert_Project
{
    public class EnemyConfig : ActorConfig
    {
        public int Age { get; set; }
    }
    
    public class Enemy : Actor
    {
        public int Age { get; init; }

        public Enemy(EnemyConfig config) : base(config)
        {
            Age = config.Age;
        }

        public override string ToString()
        {
            return base.ToString() + $"Age: {Age}\n";
        }
    }
}