using UnityEngine;

public static class DiceRoller
{
    public const int D20_SIDES = 20;

    public static int Roll(int diceCount, int diceSides)
    {
        int totalRoll = 0;

        for(int i = 0; i < diceCount; i++)
        {
            totalRoll += Random.Range(1, diceSides + 1);
        }

        return totalRoll;
    }

    // Used for iniciative rolls and attack (for hit) rolls, those are always d20 rolls
    public static int RollD20()
    {
        return Roll(1, D20_SIDES);
    }
}
