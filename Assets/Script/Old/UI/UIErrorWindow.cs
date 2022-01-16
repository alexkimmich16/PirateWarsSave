using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIErrorWindow : MonoBehaviour
{
    static public UIErrorWindow share;

    private void Awake()
    {
        share = this;
    }

    public Text errorText;

    bool isShow = false;
    float startTime = 0;

    private void OnEnable()
    {
        isShow = true;
        startTime = Time.time;
    }

    public void ShowError(string errorNote)
    {
        errorText.text = errorNote;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {
            if(Time.time - startTime > 2)
            {
                isShow = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
