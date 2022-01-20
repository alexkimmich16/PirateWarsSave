﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;

	public AllInfo.GameEquipment gameEquipment;

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
		icon.sprite = null;
		icon.enabled = false;
	}

	// Use the item
	public void UseItem()
	{
        if (gameEquipment != null)
        {
			if (gameEquipment.equipmentInfo != null)
			{
				//Debug.Log("pt2");
				Selected.instance.SetEquipment(gameEquipment);
			}
		}
		
	}

}
