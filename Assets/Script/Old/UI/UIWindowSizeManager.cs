using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIWindowSizeManager : MonoBehaviour
{
    static public UIWindowSizeManager share;

    public int designedHeight;
    public int designedWidth;

    public int curHeight;
    public int curWidth;

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCanvas();
    }

    private void Update()
    {
        if (Screen.width != curWidth || Screen.height != curHeight)
        {
            UpdateCanvas();
        }
        //if (Screen.currentResolution.width != curWidth || Screen.currentResolution.height != curHeight)
        //{
        //    UpdateCanvas();
        //} 
    }

    //void FixedUpdate()
    //{
    //    //if(Screen.currentResolution.width != curWidth || Screen.currentResolution.height != curHeight)
    //    //{
    //    //    UpdateCanvas();
    //    //}
    //}

    public void UpdateCanvas()
    {
        curHeight = Screen.height;
        curWidth = Screen.width;

        //Debug.Log("cur width ---" + curWidth + "----" + curHeight + "----" + Screen.width + "----" + Screen.height);

        float curRatio = (float)curHeight / curWidth;

        if (curRatio > (float)9 / 16)
        {
            this.gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
        else
        {
            this.gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
    }
}
