using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image image;


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
        SceneLoader.instance.DisplayNum = Num;
        SceneLoader.instance.DisplayCharacter = true;

        if (image != null)
        {
            image.enabled = true;
            image.sprite = AllInfo.instance.GamePirates[Num].pirateBase.icon;
        }
            
        UIHelper.UpdateUI();

    }
    public void AddEquipment(AllInfo.GameEquipment Equipment)
    {
        equipmentActive = true;
        equipment = Equipment;
        if (image != null)
        {
            image.sprite = Equipment.equipmentInfo.icon;
            image.enabled = true;
        }
            
        UIHelper.UpdateUI();
    }
    public void RemoveAll()
    {
        equipmentActive = false;
        pirateActive = false;
        image.enabled = false;
        UIHelper.UpdateUI();
    }
    public void Back()
    {
        SceneLoader.instance.LoadSceneWithoutFade("Main");
    }
    
    public void Confirm()
    {
        SceneLoader.instance.LoadSceneWithoutFade("Trident");
    }
}
