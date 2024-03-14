public class Item
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public virtual void Use(Character target)
    {
        // Default behavior: do nothing
    }
}

public class HealthPotion : Item
{
    public int HealingAmount { get; set; }

    public HealthPotion()
    {
        Name = "Health Potion";
        Description = "A potion that restores health.";
        HealingAmount = 50;
    }

    public override void Use(Character target)
    {
        target.CurrentVigor += HealingAmount;
        if (target.CurrentVigor > target.CurrVigorMax)
        {
            target.CurrentVigor = target.CurrVigorMax;
        }
    }
}