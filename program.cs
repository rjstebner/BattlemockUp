using System;
using System.Collections.Generic;

class program
{
    static void Main()
    {
        Character Peso = new PlayerCharacter(
            "Bob", "Rapscallion", 1, 100, 100, 50, 50, 100, 100, 100, 0, new List<Skill> {new Strike(), new Blinding() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 100, 100, 50, 50, 100, 100, 100, 100, 100, 100);
        Character Charlie = new PlayerCharacter(
            "Bob", "Archer", 1, 100, 100, 50, 50, 100, 100, 100, 0, new List<Skill> {new Strike()}, new List<StatusEffect> { }, new List<Item> { }, 
            0, 100, 100, 50, 50, 100, 100, 100, 100, 100, 100);
        Character Richie = new PlayerCharacter(
            "Bob", "Healer", 1, 100, 100, 50, 50, 100, 100, 100, 0, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 100, 100, 50, 50, 100, 100, 100, 100, 100, 100);
        Character Beary = new PlayerCharacter(
            "Bob", "Warrior", 1, 100, 100, 50, 50, 100, 100, 100, 0, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 100, 100, 50, 50, 100, 100, 100, 100, 100, 100);
        Character Piggy = new PlayerCharacter(
            "Bob", "Mage", 1, 100, 100, 50, 50, 100, 100, 100, 0, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { }, 
            0, 100, 100, 50, 50, 100, 100, 100, 100, 100, 100);
        
        
        Character enemy = new Character(
            "Goblin", "Monster", 1, 50, 50, 25, 25, 50, 50, 50, 0, new List<Skill> {new Strike() }, new List<StatusEffect> { }, new List<Item> { }); ;
        
        Strike strike = new Strike();
        Blinding blinding = new Blinding();

    }
}