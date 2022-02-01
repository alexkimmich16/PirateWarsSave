using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    #region Singleton
    public static TransitionManager instance;
    void Awake() { instance = this; }
    #endregion


    public Animator transitionMenu;
    public void StartTransition()
    {

    }
    void Start()
    {
        //transitionMenu = gameObject.transform.Find("crossfade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
