using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;

	public AllInfo.GameEquipment gameEquipment;

	public bool ActivePirate = false;
	public AllInfo.GamePirate pirate;

	public void AddCharacter(AllInfo.GamePirate NewPirate)
    {
		pirate = NewPirate;
		ActivePirate = true;
		icon.sprite = NewPirate.pirateBase.icon;
		icon.enabled = true;
	}

	public void AddEquipment(AllInfo.GameEquipment newItem)
	{
		gameEquipment = newItem;
		icon.sprite = newItem.equipmentInfo.icon;
		icon.enabled = true;
	}

	public bool Occupied()
    {
		if (gameEquipment != null)
		{
			if (gameEquipment.equipmentInfo != null)
			{
				return true;
			}
			else
				return false;
		}
		else
			return false;
    }

	public Sprite GetIcon(ScriptableObject newItem)
    {
		if(newItem is PirateInfo)
        {
			PirateInfo pirateInfo = newItem as PirateInfo;
			return pirateInfo.icon;
		}
		else if (newItem is EquipmentInfo)
		{
			EquipmentInfo equipmentInfo = newItem as EquipmentInfo;
			return equipmentInfo.icon;
		}
        else
        {
			Debug.Log("error");
			return null;
        }
	}

	public void ClearSlot()
	{
		gameEquipment = null;
		pirate = null;
		icon.sprite = null;
		icon.enabled = false;
		ActivePirate = false;
	}

	// Use the item
	public void UseItem()
	{
        if (SelectionMenu.instance != null)
        {
			if (gameEquipment != null)
			{
				if (gameEquipment.equipmentInfo != null)
				{
					if (SelectionMenu.instance.CharacterHolders[0].Active == true)
					{
						Selected.instance.SetEquipment(gameEquipment);
						Selected.instance.HelpAll();
					}
					else
					{
						//Debug.LogError("No where to set eqiptment to");
					}
				}
			}
			if (ActivePirate == true)
			{
				SelectionMenu.instance.SetCurrentPirate(pirate.NumInList);
				ActivePirate = false;
				Selected.instance.HelpAll();
			}
		}
		else if (FusionManager.instance != null)
        {
			FusionManager FM = FusionManager.instance;
			if (gameEquipment != null)
			{
				if (gameEquipment.equipmentInfo != null)
				{
					//slot empty
					FusionManager.instance.AddEquipment(gameEquipment);
				}
			}
			if (ActivePirate == true)
			{
				FusionManager.instance.AddPirate(pirate.NumInList);
				ActivePirate = false;
			}
		}
	}
}
