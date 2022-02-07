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

    //public List<ScriptableObject> Active = new List<ScriptableObject>();
    public AllInfo.GamePirate Currentpirate;

    public List<Image> Images = new List<Image>();

    public InventoryHelp Inventoryhelp;
    public InventoryHelp Characterhelp;

    public int CurrentCharacter;

    public void HelpAll()
    {
        Inventoryhelp.UpdateUI();
        Characterhelp.UpdateUI();
    }

    private void Start()
    {
        Currentpirate = AllInfo.instance.GamePirates[CurrentCharacter];
        SetNewPirate();
    }
    public void SetPirateNum(int Num)
    {
        CurrentCharacter = Num;
        Currentpirate = AllInfo.instance.GamePirates[Num];
        SetNewPirate();
    }
    public void ButtonConvertRemoveArmor(int SlotNum)
    {
        RemoveEquipment((EquipmentType)SlotNum);
    }

    public void SetNewPirate()
    {
        for(int i = 0; i < Images.Count; i++)
        {
            Images[i].sprite = null;
            Images[i].enabled = false;
            EquipmentType type = (EquipmentType)i;
            if (HasEquipment(type) == true)
            {
                Images[i].sprite = Currentpirate.gameEquipment[i].equipmentInfo.icon;
                Images[i].enabled = true;
            }
        }
        Inventoryhelp.UpdateUI();
    }

    public void RemoveEquipment(EquipmentType EquipmentType)
    {
        int Equipment = (int)EquipmentType;
        Images[Equipment].sprite = null;
        Images[Equipment].enabled = false;
        AllInfo.instance.GameEquipments.Add(Currentpirate.gameEquipment[Equipment]);
        Currentpirate.gameEquipment[Equipment] = null;
        SubtractStats(Currentpirate.gameEquipment[Equipment]);
        Inventoryhelp.UpdateUI();
    }
    public void SetEquipment(AllInfo.GameEquipment equipment)
    {
        if (HasEquipment(equipment.equipmentInfo.type) == true)
            RemoveEquipment(equipment.equipmentInfo.type);
        int Equipment = (int)equipment.equipmentInfo.type;
        Images[Equipment].sprite = equipment.equipmentInfo.icon;
        Images[Equipment].enabled = true;
        Currentpirate.gameEquipment[Equipment] = equipment;
        AddStats(equipment);
        AllInfo.instance.GameEquipments.Remove(equipment);
        Inventoryhelp.UpdateUI();
    }
    public void AddStats(AllInfo.GameEquipment equipment)
    {
        Currentpirate.Health += equipment.Health;
        Currentpirate.Damage += equipment.Damage;
        Currentpirate.Armour += equipment.Armour;
        Currentpirate.CritPercent += equipment.CritPercent;
        Currentpirate.CritDamage += equipment.CritDamage;
        Currentpirate.Intellect += equipment.Intellect;
        Currentpirate.Dexterity += equipment.Dexterity;
    }
    public void SubtractStats(AllInfo.GameEquipment equipment)
    {
        Currentpirate.Health -= equipment.Health;
        Currentpirate.Damage -= equipment.Health;
        Currentpirate.Armour -= equipment.Health;
        Currentpirate.CritPercent -= equipment.Health;
        Currentpirate.CritDamage -= equipment.Health;
        Currentpirate.Intellect -= equipment.Health;
        Currentpirate.Dexterity -= equipment.Health;
    }
    public bool HasEquipment(EquipmentType type)
    {
        int Num = (int)type;
        if (Num < Currentpirate.gameEquipment.Count)
        {
            if (Currentpirate.gameEquipment[Num] != null)
            {
                if (Currentpirate.gameEquipment[Num].equipmentInfo != null)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }
    public void SetCurrentPirate(AllInfo.GamePirate Pirate)
    {
        Currentpirate = Pirate;
        for (int i = 0; i < Images.Count; i++)
        {
            Images[i].sprite = Pirate.gameEquipment[i].equipmentInfo.icon;
        } 
    }
    public void DisplayNoEquiptment()
    {
        for (int i = 0; i < Images.Count; i++)
        {
            Images[i].enabled = false;
        }
    }
    /*
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
    */
}
