using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("General Data")]
    public int itemID;
    public string itemName;
    public Sprite icon;
    public bool isStackable = true;

    [Header("Item Effects")]
    public bool isUsableInCombat = false;
    public int healAmount = 0;
    public int bonusDamage = 0;

    public bool Use(CombatUnit target)
    {
        if(target != null && healAmount > 0)
        {
            Heal(target);
            return true;
        }else if(target != null && bonusDamage > 0)
        {
            return true;
        }
        
        return false;
    }

    private void Heal(CombatUnit target)
    {
        target.currentHealth += DiceRoller.Roll(1, healAmount) + 4;
    }
}
