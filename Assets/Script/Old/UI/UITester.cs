using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TEST_TYPE
{
    TT_CARD_GENERATION,
}

public class UITester : MonoBehaviour
{
    public GameObject testTransform;
    public GameObject cardTestPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestGame()
    {
        TEST_TYPE testType = TEST_TYPE.TT_CARD_GENERATION;
        if (testType == TEST_TYPE.TT_CARD_GENERATION)
        {
            Card ctest = new Card();
            ctest.cardImgURL = "https://storage.googleapis.com/samuraistoragetest/2-eab958a5-b2d8-423f-b81c-62cf13bef5c1.png";
            ctest.cardBgURL = "https://storage.googleapis.com/samuraibgtest/bg3r3.mp4";
            ctest.cardIdx = 2;
            ctest.power = 8;
            ctest.cardType = CARD_TYPE.CT_FIRE;

            GameObject obj = GameObject.Instantiate(cardTestPrefab) as GameObject;
            obj.GetComponent<UIGameCardObject>().InitCardInfo(ctest);

            obj.transform.parent = testTransform.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
        }
    }
}
