using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SortType
{
    Both = 0,
    Weapons = 1,
    Pirates = 2,
}
public class FusionManager : MonoBehaviour
{
    #region Singleton
    public static FusionManager instance;
    void Awake() { instance = this; }

    [System.Serializable]
    public class FuseSlots
    {
        public string SlotName;
        public int InventoryNum;
        public bool PirateActive;
        public bool EquipmentActive;
        public AllInfo.GameEquipment Equipment;
        public Image image;

        public bool Full()
        {
            if (PirateActive == true || EquipmentActive == true)
            {
                return true;
            }
            else
                return false;
        }
    }
    #endregion

    public GameObject AreYouSureMenu;
    public GameObject BaseMenu;
    public SortType sort;
    
    public List<FuseSlots> Slots;

    public InventoryHelp helpUI;

    public void RemoveSlot(int Slot)
    {
        Slots[Slot].PirateActive = false;
        Slots[Slot].EquipmentActive = false;
        Slots[Slot].InventoryNum = 0;
        Slots[Slot].image.sprite = null;
        Slots[Slot].image.enabled = false;
        helpUI.UpdateUI();
    }
    public void AddPirate(int InventoryNum)
    {
        if (Slots[0].Full() == false)
        {
            Slots[0].PirateActive = true;
            Slots[0].InventoryNum = InventoryNum;
            Slots[0].image.enabled = true;
            Slots[0].image.sprite = AllInfo.instance.GamePirates[InventoryNum].pirateBase.icon;
        }
        else
        {
            Slots[1].PirateActive = true;
            Slots[1].InventoryNum = InventoryNum;
            Slots[1].image.enabled = true;
            Slots[1].image.sprite = AllInfo.instance.GamePirates[InventoryNum].pirateBase.icon;
        }
        //Debug.Log("pirate");
        helpUI.UpdateUI();
    }
    public void AddEquipment(AllInfo.GameEquipment NewEquiptment)
    {
        if (Slots[0].Full() == false)
        {
            Slots[0].EquipmentActive = true;
            Slots[0].Equipment = NewEquiptment;
            Slots[0].image.enabled = true;
            Slots[0].image.sprite = NewEquiptment.equipmentInfo.icon;
        }
        else
        {
            Slots[1].EquipmentActive = true;
            Slots[1].Equipment = NewEquiptment;
            Slots[1].image.enabled = true;
            Slots[1].image.sprite = NewEquiptment.equipmentInfo.icon;

        }
        helpUI.UpdateUI();
    }
    public void SetWeaponType()
    {
        if (sort == SortType.Both || sort == SortType.Weapons)
        {
            sort = SortType.Weapons;
        }
        else if (sort == SortType.Pirates)
        {
            sort = SortType.Both;
        }
        helpUI.UpdateUI();
    }
    public void SetPirateType()
    {
        if (sort == SortType.Both || sort == SortType.Pirates)
        {
            sort = SortType.Pirates;
        }
        else if (sort == SortType.Weapons)
        {
            sort = SortType.Both;
        }
        helpUI.UpdateUI();
    }
    public void FuseButton()
    {
        AreYouSureMenu.SetActive(true);
        BaseMenu.SetActive(false);
    }
    public void Proceed()
    {
        List<AllInfo.GamePirate> Pirates = new List<AllInfo.GamePirate>();
        Pirates.Add(new AllInfo.GamePirate());
        Pirates.Add(new AllInfo.GamePirate());
        List<AllInfo.GameEquipment> Equipment = new List<AllInfo.GameEquipment>();
        Equipment.Add(new AllInfo.GameEquipment());
        Equipment.Add(new AllInfo.GameEquipment());

        if (Slots[0].PirateActive == true)
        {
            Pirates[0] = AllInfo.instance.GamePirates[Slots[0].InventoryNum];
        }
        else if (Slots[0].EquipmentActive == true)
            Equipment[0] = Slots[0].Equipment;
        else
            return;

        if (Slots[1].PirateActive == true)
            Pirates[1] = AllInfo.instance.GamePirates[Slots[1].InventoryNum];
        else if (Slots[1].EquipmentActive == true)
            Equipment[1] = Slots[1].Equipment;
        else
            return;

        AllInfo.StatMultiplier before = new AllInfo.StatMultiplier();

        SceneLoader.instance.Before = new AllInfo.StatMultiplier();

        Fuse(Pirates, Equipment);

        AreYouSureMenu.SetActive(false);
        BaseMenu.SetActive(true);

        SceneLoader.instance.LoadScene("Upgraded");
    }
    public void Fuse(List<AllInfo.GamePirate> Pirates, List<AllInfo.GameEquipment> Equipment)
    {
        float RankMultiplier = 100f;
        float LevelMultiplier = 10f;
        if (Pirates[0].pirateBase != null)
        {
            int FirstPirateNum = AllInfo.instance.PirateNum(Pirates[0]);
            SceneLoader.instance.IsPirate = true;
            SceneLoader.instance.TypeNumInList = FirstPirateNum;
            SceneLoader.instance.Before = GetPirateStats(FirstPirateNum);
            if (Pirates[1].pirateBase != null)
            {
                int SecondNum = AllInfo.instance.PirateNum(Pirates[1]);
                float IncreaseFloat = (Pirates[1].Level * LevelMultiplier) + (RankMultiplier * Pirates[1].Rank);
                int Increase = (int)IncreaseFloat;
                AllInfo.instance.GamePirates[FirstPirateNum].AddExperience(Increase);
                AllInfo.instance.GamePirates.Remove(AllInfo.instance.GamePirates[SecondNum]);
            }
            else if (Equipment[1].equipmentInfo != null)
            {
                int SecondNum = AllInfo.instance.EquipmentNum(Equipment[1]);
                float IncreaseFloat = (Equipment[1].Level * LevelMultiplier) + RankMultiplier * Equipment[1].Rank;
                int Increase = (int)IncreaseFloat;
                AllInfo.instance.GamePirates[FirstPirateNum].AddExperience(Increase);
                AllInfo.instance.GameEquipments.Remove(AllInfo.instance.GameEquipments[SecondNum]);
            }
            SceneLoader.instance.Added = GetPirateStats(FirstPirateNum);
        }
        else if (Equipment[0].equipmentInfo != null)
        {
            int FirstEquipmentNum = AllInfo.instance.EquipmentNum(Equipment[0]);
            SceneLoader.instance.IsPirate = false;
            SceneLoader.instance.TypeNumInList = FirstEquipmentNum;
            SceneLoader.instance.Before = GetEquipmentStats(FirstEquipmentNum);
            if (Pirates[1].pirateBase != null)
            {
                int SecondNum = AllInfo.instance.PirateNum(Pirates[1]);
                float IncreaseFloat = (Pirates[1].Level * LevelMultiplier) + (RankMultiplier * Pirates[1].Rank);
                int Increase = (int)IncreaseFloat;
                AllInfo.instance.GameEquipments[FirstEquipmentNum].AddExperience(Increase);
                AllInfo.instance.GamePirates.Remove(AllInfo.instance.GamePirates[SecondNum]);
            }
            else if (Equipment[1].equipmentInfo != null)
            {
                int SecondNum = AllInfo.instance.EquipmentNum(Equipment[1]);
                float IncreaseFloat = (Equipment[1].Level * LevelMultiplier) + RankMultiplier * Equipment[1].Rank;
                int Increase = (int)IncreaseFloat;
                AllInfo.instance.GameEquipments[FirstEquipmentNum].AddExperience(Increase);
                AllInfo.instance.GameEquipments.Remove(AllInfo.instance.GameEquipments[SecondNum]);
            }
            SceneLoader.instance.Added = GetEquipmentStats(FirstEquipmentNum);
        }

        AllInfo.StatMultiplier GetPirateStats(int ListNum)
        {
            AllInfo.StatMultiplier stats = new AllInfo.StatMultiplier();
            stats.Health = AllInfo.instance.GamePirates[ListNum].Health;
            stats.Damage = AllInfo.instance.GamePirates[ListNum].Damage;
            stats.Armour = AllInfo.instance.GamePirates[ListNum].Armour;
            stats.CritPercent = AllInfo.instance.GamePirates[ListNum].CritPercent;
            stats.Intellect = AllInfo.instance.GamePirates[ListNum].Intellect;
            stats.Dexterity = AllInfo.instance.GamePirates[ListNum].Dexterity;
            return stats;
        }
        AllInfo.StatMultiplier GetEquipmentStats(int ListNum)
        {
            AllInfo.StatMultiplier stats = new AllInfo.StatMultiplier();
            stats.Health = AllInfo.instance.GameEquipments[ListNum].Health;
            stats.Damage = AllInfo.instance.GameEquipments[ListNum].Damage;
            stats.Armour = AllInfo.instance.GameEquipments[ListNum].Armour;
            stats.CritPercent = AllInfo.instance.GameEquipments[ListNum].CritPercent;
            stats.Intellect = AllInfo.instance.GameEquipments[ListNum].Intellect;
            stats.Dexterity = AllInfo.instance.GameEquipments[ListNum].Dexterity;
            return stats;
        }
    }
    public void DontProceed()
    {
        AreYouSureMenu.SetActive(false);
        BaseMenu.SetActive(true);
    }
    public void Back()
    {
        SceneLoader.instance.LoadScene("Main");
    }
}
