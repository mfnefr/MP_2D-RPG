using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Scriptable Objects/Items/Weapon")]
public class Weapon : Item
{
    public List<Attack> attacks;

    public void OnValidate()
    {
        isStackable = false;
    }

    public override void Use(CombatUnit user, CombatUnit target, System.Action onActionComplete)
    {
        user.StartCoroutine(user.AttackSequence(target, onActionComplete));
    }
}
