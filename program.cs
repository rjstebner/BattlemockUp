using System;
using System.Collections.Generic;

class program
{
    static void Main()
    {
        Character Peso = new PlayerCharacter(
            "Peso", "Rapscallion", 1, 
        // vigor, vigorMax, armor, res, sp, spMax, tech, dealtDamage, receiveDamage
            8, 8, 10, 8, 3, 3, 3, 0, 0, 
            new List<Skill>{ new SurpriseAttack() }, new List<Skill> {new Strike(), new PocketSand()}, new List<StatusEffect> { }, new List<Item> { }, 
            0, 
        // vigor, vigorMax, armor, armorMax, res, resMax, sp, spMax, tech, techMax
            8, 12, 10, 14, 8, 10, 3, 5, 3, 7);
        
        Character Charlie = new PlayerCharacter(
            "Charlie", "Archer", 1, 
            7, 7, 7, 5, 3, 3, 5, 0, 0, 
            new List<Skill>{new Focused()}, new List<Skill> {new Strike(), new MarkPrey()}, new List<StatusEffect> { }, new List<Item> { }, 
            0, 
            7, 11, 7, 10, 5, 8, 3, 7, 5, 8);
        
        Character Richie = new PlayerCharacter(
            "Richie", "Healer", 1, 
            6, 6, 10, 12, 3, 3, 5, 0, 0, 
            new List<Skill>{ new BeconOfHealth()}, new List<Skill> {new Strike(), new Heal()}, new List<StatusEffect> { }, new List<Item> { }, 
            0, 
            6, 8, 10, 15, 12, 16, 3, 7, 5, 10);
        
        Character Beary = new PlayerCharacter(
            "Beary", "Warrior", 1, 
            10, 10, 12, 4, 3, 3, 3, 0, 0, 
            new List<Skill>{new SheildBlock() }, new List<Skill> {new Strike(), new ForTheKing() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 
            10, 15, 12, 15, 4, 8, 3, 8, 3, 3);
        
        Character Piggy = new PlayerCharacter(
            "Piggy", "Mage", 1, 
            6, 6, 8, 12, 5, 5, 8, 0, 0, 
            new List<Skill>{new LivingOnTheEdge()}, new List<Skill> {new Strike(), new Energybolt() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 
            6, 10, 8, 12, 12, 16, 5, 9, 8, 13);
        
        
        Character goblin = new Character(
            "Goblin", "Mob", 1, 6, 6, 6, 0, 0, 0, 0, 0, 0, new List<Skill>{}, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { });
        Character orc = new Character(
            "Orc", "Killer", 1, 14, 14, 10, 0, 0, 0, 0, 0, 0, new List<Skill>{}, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { });
        Character shaman = new Character(
            "Shaman", "Monster", 1, 10, 10, 0, 12, 5, 5, 8, 0, 0, new List<Skill>{}, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { });
        

        Combat combat = new Combat(new List<Character> {Peso, Charlie, Richie, Beary, Piggy }, new List<Character> {goblin, orc, shaman});
        combat.Start();
    }
}