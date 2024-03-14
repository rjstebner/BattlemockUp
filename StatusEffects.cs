public class StatusEffect
{
    public string? Name { get; set; }
    public int Duration { get; set; }
    

    // This method will be called each turn to apply the effect
    public virtual void Apply(Character character)
    {
        // Default behavior: do nothing
    }

    // This method will be called when the effect ends
    public virtual void End(Character character)
    {
        // Default behavior: do nothing
    }
}

//Character Deals Double Damage
public class Honed : StatusEffect
{
    public Honed()
    {
        Name = "Honed";
    }
    public override void Apply(Character character)
    {
        character.DealtDamage *= 2; // Honed characters deal more damage
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Honed.");
    }
}

//Character Recieves Double Damage
public class Brittle : StatusEffect
{
    public Brittle()
    {
        Name = "Brittle";
    }
    public override void Apply(Character character)
    {
        character.RecieveDamage *= 2; // Brittle characters have less armor
        Console.WriteLine(character.Name + " is now Brittle.");
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Brittle.");
    }
}

//Character Deals No Damage
public class Blind : StatusEffect
{
    public Blind()
    {
        Name = "Blind";
    }
    public override void Apply(Character character)
    {
        character.DealtDamage = 0; // Blinded characters can't deal damage
        
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Blind.");
    }
}

//Character Recieves No Damage
public class Barrier : StatusEffect
{
    public Barrier()
    {
        Name = "Barrier";
    }
    public override void Apply(Character character)
    {
        character.RecieveDamage = 0; // Barrier characters take no damage
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Protected.");
    }
}

// Character Deals 1.5x Damage
public class Empowered : StatusEffect
{
    public Empowered()
    {
        Name = "Empowered";
    }
    public override void Apply(Character character)
    {
        // Empowered characters deal more damage
        double damage = character.DealtDamage;
        double modDamage = damage * 1.5;
        character.DealtDamage = (int)modDamage;

    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Empowered.");
    }
}
// Character Recieves half Damage
public class Bolstered : StatusEffect
{
    public Bolstered()
    {
        Name = "Bolstered";
    }
    public override void Apply(Character character)
    {
        // Bolstered characters take more damage
        double damage = character.RecieveDamage;
        double modDamage = damage / 2;
        character.RecieveDamage = (int)modDamage;
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Bolstered.");
    }
}

public class Hidden : StatusEffect
{
    public Hidden()
    {
        Name = "Hidden";
    }
    public override void Apply(Character character)
    {
        // Hidden characters can't be targeted
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Hidden.");
    }
}