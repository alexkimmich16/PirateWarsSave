using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelp : MonoBehaviour
{
	public InventorySlot[] slots;
	void Start()
    {
        //inventory = Inventory.instance;

        //inventory.onItemChangedCallback += UpdateUI;
		UpdateUI();

	}

    private void Update()
    {
		UpdateUI();
	}

    public void UpdateUI()
	{
		slots = GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearSlot();
		}
		for (int i = 0; i < slots.Length; i++)
		{
			//count is smaller than the amount of items in inventory
			if (i < AllInfo.instance.GameWeapons.Count + AllInfo.instance.GameArmours.Count)
			{
				if (i < AllInfo.instance.GameWeapons.Count)
				{
					slots[i].AddWeapon(AllInfo.instance.GameWeapons[i]);
				}
				else
				{
					slots[i].AddArmor(AllInfo.instance.GameArmours[i - AllInfo.instance.GameWeapons.Count]);
				}

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
