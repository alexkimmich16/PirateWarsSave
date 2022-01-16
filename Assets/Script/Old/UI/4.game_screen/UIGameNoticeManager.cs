using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIGameNoticeManager : MonoBehaviour
{
    public Text noticeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowNoticeAnim(string strText)
    {
        noticeText.text = strText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
