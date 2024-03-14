public class Combat
{
    private Character player;
    private Character enemy;

    public Combat(List<Character> players, List<Character> enemies)
    {
        player = players[0];
        enemy = enemies[0];
    }

    public void Start()
    {
        // Combat logic goes here
    }
}