using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
    [Header("General")]
    [SerializeField] private string _attackName;

    [Header("Damage Dice")]
    [SerializeField] private int _diceCount;
    [SerializeField] private int _diceSides;

    public int RollToHit()
    {
        return DiceRoller.RollD20();
    }

    public int RollDamage()
    {
        return DiceRoller.Roll(_diceCount, _diceSides);
    }
}
