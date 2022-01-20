using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectConverter : MonoBehaviour
{
    public Slider slider;
    public ScrollRect scroll;
    public bool Active;
    public float sliderValue;
    public float ScrollValue;

    void Update()
    {
        if(Active == true)
        {
            sliderValue = slider.value;
            //invert
            float NewValue = 1 - sliderValue;
            scroll.verticalNormalizedPosition = NewValue;
            ScrollValue = scroll.verticalNormalizedPosition;
        }
        
    }
}
