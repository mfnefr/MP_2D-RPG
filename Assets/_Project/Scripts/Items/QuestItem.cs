using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestItem", menuName = "Scriptable Objects/Items/QuestItem")]
public class QuestItem : Item
{
    private void OnValidate()
    {
        isUsableInCombat = false;
    }

    public override bool CanUse(CombatUnit user) => false;
    public override void Use(CombatUnit user, CombatUnit target, System.Action onComplete) { }
}
