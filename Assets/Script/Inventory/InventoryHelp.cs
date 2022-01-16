using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelp : MonoBehaviour
{
    public Inventory inventory;
	public InventorySlot[] slots;
	void Start()
    {
        inventory = Inventory.instance;

        inventory.onItemChangedCallback += UpdateUI;
    }

	public void UpdateUI()
	{
		slots = GetComponentsInChildren<InventorySlot>();

		for (int i = 0; i < slots.Length; i++)
		{
			//count is smaller than the amount of items in inventory
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
				//Debug.Log("addItemUi");
			}
			//count is larger than the amount of items in inventory
			else
			{
				slots[i].ClearSlot();
				//Debug.Log("ClearItemUi");
			}
		}
	}
}
