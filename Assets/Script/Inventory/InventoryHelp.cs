using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelp : MonoBehaviour
{
	public InventorySlot[] slots;
	public bool Character;


	void Start()
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
		if(Character == true)
        {
			//get all in inventory
			List<AllInfo.GamePirate> InventoryPirates = new List<AllInfo.GamePirate>(AllInfo.instance.GamePirates);
			for (int i = 0; i < SelectionMenu.instance.CharacterHolders.Count; i++)
			{
				if (SelectionMenu.instance.CharacterHolders[i].Active == true)
				{
					int Num = SelectionMenu.instance.CharacterHolders[i].NumInList;
					InventoryPirates.Remove(AllInfo.instance.GamePirates[Num]);
				}
			}

			for (int i = 0; i < slots.Length; i++)
			{
				//count is smaller than the amount of items in inventory
				if (i < InventoryPirates.Count)
				{
					slots[i].AddCharacter(InventoryPirates[i]);
				}
				else
				{
					slots[i].ClearSlot();
				}
			}
		}
        else
        {
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
				}
			}
		}
	}
}
