using UnityEngine;

public class PlayerCombatUnit : CombatUnit
{
    void Start()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _armor = 10;
        // _moveDistance = 3.0f;       
    }
}
