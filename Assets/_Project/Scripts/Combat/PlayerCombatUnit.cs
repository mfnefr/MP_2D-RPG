using UnityEngine;

public class PlayerCombatUnit : CombatUnit
{
    public override int RollDamage()
    {
        Attack attack = attacks[Random.Range(0, attacks.Count)];
        return attack.RollDamage();
    }
}
