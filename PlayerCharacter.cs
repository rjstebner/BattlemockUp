public class PlayerCharacter : Character
{
    public PlayerCharacter(string name, string vocation, int level ,int currentVigor, int currVigrorMax, int currArmor, int currRes, int currSP, int currSPMax, int currentTech, int dealtDamage, int receiveDamage,
    List <Skill> passSkills, List<Skill> actSkills, List<StatusEffect> statusEffects, List<Item> items,
    int experience, int vigor, int vigorMax, int armor, int armorMax, int res, int resMax, int sp, int spMax, int tech, int techMax) 
        : base(name, vocation, level, currentVigor, currVigrorMax, currArmor, currRes, currSP, currSPMax, currentTech, dealtDamage, receiveDamage, passSkills, actSkills, statusEffects, items)
    {
        Level = level;
        Experience = experience;
        Vigor = vigor;
        VigorMax = vigorMax;
        Armor = armor;
        ArmorMax = armorMax;
        Res = res;
        ResMax = resMax;
        SP = sp;
        SPMax = spMax;
        Tech = tech;
        TechMax = techMax;
    }

    public int Experience { get; set; }
    public int Vigor { get; set; } 
    public int VigorMax { get; set; }
    public int Armor { get; set; }
    public int ArmorMax { get; set; }
    public int Res { get; set; }
    public int ResMax { get; set; }
    public int SP { get; set; }
    public int SPMax { get; set; }
    public int Tech { get; set; }
    public int TechMax { get; set; }


    public void LevelUp()
    {
        Level++;
        Experience = 0;
        // You might want to increase other stats here as well
    }

    public override void DisplayStats()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("HP: " + string.Concat(Enumerable.Repeat("O", CurrentVigor)) + string.Concat(Enumerable.Repeat("X", CurrVigorMax - CurrentVigor)));
        Console.WriteLine("Armor: " + CurrArmor + "\t" + "| Resistance: " + CurrRes);
        Console.WriteLine("SP: " + string.Concat(Enumerable.Repeat("O", CurrSP)) + string.Concat(Enumerable.Repeat("X", CurrSPMax - CurrSP)) + " \t| Tech: " + CurrentTech);
    }
}