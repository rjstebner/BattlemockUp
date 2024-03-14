public class Skill
{
    public string? Name { get; set; }

    public string? Description { get; set; }
    public int Cost { get; set; }


    public virtual void Execute(Character user, Character target)
    {
        // Default behavior: do nothing
    }
    public virtual int DealDamage(Character user, Character target)
    {
        return 0;
    }
}

public class Strike : Skill
{
    public Strike()
    {
        Name = "Strike";
        Description = "A basic attack that deals damage to enemies.";
        Cost = 0;
    }

    public override int DealDamage(Character user, Character target)
    {
    
        int damage = user.CurrentVigor - target.CurrArmor;
        return damage;
    
    }
}

public class Blinding : Skill
{
    public Blinding()
    {
        Name = "Blinding";
        Description = "A basic attack that deals damage to enemies.";
        Cost = 1;
    }

    public override void Execute(Character user, Character target)
    {
    
        target.AddStatusEffect(new Blind());
        Console.WriteLine(target.Name + " is Blinded.");
    
    }
}
