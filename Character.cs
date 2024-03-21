using System.ComponentModel.Design;

public class Character
{
    public Character(string name, string vocation,bool hasTurn , int level, int currentVigor, int currVigorMax, int currArmor, int currRes, int currSP, int currSPMax, int currentTech, int dealtDamage, int receiveDamage, 
        List<Skill> passSkills,List<Skill> actSkill , List<StatusEffect> statusEffects, List<Item> items)
    {
        Name = name;
        Vocation = vocation;
        HasTurn = hasTurn;
        Level = level;
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
    public bool HasTurn { get; set; }
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

            
        Console.WriteLine(Name);
        Console.WriteLine("HP: " + string.Concat(Enumerable.Repeat("O", CurrentVigor)) + string.Concat(Enumerable.Repeat("X", CurrVigorMax - CurrentVigor)));
        if (CurrArmor > 0)
        {
            Console.WriteLine("Armor: " + CurrArmor);
        }
        if (CurrRes > 0)
        {
            Console.WriteLine("Resistance: " + CurrRes);
        }
        if (CurrentTech > 0)
        {
            Console.WriteLine("Tech: " + CurrentTech);
        }
        if (StatusEffects.Count > 0)
        {
            Console.WriteLine("Status Effects: ");
            foreach (var effect in StatusEffects)
            {
                Console.Write(effect.Name + " ");
            }
        }
        
    }
    public void DisplaySkills()
    {

        Console.WriteLine("Active Skills: ");
        foreach (var skill in ActSkills)
        {
            Console.WriteLine(skill.Name);
        }
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
        List<string> selfPhysicalEffects = new List<string> { "Blind", "Honed", "Empowered" };
        List<string> targetPhysicalEffects = new List<string> { "Brittle", "Barrier", "Bolstered" };
        List<string> selfMagicalEffects = new List<string> { "Empowered" };
        List<string> targetMagicalEffects = new List<string> { "Bolstered" };
        Skill skill = ActSkills.Find(s => s.Name == skillName);
        if (skill != null)
        {
            skill.Execute(this, target);
            DealtDamage = skill.DealDamage(this, target);

            // set max damage limit
            int fatal = DealtDamage * 4;
            CurrSP -= skill.Cost;

            if (skill.Type == "Physical")
            {
                
                foreach (string selfEffect in selfPhysicalEffects)
                {
                    // Check if the user has the effect
                    if (StatusEffects.Any(e => e.Name == selfEffect))
                    {
                        StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                        effect.Apply(this);
                    }
                    target.RecieveDamage = DealtDamage;
                }

                foreach (string targetEffect in targetPhysicalEffects)
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
                DealtDamage = 0;

                // Remove Strike-based effects after the strike is used
                foreach (string selfEffect in selfPhysicalEffects)
                {
                    // End effect on the character
                    StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                    if (effect != null)
                    {
                        effect.End(this);
                        StatusEffects.Remove(effect);
                    }
                }
                foreach (string effectName in targetPhysicalEffects)
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
            if (skill.Type == "Magical")
            {
                foreach (string selfEffect in selfMagicalEffects)
                {
                    // Check if the user has the effect
                    if (StatusEffects.Any(e => e.Name == selfEffect))
                    {
                        StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                        effect.Apply(this);
                    }
                }
                foreach (string targetEffect in targetMagicalEffects)
                {
                    // Check if the target has the effect
                    if (target.StatusEffects.Any(e => e.Name == targetEffect))
                    {
                        StatusEffect effect = target.StatusEffects.Find(e => e.Name == targetEffect);
                        effect.Apply(target);
                    }
                }
                target.RecieveDamage = DealtDamage;
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
                DealtDamage = 0;

                // Remove Strike-based effects after the strike is used
                foreach (string selfEffect in selfMagicalEffects)
                {
                    // End effect on the character
                    StatusEffect effect = StatusEffects.Find(e => e.Name == selfEffect);
                    if (effect != null)
                    {
                        effect.End(this);
                        StatusEffects.Remove(effect);
                    }
                }
                foreach (string effectName in targetMagicalEffects)
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