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

    public static float MoveYSpeed = 20f;
    public static float TimeDelete = 3f;
    public GameObject BasicPopup;
    public GameObject CritPopup;

    public Transform CameraObj;
    private void Start()
    {
        CameraObj = GameObject.Find("Main Camera").transform;
    }
    //Font 
    public void CreatePopup(Vector3 Position, int Damage, bool Crit)
    {
        GameObject popup;
        if (Crit == false)
        {
            popup = Instantiate(BasicPopup, Position, Quaternion.identity);
        }
        else
        {
            popup = Instantiate(CritPopup, Position, Quaternion.identity);
        }

        if (CameraObj != null)
        {
            Debug.Log("doen't exist");
        }
        popup.GetComponent<DamagePopup>().Initialise(Damage.ToString());
        popup.transform.parent = CameraObj;
        Destroy(popup, TimeDelete);
    }
    
}
