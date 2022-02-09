using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    #region Singleton
    public static UpgradeManager instance;
    void Awake() { instance = this; }
    #endregion
    public AllInfo.GameEquipment equipment;
    public bool equipmentActive;
    public int pirateNum;
    public bool pirateActive;


    /// <summary>
    /// list won't update on start
    /// </summary>

    private void Start()
    {
        UIHelper.UpdateUI();
    }
    public InventoryHelp UIHelper;
    public void DisplayCharacters()
    {
        UIHelper.UpdateUI();
    }

    public void AddPirate(int Num)
    {
        pirateActive = true;
        pirateNum = Num;
    }
    public void AddEquipment(AllInfo.GameEquipment Equipment)
    {
        equipmentActive = true;
        equipment = Equipment;
    }

    public void Back()
    {
        SceneLoader.instance.LoadSceneWithoutFade("Trident");
    }
}
