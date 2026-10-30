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
        if(target == null) return false;

        if(healAmount > 0)
        {
            target.currentHealth += DiceRoller.Roll(1, healAmount);
            return true;
        }
        
        return false;
    }
}
