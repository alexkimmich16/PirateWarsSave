using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICustomGameButton : MonoBehaviour
{
    public void ButtonHOver()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_MOUSE_HOVER);
    }

    public void ButtonClick()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_MOUSE_CLICK);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
