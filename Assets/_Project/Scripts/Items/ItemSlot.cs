using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Item Data")]
    public Item item;
    public int itemCount;

    [Header("UI References")]
    public Image icon;
    public TextMeshProUGUI itemCountText;

    private void Start()
    {
        UpdateUI();
    }

    public void SetSlot(Item newItem, int count = 1)
    {
        item = newItem;
        itemCount = count;
        UpdateUI();
    }

    public void AddCount(int amount)
    {
        itemCount += amount;
        UpdateUI();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(item != null && itemCount > 0)
        {
            icon.sprite = item.icon;
            icon.gameObject.SetActive(true);
            itemCountText.text = (item.isStackable && itemCount > 1) ? itemCount.ToString() : string.Empty;
        }
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            itemCountText.text = string.Empty;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(item == null) return;

        if (item.Use(CombatManager.Instance.currentPlayer))
        {
            AddCount(-1);
            
            if(itemCount <= 0)
            {
                ClearSlot();
            }
        }
    }
}
