using UnityEngine;

public class EnemyCombatUnit : CombatUnit
{
    private bool isPlayerInRange = false;

    void Start()
    {
        _maxHealth = 30;
        _currentHealth = _maxHealth;
        _armor = 5;
        //_moveDistance = 2.0f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // CombatManager.Instance.currentEnemy = this;
            // CombatManager.Instance.EnterCombatMode();
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
