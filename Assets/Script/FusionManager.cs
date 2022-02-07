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
    //public List<AllInfo.GameEquipment> Equipments;
    //public List<AllInfo.GamePirate> Pirates;
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
        Debug.Log("pirate");
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
        Fuse();
    }

    public void Fuse()
    {

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
