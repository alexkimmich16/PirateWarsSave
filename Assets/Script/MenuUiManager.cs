using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour
{
    #region Singleton
    public static MenuUiManager instance;
    void Awake() { instance = this; }
    #endregion

    public GameObject StartMenuOBJ;
    public GameObject TridentMenuOBJ;
    public GameObject ShopMenuOBJ;
    public GameObject BattleMenuOBJ;

    public float FadeTime;

    void Start()
    {
        
    }
    public void TridentMenu(bool Active)
    {
        TridentMenuOBJ.SetActive(Active);
    }

    public void StartMenu(bool Active)
    {
        StartMenuOBJ.SetActive(Active);
    }
    public void ShopMenu(bool Active)
    {
        ShopMenuOBJ.SetActive(Active);
    }
    public void BattleMenu(bool Active)
    {
        BattleMenuOBJ.SetActive(Active);
    }
    
}
