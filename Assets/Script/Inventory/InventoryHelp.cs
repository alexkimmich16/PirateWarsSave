using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelp : MonoBehaviour
{
	public InventorySlot[] slots;
	void Start()
    {
		UpdateUI();
	}

    private void Update()
    {
		//UpdateUI();
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
			if (i < AllInfo.instance.GameEquipments.Count)
			{
				slots[i].AddEquipment(AllInfo.instance.GameEquipments[i]);
			}
			else
			{
				slots[i].ClearSlot();
				//Debug.Log("ClearItemUi");
			}
		}
	}
}
