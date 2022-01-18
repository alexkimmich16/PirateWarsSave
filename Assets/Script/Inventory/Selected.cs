using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{
    #region Singleton
    public static Selected instance;
    void Awake() { instance = this; }
    #endregion

    public List<ScriptableObject> Active = new List<ScriptableObject>();
    public AllInfo.GamePirate Currentpirate;

    public Image Hat;
    public Image Armor;
    public Image Bracelet;
    public Image Ring;
    public Image Weapon;

    public InventoryHelp help;  
    private void Start()
    {
        Currentpirate = AllInfo.instance.GamePirates[0];
        SetNewPirate();
    }
    public void ButtonConvertRemoveArmor(int SlotNum)
    {
        RemoveArmor((ArmorType)SlotNum);
    }
    public void SetNewPirate()
    {
        Hat.sprite = null;
        Armor.sprite = null;
        Bracelet.sprite = null;
        Ring.sprite = null;
        Weapon.sprite = null;
        if (Currentpirate.Hat != null)
        {
            Hat.sprite = Currentpirate.Hat.ArmorInfo.icon;
        }
        if (Currentpirate.Armor != null)
        {
            Armor.sprite = Currentpirate.Armor.ArmorInfo.icon;
        }
        if (Currentpirate.Bracelet != null)
        {
            Bracelet.sprite = Currentpirate.Bracelet.ArmorInfo.icon;
        }
        if (Currentpirate.Ring != null)
        {
            Ring.sprite = Currentpirate.Ring.ArmorInfo.icon;
        }
        if (Currentpirate.Weapon != null)
        {
            Weapon.sprite = Currentpirate.Weapon.WeaponInfo.icon;
        }
        help.UpdateUI();
    }

    //armor enum
    public void RemoveArmor(ArmorType armorType)
    {
        if(armorType == ArmorType.Armor)
        {
            Armor.sprite = null;
            Armor.enabled = false;
            AllInfo.instance.GameArmours.Add(Currentpirate.Armor);
            Currentpirate.Armor = null;
        }
        else if (armorType == ArmorType.Bracelet)
        {
            Bracelet.sprite = null;
            Bracelet.enabled = false;
            AllInfo.instance.GameArmours.Add(Currentpirate.Bracelet);
            Currentpirate.Bracelet = null;
        }
        else if (armorType == ArmorType.Hat)
        {
            Hat.sprite = null;
            Hat.enabled = false;
            AllInfo.instance.GameArmours.Add(Currentpirate.Hat);
            Currentpirate.Hat = null;
        }
        else if (armorType == ArmorType.Ring)
        {
            Ring.sprite = null;
            Ring.enabled = false;
            AllInfo.instance.GameArmours.Add(Currentpirate.Ring);
            Currentpirate.Ring = null;
        }
        help.UpdateUI();
    }
    public void RemoveWeapon()
    {
        Weapon.sprite = null;
        Weapon.enabled = false;
        AllInfo.instance.GameWeapons.Add(Currentpirate.Weapon);
        Currentpirate.Weapon = null;
        help.UpdateUI();
    }
    public void EquipArmor(AllInfo.GameArmour armor)
    {
        if (armor.ArmorInfo.Armor == ArmorType.Armor)
        {
            if (Currentpirate.Armor != null)
            {
                if (Currentpirate.Armor.ArmorInfo != null)
                {
                    RemoveArmor(armor.ArmorInfo.Armor);
                }
            } 
            Armor.enabled = true;
            Armor.sprite = armor.ArmorInfo.icon;
            Currentpirate.Armor = armor;
        }
        else if(armor.ArmorInfo.Armor == ArmorType.Bracelet)
        {
            if (Currentpirate.Bracelet != null)
            {
                if (Currentpirate.Bracelet.ArmorInfo != null)
                {
                    RemoveArmor(armor.ArmorInfo.Armor);
                }
            }
            Bracelet.enabled = true;
            Bracelet.sprite = armor.ArmorInfo.icon;
            Currentpirate.Bracelet = armor;
        }
        else if(armor.ArmorInfo.Armor == ArmorType.Hat)
        {
            if (Currentpirate.Hat != null)
            {
                if (Currentpirate.Hat.ArmorInfo != null)
                {
                    RemoveArmor(armor.ArmorInfo.Armor);
                }
            }
            
            Hat.enabled = true;
            Hat.sprite = armor.ArmorInfo.icon;
            Currentpirate.Hat = armor;
        }
        else if(armor.ArmorInfo.Armor == ArmorType.Ring)
        {
            if (Currentpirate.Ring != null)
            {
                if (Currentpirate.Ring.ArmorInfo != null)
                {
                    RemoveArmor(armor.ArmorInfo.Armor);
                }
            }
                
            Ring.enabled = true;
            Ring.sprite = armor.ArmorInfo.icon;
            Currentpirate.Ring = armor;
        }
        AllInfo.instance.GameArmours.Remove(armor);
    }

    public void EquipWeapon(AllInfo.GameWeapon weapon)
    {
        if(Currentpirate.Weapon != null)
        {
            if (Currentpirate.Weapon.WeaponInfo != null)
            {
                RemoveWeapon();
            }
        }
        Weapon.enabled = true;
        Weapon.sprite = weapon.WeaponInfo.icon;
        AllInfo.instance.GameWeapons.Remove(weapon);
        Currentpirate.Weapon = weapon;
    }
    public void AddAtActive(int Count, ScriptableObject ToAdd)
    {
        Active[Count] = ToAdd;
        return;
    }
    public void SetCurrentPirate(AllInfo.GamePirate Pirate)
    {
        Currentpirate = Pirate;
        Hat.sprite = Pirate.Hat.ArmorInfo.icon;
        Armor.sprite = Pirate.Armor.ArmorInfo.icon;
        Bracelet.sprite = Pirate.Bracelet.ArmorInfo.icon;
        Ring.sprite = Pirate.Ring.ArmorInfo.icon;
        Weapon.sprite = Pirate.Weapon.WeaponInfo.icon;
    }

    public void AddToActive(ScriptableObject ToAdd)
    {
        for (int i = 0; i < Active.Count; i++)
        {
            if (Active[i] == null)
            {
                Active[i] = ToAdd;
                return;
            }
        }
    }
}
