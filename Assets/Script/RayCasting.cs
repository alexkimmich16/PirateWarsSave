using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour {

    public Camera gameCamera;

    // Update is called once per frame
    void Update() {

        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.GetComponent<InteractableType>())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CameraMovement.instance.MoveTo(hitInfo.transform.gameObject.GetComponent<InteractableType>().type);
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            CameraMovement.instance.MoveTo(Interactable.Main);
        }
    }
}
