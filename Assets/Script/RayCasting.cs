using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour {

    public Camera gameCamera;
    public LayerMask mask;

    // Update is called once per frame
    void Update() {

        Ray ray = gameCamera.ScreenPointToRay(Input.mousePoition);
        RaycastHit hitInfo;

        if (Physics.Raycast (ray, out hitInfo, 100, mask, QueryTriggerInteraction.Ignore)) {
            Destroy(hitInfo.collider.gameObject);

                    
            

        }
    }
}
