using UnityEngine;
using System.Collections.Generic;

public class CombatUnit : MonoBehaviour
{
    [Header("General")]
    private Vector3 _startPos;
    private Rigidbody2D _rb;

    [Header("UI")]
    public GameObject _healthBar;

    [Header("Stats")]
    public int _maxHealth;
    public int _currentHealth;
    public int _armor;
    public float _moveDistance;
    public List<Attack> _attacks;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;
    }

    public int RollIniciative()
    {
        return DiceRoller.RollD20();
    }

    public int RollDamage()
    {
        Attack attack = _attacks[Random.Range(0, _attacks.Count)];
        return attack.RollDamage();
    }
}
