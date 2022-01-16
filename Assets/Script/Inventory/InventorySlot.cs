using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public Image icon;
	public Button removeButton;

	public Item item;

	public ItemDrag itemDrag;
	// Current item in the slot
							 // Add item to the slot
	public void OnDrop(PointerEventData eventData)
	{
		itemDrag = eventData.pointerDrag.GetComponent<ItemDrag>();
		if (itemDrag != null)
        {
			Item item = itemDrag.GetItem();
			Inventory.instance.Add(item);

			//ObjectManager.instance.EquipSail(item);
			//DESTROY
			itemDrag.ItemToNull();

		}
	}


	public void AddItem(Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot()
	{
		//Debug.Log("ClearSlotInv");
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;

	}

	// If the remove button is pressed, this function will be called.
	public void RemoveItemFromInventory()
	{
		//Debug.Log("RemoveInv");
		Inventory.instance.Remove(item);
	}

	// Use the item
	public void UseItem()
	{
		if (item != null)
		{
			
			item.Use();
		}
	}

}
