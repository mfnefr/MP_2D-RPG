using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject inventoryPanel;
    public bool isInventoryOpen;
    private List<ItemSlot> _inventorySlots = new List<ItemSlot>();

    private void Awake()
    {
        Instance = this;
        inventoryPanel.GetComponentsInChildren(true, _inventorySlots);
        inventoryPanel.SetActive(isInventoryOpen);
    }

    private void Update()
    {
        if(Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }
    }

    public List<ItemSlot> GetInventorySlots(){
        return _inventorySlots;
    }

    public void AddItem(Item itemToAdd, int count = 1)
    {
        if (itemToAdd == null) return;

        if (itemToAdd.isStackable)
        {
            foreach(ItemSlot slot in _inventorySlots)
            {
                if(slot.item != null && slot.item.itemID == itemToAdd.itemID)
                {
                    slot.AddCount(count);
                    return;
                }
            }
        }

        foreach(ItemSlot slot in _inventorySlots)
        {
            if(slot.item == null)
            {
                slot.SetSlot(itemToAdd, count);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }

    public void AddItemByName(string resourceName, int count)
    {
        Item loadedItem = Resources.Load<Item>("items/" + resourceName);

        if(loadedItem == null)
        {
            Debug.Log($"Item {resourceName} not found in Resources/items/ folder.");
            return;
        }

        AddItem(loadedItem, count);
    }

    public bool RemoveItem(string itemName, int countToRemove = 1)
    {
        foreach(ItemSlot slot in _inventorySlots)
        {
            if(slot.item != null && slot.item.itemName == itemName)
            {
                slot.AddCount(-countToRemove);

                if(slot.itemCount <= 0)
                {
                    slot.ClearSlot();
                }

                return true;
            }
        }

        return false;
    }

    public bool HasItem(string itemName)
    {
        Debug.Log($"{itemName}");
        
        foreach(ItemSlot slot in _inventorySlots)
        {
            if(slot.item != null && slot.item.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }
}
