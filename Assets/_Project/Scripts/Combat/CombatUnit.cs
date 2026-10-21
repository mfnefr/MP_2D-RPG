using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatUnit : MonoBehaviour
{
    [Header("General")]
    private Vector3 _startPos;
    private Rigidbody2D _rb;

    [Header("UI")]
    public UnityEngine.GameObject healthBar;

    [Header("Stats")]
    public int maxHealth;
    public int currentHealth;
    public int armor;
    public float moveDistance;
    public List<Attack> attacks;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;
        currentHealth = maxHealth;
    }

    public int RollInitiative()
    {
        return DiceRoller.RollD20();
    }

    public int RollDamage()
    {
        Attack attack = attacks[Random.Range(0, attacks.Count)];
        return attack.RollDamage();
    }

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
        Debug.Log($"{gameObject.name} attacks {target.gameObject.name} for {damage} damage!");

        yield return new WaitForSeconds(0.5f);

        onSequenceComplete?.Invoke();
    }
}
