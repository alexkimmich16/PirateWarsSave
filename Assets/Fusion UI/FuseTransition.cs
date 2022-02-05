using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseTransition : MonoBehaviour
{
    GameObject alertPortrait;
    GameObject waitButton;
    GameObject yesButton;

    //void Start()
    //{
        
    //}



    // Update is called once per frame
    void Update()
    {
        alertPortrait.SetActive(false);
        waitButton.SetActive(false);
        yesButton.SetActive(false);
    }
}
