using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public void Initialise(string Text)
    {
        gameObject.GetComponent<TextMeshPro>().text = Text;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,PopupManager.MoveYSpeed * Time.deltaTime,0);
    }
}
