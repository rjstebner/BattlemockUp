public class Combat
{
    public List<Character> Players { get; private set; }
    public List<Character> Enemies { get; private set; }

    public Combat(List<Character> players, List<Character> enemies)
    {
        Players = new List<Character>(players);
        Enemies = new List<Character>(enemies);
    }

    public void Start()
    {
        int playerIndex = 0;
        int enemyIndex = 0;
        int turnCount = 0;

        while (Players.Any(p => p.CurrentVigor > 0) && Enemies.Any(e => e.CurrentVigor > 0))
        {
            turnCount++;
            Console.WriteLine($"Turn {turnCount}");
            DisplayCombatants();
            List<Character> playerHasTurn = new List<Character>(Players);
            List<Character> enemyHasTurn = new List<Character>(Enemies);
            TriggerPassives();
            while (playerHasTurn.Count != 0 && enemyHasTurn.Count !=0)
            {
                if (playerHasTurn.Any(p => p.CurrentVigor > 0))
                {
                    // Player's turn logic goes here
                    Character PC = SelectPlayer();
                    PC.DisplaySkills();
                    string skill = SelectSkill();
                    foreach (var enemy in Enemies)
                    {
                        enemy.DisplayStats();
                    }
                    Character EC = SelectEnemy();
                    PC.UseSkill(skill, EC);

                    playerHasTurn.Remove(PC);
                }


                // Enemy's turn
                if (enemyHasTurn.Any(e => e.CurrentVigor > 0))
                {
                    // Enemy's turn logic goes here
                    EnenyTurn(enemyHasTurn[0]);

                }
                enemyHasTurn.Remove(enemyHasTurn[0]);
                }
        }
    }

    public void TriggerPassives()
    {
        foreach (var character in Players.Concat(Enemies))
        {
            foreach (var skill in character.PassSkills)
            {
                skill.Execute(character, character);
            }
        }
    }

    public void DisplayCombatants()
    {
        foreach (var character in Players)
        {
            character.DisplayStats();
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("==================================");
        Console.WriteLine();

        foreach (var character in Enemies)
        {
            character.DisplayStats();
        }
    }

    public Character SelectPlayer()
    {
        Console.WriteLine("Select a character to Control: ");
        string input = Console.ReadLine();
        
        if (!string.IsNullOrEmpty(input))
        {
            var selectedPlayer = Players.FirstOrDefault(p => p.Name == input);
            if (selectedPlayer != null)
            {
                return selectedPlayer;
            }
        }
        
        Console.WriteLine("Invalid input");
        return SelectPlayer();
    }

    public Character SelectEnemy()
    {
        Console.WriteLine("Select an enemy to attack: ");
        string input = Console.ReadLine();
        
        if (!string.IsNullOrEmpty(input))
        {
            var selectedEnemy = Enemies.FirstOrDefault(e => e.Name == input);
            if (selectedEnemy != null)
            {
                return selectedEnemy;
            }
        }
        
        Console.WriteLine("Invalid input");
        return SelectEnemy();
    }

    public string SelectSkill()
    {
        Console.WriteLine("Select a skill to use: ");
        string input = Console.ReadLine();
        
        if (!string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        Console.WriteLine("Invalid input");
        return SelectSkill();
    }
    public void EnenyTurn(Character Enemy)
    {
        if (Enemy.Vocation == "Mob")
        {
            // Mob logic attacks a random player, prio the first player in list
            Random random = new Random();
            int target = random.Next(0, 10);
            if (target < 5)
            {
                Enemy.UseSkill("Strike", Players[0]);
            }
            if (target >= 5  &&  target <= 7)
            {
                Enemy.UseSkill("Strike", Players[1]);
            }
            if (target == 8 & target == 9)
            {
                Enemy.UseSkill("Strike", Players[2]);
            }
            else
            {
                Enemy.UseSkill("Strike", Players[3]);
            }
        }
        if (Enemy.Vocation == "Killer")
        {
            // Killer logic attack the lowest health player
            int lowestHealth = 100;
            Character target = null;
            foreach (var player in Players)
            {
                if (player.CurrentVigor < lowestHealth)
                {
                    lowestHealth = player.CurrentVigor;
                    target = player;
                }
            }
            Enemy.UseSkill("Strike", target);
        }
        if (Enemy.Vocation == "Mage")
        {
            // Mage logic attack player with lowest Res

            if (Enemy.CurrSP != 0)
            {
                Character target = null;
                int lowestRes = 100;
                foreach (var player in Players)
                {
                    if (player.CurrRes < lowestRes)
                    {
                        lowestRes = player.CurrRes;
                        target = player;
                    }
                }
                Enemy.UseSkill("Energybolt", target);
            }
            else
            {
                Enemy.Vocation = "Killer";
            }
        }
        if (Enemy.Vocation == "Support")
        {
            // Support logic goes here Heal Enemy With lowest health
        }
    }

    public void End()
    {
        // Combat end logic goes here
    }
}