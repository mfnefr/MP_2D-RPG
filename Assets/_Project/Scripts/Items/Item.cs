using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("General Data")]
    public int itemID;
    public string itemName;
    public Sprite icon;
    public bool isStackable = true;
    public bool isUsableInCombat = false;

    public abstract void Use(CombatUnit user, CombatUnit target, System.Action onActionComplete);

    public virtual bool CanUse(CombatUnit user) => user != null;
}
