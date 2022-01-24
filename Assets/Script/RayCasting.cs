using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour {

    public Camera gameCamera;
    public gameObject.tag.Interactable

    // Update is called once per frame
    void Update() {

        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast (ray, out hitInfo, 100, gameObject.tag.Interactable, QueryTriggerInteraction.Ignore)) {
            Destroy(hitInfo.transform.gameObject);
            

        }
    }
}
