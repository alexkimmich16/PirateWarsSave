using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    //public Transform 
    public void Initialise(string Text)
    {
        gameObject.GetComponent<TextMeshPro>().text = Text;
        gameObject.GetComponent<TextMeshPro>().fontSize = PopupManager.instance.FontSize;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,PopupManager.instance.MoveYSpeed * Time.deltaTime,0);
        transform.LookAt(Camera.main.transform);
    }
}
