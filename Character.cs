public class Character
{
    public Character(string name, string vocation, int level, int currentVigor, int currVigorMax, int currArmor, int currRes, int currSP, int currSPMax, int currentTech, int damage, List<Skill> skills, List<StatusEffect> statusEffects, List<Item> items)
    {
        Name = name;
        Vocation = vocation;
        level = Level;
        CurrentVigor = currentVigor;
        CurrVigorMax = currVigorMax;
        CurrArmor = currArmor;
        CurrRes = currRes;
        CurrSP = currSP;
        CurrSPMax = currSPMax;
        CurrentTech = currentTech;
        Damage = damage;
        Skills = skills;
        StatusEffects = statusEffects;
        Items = items;
    }
    
    public string Name { get; set; }
    public string Vocation { get; set; }  
    public int Level { get; set; }
    public int CurrentVigor { get; set; }
    public int CurrVigorMax { get; set; }
    public int CurrArmor { get; set; }
    public int CurrRes { get; set; }
    public int CurrSP { get; set; }
    public int CurrSPMax { get; set; }
    public int CurrentTech { get; set; }
    public int Damage { get; set; }
    public List<Skill> Skills { get; set; }
    public List<StatusEffect> StatusEffects { get; set; }
    public List<Item> Items { get; set; }
    
    public virtual void DisplayStats()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Vocation: " + Vocation);
        Console.WriteLine("Current Vigor: " + CurrentVigor);
        Console.WriteLine("Max Vigor: " + CurrVigorMax);
        Console.WriteLine("Current Armor: " + CurrArmor);
        Console.WriteLine("Current Resistance: " + CurrRes);
        Console.WriteLine("Current SP: " + CurrSP);
        Console.WriteLine("Max SP: " + CurrSPMax);
        Console.WriteLine("Current Tech: " + CurrentTech);
        Console.WriteLine("Skills: " + string.Join(", ", Skills));
    }
    
    public void addSkill(Skill skill)
    {
        Skills.Add(skill);
    }
    public void UseSkill(string skillName, Character target)
    {
        Skill skill = Skills.Find(s => s.Name == skillName);
        if (skill != null)
        {
            skill.Execute(this, target);
            Damage = skill.DealDamage(this, target);
            CurrSP -= skill.Cost;
            if (skillName == "Strike")
            {
                List<string> strikeEffects = new List<string> { "Blind" };
                foreach (string effectName in strikeEffects)
                {
                    // Check if the target has the effect
                    if (target.StatusEffects.Any(e => e.Name == effectName))
                    {
                        StatusEffect effect = target.StatusEffects.Find(e => e.Name == effectName);
                        effect.Apply(target);
                    }

                    // Check if the user has the effect
                    if (StatusEffects.Any(e => e.Name == effectName))
                    {
                        StatusEffect effect = StatusEffects.Find(e => e.Name == effectName);
                        effect.Apply(this);
                    }
                }

                target.CurrentVigor -= Damage;

                // Remove Strike-based effects after the strike is used
                foreach (string effectName in strikeEffects)
                {
                    // End effect on the character
                    StatusEffect effect = StatusEffects.Find(e => e.Name == effectName);
                    if (effect != null)
                    {
                        effect.End(this);
                        StatusEffects.Remove(effect);
                    }

                    // End effect on the target
                    StatusEffect targetEffect = target.StatusEffects.Find(e => e.Name == effectName);
                    if (targetEffect != null)
                    {
                        targetEffect.End(target);
                        target.StatusEffects.Remove(targetEffect);
                    }
                }
            }
        
            
        }
        else
            {
                Console.WriteLine("Skill not found.");
            }
    }
    public void AddStatusEffect(StatusEffect effect)
    {
        StatusEffects.Add(effect);
    }
    public void RemoveStatusEffect(StatusEffect effect)
    {
        StatusEffects.Remove(effect);
        effect.End(this);
    }
}