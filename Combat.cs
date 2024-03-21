using System.Data.Common;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

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
        int turnCount = 0;

        while (Players.Any(p => p.CurrentVigor > 0) && Enemies.Any(e => e.CurrentVigor > 0))
        {
            turnCount++;


            Console.WriteLine($"Turn {turnCount}");
                        
            TriggerPassives();
            
            List<Character> Combatants = new List<Character>(Players.Concat(Enemies));
            foreach (var combatant in Combatants)
            {
                combatant.HasTurn = true;
            }

            LineBreak();   

            DisplayCombatants(Players);
            
            LineBreak();

            DisplayCombatants(Enemies);

            LineBreak();

            Console.Write("Press enter to continue ");
            Console.ReadLine();
            try
                {
                    Console.Clear();
                }
            catch (System.IO.IOException)
                {
                    // Handle the exception here
                }


            while (Players.Any(p => p.HasTurn == true) || Enemies.Any(e => e.HasTurn == true))
            {
            Console.Clear();
            
            if (Players.Any(p => p.HasTurn == true))
            {
                TurnLoop(Players, Enemies);

            }
  
            }        
        }
        End();
    }

    public void TurnLoop(List<Character> players, List<Character> enemies)
    {
            
        DisplayCombatants(players);
        LineBreak();
    

        Character pc = SelectPlayer(players);
        if (pc.HasTurn == false)
        {
            Console.WriteLine("This character has already had a turn");
            TurnLoop(players, enemies);
        }
        else
        {
            Console.Clear();

            pc.DisplaySkills();
            string skill = SelectSkill();

            Console.Clear();

            DisplayCombatants(enemies);
            Character target = SelectEnemy();
            pc.UseSkill(skill, target);
            if (enemies.Any(e => e.HasTurn == true))
            {
                EnenyTurn(enemies.First(e => e.HasTurn == true));
                enemies.First(e => e.HasTurn == true).HasTurn = false;
            }
            pc.HasTurn = false;

            Console.ReadLine();
            players.RemoveAll(p => p.CurrentVigor <= 0);
            enemies.RemoveAll(e => e.CurrentVigor <= 0);  

            }
            Console.Clear();   
            DisplayCombatants(players);
            LineBreak();
            DisplayCombatants(enemies);
            LineBreak();

            foreach (var player in players)
            {
                if (player.RecieveDamage > 0)
                {
                    Console.WriteLine(player.Name + " took " + player.RecieveDamage + " damage");
                    player.RecieveDamage = 0;
                }
                if (player.CurrentVigor <= 0)
                {
                    Console.WriteLine(player.Name + " has been defeated");
                }

            }
            foreach (var enemy in enemies)
            {
                if (enemy.RecieveDamage > 0)
                {
                    Console.WriteLine(enemy.Name + " took " + enemy.RecieveDamage + " damage");
                    enemy.RecieveDamage = 0;
                }
                if (enemy.CurrentVigor <= 0)
                {
                    Console.WriteLine(enemy.Name + " has been defeated");
                }
            }   
            Console.ReadLine();
            Console.Clear();


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
    public void DisplayCombatants(List<Character> Combatants)
    {
        int cursorspot = 31;
        foreach (var player in Combatants)
        {
            Console.Write("||| ");
            Console.Write(player.Name + " | " + player.HasTurn );
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();
        cursorspot = 31;

        foreach (var player in Combatants)
        {
            Console.Write("||| ");
            Console.Write("HP: " + string.Concat(Enumerable.Repeat("O", player.CurrentVigor)) + string.Concat(Enumerable.Repeat("X", player.CurrVigorMax - player.CurrentVigor)));
            Console.Write("   " + player.CurrentVigor + "/" + player.CurrVigorMax + " ");
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();
        cursorspot = 31;
        foreach (var player in Combatants)
        {
            Console.Write("||| ");
            if (player.CurrArmor > 0)
            {
                Console.Write("Armor: " + player.CurrArmor + " ");
            }
            if (player.CurrRes > 0)
            {
                Console.Write("| Resistance: " + player.CurrRes);
            }
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();
        cursorspot = 31;

        foreach (var player in Combatants)
        {
            Console.Write("||| ");
            Console.Write("SP: " + string.Concat(Enumerable.Repeat("O", player.CurrSP)) + string.Concat(Enumerable.Repeat("X", player.CurrSPMax - player.CurrSP)) + " ");
            if (player.CurrentTech > 0)
            {
                Console.Write("| Tech: " + player.CurrentTech);
            }
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();

        cursorspot = 31;

        foreach (var player in Combatants)
        {
            Console.Write("||| ");
            Console.Write("Status Effects: ");
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();
        cursorspot = 31;

        foreach (var player in Combatants)
        {
            Console.Write("         ");
            foreach (var effect in player.StatusEffects)
            {
                Console.Write(effect.Name + " ");
            }
            Console.SetCursorPosition(cursorspot, Console.CursorTop);
            cursorspot += 31;
        }
        Console.WriteLine();
    }
    public Character SelectPlayer(List<Character> Players)
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
        return SelectPlayer(Players);
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
            if (target < 4)
            {
                Enemy.UseSkill("Strike", Players[0]);
            }
            else if (target >= 4 && target <= 6)
            {
                Enemy.UseSkill("Strike", Players[1]);
            }
            else if (target == 7 || target == 8)
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
    public void LineBreak()
    {
        int number = 124;
        Console.WriteLine();
        while (number > 0)
        {
            Console.Write("=");
            number -= 1;
        }
        Console.WriteLine();
    }
    public void End()
    {
        if (Players.Any(p => p.CurrentVigor > 0))
        {
            Console.Clear();
            Console.WriteLine("You win!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You lose!");
        }
    }
}