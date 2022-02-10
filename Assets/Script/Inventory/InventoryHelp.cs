using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelp : MonoBehaviour
{
	public InventorySlot[] slots;
	public bool Character;

	/// <summary>
	/// slot being cleared randomly
	/// </summary>
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
        if (SelectionMenu.instance != null)
        {
			if (Character == true)
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
        else if(FusionManager.instance != null)
        {
			for (int i = 0; i < slots.Length; i++)
			{
				slots[i].ClearSlot();
			}
			FusionManager FM = FusionManager.instance;
			
			List<AllInfo.GamePirate> InventoryPirates = new List<AllInfo.GamePirate>(AllInfo.instance.GamePirates);
			int InsideCount = 0;
			if (FM.sort == SortType.Pirates || FM.sort == SortType.Both)
            {
				InsideCount = InventoryPirates.Count;
				for (int i = 0; i < AllInfo.instance.GamePirates.Count; i++)
				{
					if (FM.Slots[0].PirateActive == true && FM.Slots[0].InventoryNum == i)
                    {
						InventoryPirates.Remove(AllInfo.instance.GamePirates[i]);
						InsideCount -= 1;
					}
					else if(FM.Slots[1].PirateActive == true && FM.Slots[1].InventoryNum == i)
                    {
						InventoryPirates.Remove(AllInfo.instance.GamePirates[i]);
						InsideCount -= 1;
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

			List<AllInfo.GameEquipment> InventoryEquipments = new List<AllInfo.GameEquipment>(AllInfo.instance.GameEquipments);
			if (FM.sort == SortType.Weapons || FM.sort == SortType.Both)
            {
				for (int i = 0; i < AllInfo.instance.GameEquipments.Count; i++)
                {
					if (FM.Slots[0].EquipmentActive == true && FM.Slots[0].Equipment == AllInfo.instance.GameEquipments[i] ||
						FM.Slots[1].EquipmentActive == true && FM.Slots[1].Equipment == AllInfo.instance.GameEquipments[i])
					{
						InventoryEquipments.Remove(AllInfo.instance.GameEquipments[i]);
					}
				}

				for (int i = 0; i < slots.Length; i++)
				{
					if (i < InventoryEquipments.Count + InsideCount && i > InsideCount - 1)
					{
						
						slots[i].AddEquipment(InventoryEquipments[i - InsideCount]);
					}
				}
			}
        }
		else if (UpgradeManager.instance != null)
        {
			for (int i = 0; i < slots.Length; i++)
			{
				slots[i].ClearSlot();
			}
			UpgradeManager UM = UpgradeManager.instance;

			List<AllInfo.GamePirate> InventoryPirates = new List<AllInfo.GamePirate>(AllInfo.instance.GamePirates);
			int InsideCount = 0;
			InsideCount = InventoryPirates.Count;
			for (int i = 0; i < AllInfo.instance.GamePirates.Count; i++)
			{
				if (UM.pirateActive == true && UM.pirateNum == i)
				{
					InventoryPirates.Remove(AllInfo.instance.GamePirates[i]);
					InsideCount -= 1;
				}
			}
			for (int i = 0; i < slots.Length; i++)
			{
				//count is smaller than the amount of items in inventory
				if (i < InventoryPirates.Count)
					slots[i].AddCharacter(InventoryPirates[i]);
				else
					slots[i].ClearSlot();
			}
			/*
			List<AllInfo.GameEquipment> InventoryEquipments = new List<AllInfo.GameEquipment>(AllInfo.instance.GameEquipments);
			for (int i = 0; i < AllInfo.instance.GameEquipments.Count; i++)
			{
				if (UM.equipmentActive == true && UM.equipment == AllInfo.instance.GameEquipments[i])
				{
					InventoryEquipments.Remove(AllInfo.instance.GameEquipments[i]);
				}
			}

			for (int i = 0; i < slots.Length; i++)
			{
				if (i < InventoryEquipments.Count + InsideCount && i > InsideCount - 1)
				{
					slots[i].AddEquipment(InventoryEquipments[i - InsideCount]);
				}
			}
			*/
		}


	}
}
