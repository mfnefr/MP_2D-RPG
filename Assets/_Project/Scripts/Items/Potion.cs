using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Scriptable Objects/Items/Potion")]
public class Potion : Item
{
    [Header("Potion Data")]
    public int healAmount;

    public override void Use(CombatUnit user, CombatUnit target, System.Action onActionComplete)
    {
        user.currentHealth = Mathf.Min(user.currentHealth + DiceRoller.Roll(1, healAmount), user.maxHealth);
        
        onActionComplete?.Invoke();
    }
}
