namespace Convert_Project.Managers
{
    public class GameManager
    {
        private Player player = new Player(new ActorConfig
        {
            Name = "Legolas",
            Description = "Super awesome arrow elf",
            MaxHitpoints = 120,
            StartingHitpoints = 120,
            StartingCurrency = 5
        });
        
        private Area currentArea = new Market()
        {
            Name = "Me Castle",
            Inventory = new Inventory(Util.LoadFromFile<Item>())
        };

        public void GameLoop()
        {
            currentArea.Enter(player);
            
            while (!currentArea.IsPlayerLeaving)
            {
                currentArea.GameLoop();
            }
        }
    }
}