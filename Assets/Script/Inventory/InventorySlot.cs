using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;

	//public ScriptableObject item;

	public AllInfo.GameArmour gameArmor;
	public AllInfo.GameWeapon gameWeapon;

	public void AddArmor(AllInfo.GameArmour newItem)
	{
		gameArmor = newItem;
		icon.sprite = newItem.ArmorInfo.icon;
		icon.enabled = true;
		//removeButton.interactable = true;
	}
	public void AddWeapon(AllInfo.GameWeapon newItem)
	{
		gameWeapon = newItem;
		icon.sprite = newItem.WeaponInfo.icon;
		icon.enabled = true;
		//removeButton.interactable = true;
	}

	public bool Occupied()
    {
		if (gameWeapon != null)
        {
            if (gameWeapon.WeaponInfo != null)
            {
				return true;
			}
        }

		if (gameArmor != null)
		{
			if (gameArmor.ArmorInfo != null)
			{
				return true;
			}
            else
            {
				return false;
            }
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
		else if (newItem is WeaponInfo)
		{
			WeaponInfo weaponInfo = newItem as WeaponInfo;
			return weaponInfo.icon;
		}
		else if (newItem is ArmourInfo)
		{
			ArmourInfo armourInfo = newItem as ArmourInfo;
			return armourInfo.icon;
		}
        else
        {
			Debug.Log("error");
			return null;
        }
	}

	public void ClearSlot()
	{
		gameArmor = null;
		gameWeapon = null;
		icon.sprite = null;
		icon.enabled = false;
	}

	// Use the item
	public void UseItem()
	{
        if (gameArmor != null)
        {
			if (gameArmor.ArmorInfo != null)
			{
				//Debug.Log("pt2");
				Selected.instance.EquipArmor(gameArmor);
			}
		}
        if (gameWeapon != null)
        {
			if (gameWeapon.WeaponInfo != null)
			{
				//Debug.Log("pt1");
				Selected.instance.EquipWeapon(gameWeapon);
			}
		}
		
	}

}
