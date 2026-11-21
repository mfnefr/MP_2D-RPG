using UnityEngine;

public class EnemyCombatUnit : CombatUnit
{
    private bool isPlayerInRange = false;

    public override int RollDamage()
    {
        Attack attack = attacks[Random.Range(0, attacks.Count)];
        return attack.RollDamage();
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
