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
public class Blind : StatusEffect
{
    public Blind()
    {
        Name = "Blind";
    }
    public override void Apply(Character character)
    {
        character.Damage = 0; // Blinded characters can't deal damage
        
    }

    public override void End(Character character)
    {
        Console.WriteLine(character.Name + " is no longer Blind.");
    }
}