using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static float MoveYSpeed = 20f;
    public GameObject BasicPopup;
    public GameObject CritPopup;

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
        popup.GetComponent<DamagePopup>().Initialise(Damage.ToString());
    }
    
}
