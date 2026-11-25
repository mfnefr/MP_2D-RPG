using UnityEngine;

[System.Serializable]
public class LootDrop
{
    public Item item;
    public string requiredQuest; // name of ink quest
    public float dropChance; // values in range: 0f - 100f
}