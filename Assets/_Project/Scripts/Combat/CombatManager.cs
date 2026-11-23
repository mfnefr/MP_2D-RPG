using UnityEngine;
using System.Collections.Generic;
using static InventoryManager;

public class CombatManager : MonoBehaviour
{
    [Header("Object References")]
    public static CombatManager Instance { get; private set; }
    [SerializeField] private CameraController _cameraController;

    [Header("Combatants")]
    public CombatUnit currentPlayer;
    public CombatUnit currentEnemy;

    [Header("Combat Settings")]
    public bool isCombatActive = false;
    public GameObject combatPanel;
    private List<CombatUnit> _turnOrder = new List<CombatUnit>();
    private List<ItemSlot> _combatSlots = new List<ItemSlot>();

    private void Awake()
    {
        Instance = this;
        combatPanel.SetActive(isCombatActive);
    }

    private void MakeTurnOrder()
    {
        _turnOrder.Clear();

        int playerInitiative = currentPlayer.RollInitiative();
        int enemyInitiative = currentEnemy.RollInitiative();

        if (playerInitiative >= enemyInitiative)
        {
            _turnOrder.Add(currentPlayer);
            _turnOrder.Add(currentEnemy);
        }
        else
        {
            _turnOrder.Add(currentEnemy);
            _turnOrder.Add(currentPlayer);
        }
 
        Debug.Log("Enemy Initiative: " + enemyInitiative);
        Debug.Log("Player Initiative: " + playerInitiative);
    }

    private void StartCombat()
    {
        if(_turnOrder[0] == currentPlayer)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    private void PlayerTurn()
    {
        foreach (ItemSlot slot in _combatSlots)
        {
            slot.button.interactable = (slot.item != null);
        }
    }

    private void EndPlayerTurn()
    {
        if(IsCombatOver()) return;
        EnemyTurn();
    }

    private void EnemyTurn()
    {
        StartCoroutine(currentEnemy.AttackSequence(currentPlayer, EndEnemyTurn));
    }

    private void EndEnemyTurn()
    {
        if(IsCombatOver()) return;
        PlayerTurn();
    }

    private bool IsCombatOver()
    {
        if (currentPlayer.currentHealth <= 0)
        {
            Destroy(currentPlayer.gameObject);
            ExitCombat();
            Debug.Log("Player has been defeated!");
            return true;
        }
        else if (currentEnemy.currentHealth <= 0)
        {
            Destroy(currentEnemy.gameObject);
            ExitCombat();
            Debug.Log("Enemy has been defeated!");
            return true;
        }

        return false;
    }

    public void EnterCombat()
    {
        if(isCombatActive) return;

        isCombatActive = true;
        SetSlotsForCombat();
        combatPanel.SetActive(isCombatActive);
        DisableCombatSlots();

        currentPlayer.transform.position = currentPlayer.transform.position;
        _cameraController.ZoomOut();

        MakeTurnOrder();
        StartCombat();
    }

    public void ExitCombat()
    {
        isCombatActive = false;
        combatPanel.SetActive(isCombatActive);
        _cameraController.ZoomIn();
    }

    private void SetSlotsForCombat()
    {
        combatPanel.GetComponentsInChildren(true, _combatSlots);
        List<ItemSlot> inventorySlots = InventoryManager.Instance.GetInventorySlots();
        int index = 0;

        foreach (ItemSlot slot in _combatSlots)
        {
            if (index >= inventorySlots.Count) break;

            foreach (ItemSlot inventorySlot in inventorySlots)
            {
                if  (inventorySlots[index].item != null && inventorySlots[index].item.isUsableInCombat)
                {
                    slot.SetSlot(inventorySlots[index].item, inventorySlots[index].itemCount);
                    index++;
                    break;
                }
            }
        }
    }

    public bool TryUseItem(Item item)
    {
        if(item == null || !item.CanUse(currentPlayer)) return false;
        
        DisableCombatSlots();
        item.Use(currentPlayer, currentEnemy, EndPlayerTurn);
        return true;
    }

    private void DisableCombatSlots()
    {
        foreach (ItemSlot slot in _combatSlots)
        {
            slot.button.interactable = false;
        }
    }
}