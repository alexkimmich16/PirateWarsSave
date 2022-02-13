using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera cameraToLookAt;
     // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
         
    }
 
     // Update is called once per frame 
     void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        //transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
