using UnityEngine;
using System.Collections.Generic;

public class EnemyCombatUnit : CombatUnit
{
    private bool isPlayerInRange = false;
    [SerializeField] private List<LootDrop> _lootTable = new List<LootDrop>();

    public override int RollDamage()
    {
        Attack attack = attacks[Random.Range(0, attacks.Count)];
        return attack.RollDamage();
    }

    public void DropLoot()
    {
        foreach(LootDrop drop in _lootTable)
        {
            if(drop.item == null) continue;

            if (!string.IsNullOrEmpty(drop.requiredQuest))
            {
                object questState = DialogueManager.Instance.GetStoryVariable(drop.requiredQuest);

                if(questState == null || (int)questState != 1)
                {
                    continue;
                }
            }

            if(Random.Range(0, 100f) <= drop.dropChance)
            {
                InventoryManager.Instance.AddItem(drop.item);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(CombatManager.Instance.isCombatActive) return;

        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            CombatManager.Instance.currentEnemy = this;
            CombatManager.Instance.EnterCombat();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
