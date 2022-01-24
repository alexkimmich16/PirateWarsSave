using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour {

    public Camera gameCamera;

    // Update is called once per frame
    void Update() {

        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;


        //Debug.Log("number1");
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            //Debug.Log("number2");
            //Debug.Log(hitInfo.transform.gameObject.tag);
            if (hitInfo.transform.gameObject.tag == "Interactable")
            {
               // Debug.Log("number3");


                Destroy(hitInfo.transform.gameObject);
            }
        }
    }
}
