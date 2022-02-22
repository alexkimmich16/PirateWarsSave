using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    #region Singleton
    public static PopupManager instance;
    void Awake() { instance = this; }
    #endregion

    public float MoveYSpeed = 20f;
    public float TimeDelete = 3f;
    public float FontSize;
    public GameObject BasicPopup;
    public GameObject CritPopup;

    //public Transform CameraObj;
    /*
    private void Start()
    {
        CameraObj = GameObject.Find("Main Camera").transform;
    }
    */
    //Font 
    public void CreatePopup(Vector3 Position, int Damage, bool Crit)
    {
        GameObject popup;
        if (Crit == false)
            popup = Instantiate(BasicPopup, Position, Quaternion.identity);
        else
            popup = Instantiate(CritPopup, Position, Quaternion.identity);

        if (Damage != 0)
            popup.GetComponent<DamagePopup>().Initialise(Damage.ToString());
        else
            popup.GetComponent<DamagePopup>().Initialise("Dodge!");
        Destroy(popup, TimeDelete);
    }
    
}
