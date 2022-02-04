﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    //public Transform 
    public void Initialise(string Text)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Text;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,PopupManager.MoveYSpeed * Time.deltaTime,0);
        //transform.LookAt(Camera.main.transform)
    }
}
