using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData itemToDisplay;
    int quantity;

    public Image itemDisplayImage;
    public Text quantityText;

    public enum InventoryType
    {
        Item, Tool
    }

    public InventoryType inventoryType;

    int slotIndex;

    public void Display(ItemSlotData itemSlot)
    {
        itemToDisplay = itemSlot.itemData;
        quantity = itemSlot.quantity;
        quantityText.text = "";
      
        if (itemToDisplay != null)
        {
            //Switch the thumbnail over
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            if (quantity > 1)
            {
                quantityText.text = quantity.ToString();
            }



            itemDisplayImage.gameObject.SetActive(true);

            return;
        }

        itemDisplayImage.gameObject.SetActive(false);


    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryToHand(slotIndex, inventoryType);
    }

    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public void OnPointerEnter(PointerEventData evenData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }
    public void OnPointerExit(PointerEventData evenData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }


}