public class Character
{
    public Character(string name, string vocation, int level, int currentVigor, int currVigorMax, int currArmor, int currRes, int currSP, int currSPMax, int currentTech, int dealtDamage, int receiveDamage,
    List<Skill> passSkills,List<Skill> actSkill , List<StatusEffect> statusEffects, List<Item> items)
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
        DealtDamage = dealtDamage;
        RecieveDamage = receiveDamage;
        PassSkills = passSkills;
        ActSkills = actSkill;
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
    public int DealtDamage { get; set; }
    public int RecieveDamage { get; set; } 
    public List<Skill> PassSkills { get; set; }
    public List<Skill> ActSkills { get; set; }
    public List<StatusEffect> StatusEffects { get; set; }
    public List<Item> Items { get; set; }
    
    public virtual void DisplayStats()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Current Vigor: " + CurrentVigor);
        Console.WriteLine("Max Vigor: " + CurrVigorMax);
        Console.WriteLine("Current Armor: " + CurrArmor);
        Console.WriteLine("Current Resistance: " + CurrRes);
        Console.WriteLine("Current SP: " + CurrSP);
        Console.WriteLine("Max SP: " + CurrSPMax);
        Console.WriteLine("Current Tech: " + CurrentTech);
    }
    
    public void addPassSkill(Skill skill)
    {
        PassSkills.Add(skill);
    }
        public void addActSkill(Skill skill)
    {
        ActSkills.Add(skill);
    }

    public void removePassSkill(Skill skill)
    {
        PassSkills.Remove(skill);
    }
    public void removeActSkill(Skill skill)
    {
        ActSkills.Remove(skill);
    }
    public void UsePassSkill(string skillName, Character self)
    {
        Skill skill = PassSkills.Find(s => s.Name == skillName);
        if (skill != null)
        {
            skill.Execute(this, self);
        }
        else
        {
            Console.WriteLine("Skill not found.");
        }
    }
    public void UseSkill(string skillName, Character target)
    {
        List<string> selfStrikeEffects = new List<string> { "Blind", "Honed", "Empowered" };
        List<string> targetStrikeEffects = new List<string> { "Brittle", "Barrier", "Bolstered" };
        Skill skill = ActSkills.Find(s => s.Name == skillName);
        if (skill != null)
        {
            skill.Execute(this, target);
            DealtDamage = skill.DealDamage(this, target);

            // set max damage limit
            int fatal = DealtDamage * 4;
            CurrSP -= skill.Cost;

            if (skillName == "Strike")
            {
                
                foreach (string selfEffect in selfStrikeEffects)
                    // Check if the user has the effect
                    if (StatusEffects.Any(e => e.Name == selfEffect))
                    {
                        StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                        effect.Apply(this);
                    }
                    target.RecieveDamage = DealtDamage;
                }

                foreach (string targetEffect in targetStrikeEffects)
                {
                    // Check if the target has the effect
                    if (target.StatusEffects.Any(e => e.Name == targetEffect))
                    {
                        StatusEffect effect = target.StatusEffects.Find(e => e.Name == targetEffect);
                        effect.Apply(target);
                    }
                }      

                // cap max damage
                if (target.RecieveDamage >= fatal)
                {
                    target.RecieveDamage = fatal;
                }
                // set min damage limit
                if (target.RecieveDamage < 0)
                {
                    target.RecieveDamage = 1;
                }
                target.CurrentVigor -= target.RecieveDamage;
                
                // reset damage calculations
                target.RecieveDamage = 0;
                DealtDamage = 0;

                // Remove Strike-based effects after the strike is used
                foreach (string selfEffect in selfStrikeEffects)
                {
                    // End effect on the character
                    StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                    if (effect != null)
                    {
                        effect.End(this);
                        StatusEffects.Remove(effect);
                    }
                }
                foreach (string effectName in targetStrikeEffects)
                {
                    // End effect on the target
                    StatusEffect effect = target.StatusEffects.Find(e => e.Name == effectName);
                    if (effect != null)
                    {
                        effect.End(target);
                        target.StatusEffects.Remove(effect);
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