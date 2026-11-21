using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewUnitStats", menuName = "Scriptable Objects/Combat/Unit Stats")]
public class UnitStats : ScriptableObject
{
    [Header("Stats")]
    public string unitName;
    public int maxHealth;
    public int armor;
    public float moveDistance;
    public List<Attack> attacks;
}
