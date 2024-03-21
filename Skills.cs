using System.Reflection.Metadata;
using System.Security;

public class Skill
{
    public string? Name { get; set; }

    public string? Description { get; set; }
    public string? Type { get; set; }
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
// Passive Skills
public class SheildBlock : Skill
{
    public SheildBlock()
    {
        Name = "Sheild Block";
        Description = "Start Each Combat with a Bolstered.";
        Cost = 0;

    }

    public override void Execute(Character user, Character target)
    {
        bool isBolstered = false;
        foreach (var effect in user.StatusEffects)
        {
            if (effect is Bolstered)
            {
                isBolstered = true;
                break;
            }
        }

        if (!isBolstered)
        {
            user.AddStatusEffect(new Bolstered());
        }
        Console.WriteLine(user.Name + " is now Bolstered.");

    }
}
public class Focused : Skill
{
    public Focused()
    {
        Name = "Focused";
        Description = "Increase Damage When Vigor is Full.";
        Cost = 0;
    }

    public override void Execute(Character user, Character target)
    {
        if (user.CurrentVigor == user.CurrVigorMax)
        {
            user.DealtDamage += 2;
        }
    }
}
public class SurpriseAttack : Skill
{
    public SurpriseAttack()
    {
        Name = "Surprise Attack";
        Description = "A Strike that avoids armor.";
        Cost = 0;
    }

    public override void Execute(Character user, Character target)
    {
        if (user.StatusEffects.Any(effect => effect is Hidden))
        {
            if (!user.ActSkills.Any(skill => skill.Name == "Stealth Strike"))
            {
                user.ActSkills.Add(new StealthStrike());
            }
            user.ActSkills.RemoveAll(skill => skill is Strike);
        }
        else
        {
            if (!user.ActSkills.Any(skill => skill.Name == "Strike"))
            {
                user.ActSkills.Add(new Strike());
            }
            user.ActSkills.RemoveAll(skill => skill is StealthStrike);
        }
    }
}
// maybe change?
public class BeconOfHealth : Skill
{
    public BeconOfHealth()
    {
        Name = "Becon of Health";
        Description = "Heal 10% of Max Health at the start of each turn.";
        Cost = 0;
    }

    public override void Execute(Character user, Character target)
    {
        user.CurrentVigor += user.CurrVigorMax / 10;
        Console.WriteLine(user.Name + " has healed for " + user.CurrVigorMax / 10 + " health.");
    }
}

public class LivingOnTheEdge : Skill
{
    public LivingOnTheEdge()
    {
        Name = "Living on the Edge";
        Description = "Deal 50% more damage when below 25% health.";
        Cost = 0;
    }
    public override void Execute(Character user, Character target)
    {
        if (user.CurrentVigor < user.CurrVigorMax / 4)
        {
            user.AddStatusEffect(new Empowered()); 
            Console.WriteLine(user.Name + " is now Empowered.");
        }
    }
}

// Active Skills
public class Strike : Skill
{
    public Strike()
    {
        Name = "Strike";
        Description = "A basic attack that deals damage to enemies.";
        Type = "Physical";
        Cost = 0;
    }

    public override int DealDamage(Character user, Character target)
    {
    
        int damage = (user.DealtDamage + user.CurrentVigor) - target.CurrArmor;
        return damage;
    
    }
}

public class StealthStrike : Skill
{
    public StealthStrike()
    {
        Name = "Stealth Strike";
        Description = "A basic attack that pierces all armor.";
        Type = "Physical";
        Cost = 0;
    }

    public override int DealDamage(Character user, Character target)
    {
        int damage = (user.DealtDamage + user.CurrentVigor);
        return damage;
    }
}

public class ForTheKing : Skill
{
    public ForTheKing()
    {
        Name = "For the King";
        Description = "Give an Ally Barrier.";
        Cost = 1;
    }

    public override void Execute(Character user, Character target)
    {
        target.AddStatusEffect(new Barrier());
        Console.WriteLine(target.Name + " is now Barriered.");
    }
}

public class MarkPrey : Skill
{
    public MarkPrey()
    {
        Name = "Mark Prey";
        Description = "Target gains Brittle.";
        Cost = 1;
    }

    public override void Execute(Character user, Character target)
    {
        target.AddStatusEffect(new Brittle());
        Console.WriteLine(target.Name + " is now Brittle.");
    }
}
public class PocketSand : Skill
{
    public PocketSand()
    {
        Name = "Pocket Sand";
        Description = "Target gains Blind.";
        Cost = 1;
    }

    public override void Execute(Character user, Character target)
    {
        target.AddStatusEffect(new Blind());
        Console.WriteLine(target.Name + " is now Blinded.");
    }
}

public class Heal : Skill
{
    public Heal()
    {
        Name = "Heal";
        Description = "Heal an Ally.";
        Cost = 1;
    }

    public override void Execute(Character user, Character target)
    {
        target.CurrentVigor += user.CurrentTech;
        Console.WriteLine(target.Name + " has healed for " + user.CurrentTech + " health.");
    }
}   
public class Energybolt : Skill
{
    public Energybolt()
    {
        Name = "Energybolt";
        Description = "Deal damage to an enemy.";
        Type = "Magical";
        Cost = 1;
    }

    public override int DealDamage(Character user, Character target)
    {
        int damage = user.CurrentTech - target.CurrRes;
        return damage;
    }
}