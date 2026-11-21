using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class CombatUnit : MonoBehaviour
{
    [Header("General")]
    private Vector3 _startPos;
    protected Rigidbody2D _rb;

    [Header("UI")]
    public GameObject healthBar;

    [Header("Stats")]
    public UnitStats _stats;
    public int maxHealth => _stats.maxHealth;
    public int armor => _stats.armor;
    public float moveDistance => _stats.moveDistance;
    public int currentHealth;
    public List<Attack> attacks => _stats.attacks;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;
        currentHealth = maxHealth;
    }

    public int RollInitiative() => DiceRoller.RollD20();
    public abstract int RollDamage();

    public IEnumerator AttackSequence(CombatUnit target, System.Action onSequenceComplete)
    {
        Vector2 targetPos = (Vector2)target.transform.position + ((_rb.position - (Vector2)target.transform.position).normalized * 1.5f);
        
        while(Vector2.Distance(_rb.position, targetPos) > 0.5f)
        {
            Vector2 newPos = Vector2.MoveTowards(_rb.position, targetPos, moveDistance * Time.fixedDeltaTime);
            _rb.MovePosition(newPos);

            yield return new WaitForFixedUpdate();
        }

        int damage = RollDamage();
        target.currentHealth -= damage;
        Debug.Log($"{gameObject.name} attacked {target.gameObject.name} for {damage} damage!");

        yield return new WaitForSeconds(0.5f);

        onSequenceComplete?.Invoke();
    }
}
