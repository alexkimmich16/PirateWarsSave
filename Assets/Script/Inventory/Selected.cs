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
    //public AllInfo.GamePirate Currentpirate;

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
        //Currentpirate = AllInfo.instance.GamePirates[CurrentCharacter];
        SetNewPirate();
    }
    public void SetPirateNum(int Num)
    {
        CurrentCharacter = Num;
        AllInfo.instance.GamePirates[CurrentCharacter] = AllInfo.instance.GamePirates[Num];
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
                Images[i].sprite = AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[i].equipmentInfo.icon;
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
        SubtractStats(AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Equipment]);
        AllInfo.instance.GameEquipments.Add(AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Equipment]);
        AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Equipment] = null;
        Inventoryhelp.UpdateUI();
    }
    public void SetEquipment(AllInfo.GameEquipment equipment)
    {
        if (HasEquipment(equipment.equipmentInfo.type) == true)
            RemoveEquipment(equipment.equipmentInfo.type);
        int Equipment = (int)equipment.equipmentInfo.type;
        Images[Equipment].sprite = equipment.equipmentInfo.icon;
        Images[Equipment].enabled = true;
        AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Equipment] = equipment;
        AddStats(equipment);
        AllInfo.instance.GameEquipments.Remove(equipment);
        Inventoryhelp.UpdateUI();
    }
    public void AddStats(AllInfo.GameEquipment equipment)
    {
        AllInfo.instance.GamePirates[CurrentCharacter].Health += equipment.Health;
        AllInfo.instance.GamePirates[CurrentCharacter].Damage += equipment.Damage;
        AllInfo.instance.GamePirates[CurrentCharacter].Armour += equipment.Armour;
        AllInfo.instance.GamePirates[CurrentCharacter].CritPercent += equipment.CritPercent;
        AllInfo.instance.GamePirates[CurrentCharacter].CritDamage += equipment.CritDamage;
        AllInfo.instance.GamePirates[CurrentCharacter].Intellect += equipment.Intellect;
        AllInfo.instance.GamePirates[CurrentCharacter].Dexterity += equipment.Dexterity;
    }
    public void SubtractStats(AllInfo.GameEquipment equipment)
    {
        AllInfo.instance.GamePirates[CurrentCharacter].Health -= equipment.Health;
        AllInfo.instance.GamePirates[CurrentCharacter].Damage -= equipment.Damage;
        AllInfo.instance.GamePirates[CurrentCharacter].Armour -= equipment.Armour;
        AllInfo.instance.GamePirates[CurrentCharacter].CritPercent -= equipment.CritPercent;
        AllInfo.instance.GamePirates[CurrentCharacter].CritDamage -= equipment.CritDamage;
        AllInfo.instance.GamePirates[CurrentCharacter].Intellect -= equipment.Intellect;
        AllInfo.instance.GamePirates[CurrentCharacter].Dexterity -= equipment.Dexterity;
    }
    public bool HasEquipment(EquipmentType type)
    {
        int Num = (int)type;
        if (Num < AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment.Count)
        {
            if (AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Num] != null)
            {
                if (AllInfo.instance.GamePirates[CurrentCharacter].gameEquipment[Num].equipmentInfo != null)
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
        AllInfo.instance.GamePirates[CurrentCharacter] = Pirate;
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
