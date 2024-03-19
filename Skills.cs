using System.Reflection.Metadata;
using System.Security;

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
        user.AddStatusEffect(new Bolstered());
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
        Cost = 0;
    }

    public override int DealDamage(Character user, Character target)
    {
    
        int damage = (user.DealtDamage + user.CurrentVigor) - target.CurrArmor;
        Console.WriteLine(user.Name + "Stikes " + target.Name + " for " + damage + " damage.");
        return damage;
    
    }
}

public class StealthStrike : Skill
{
    public StealthStrike()
    {
        Name = "Stealth Strike";
        Description = "A basic attack that pierces all armor.";
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
    }
}   
public class Energybolt : Skill
{
    public Energybolt()
    {
        Name = "Energybolt";
        Description = "Deal damage to an enemy.";
        Cost = 1;
    }

    public override int DealDamage(Character user, Character target)
    {
        int damage = user.CurrentTech - target.CurrRes;
        return damage;
    }
}