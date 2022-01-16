using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelStateDisplayer : MonoBehaviour
{
    bool isDisplay = false;
    float startTime = 0f;

    private void OnEnable()
    {
        isDisplay = true;
        startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisplay)
        {
            if(Time.time - startTime > 2f)
            {
                isDisplay = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
