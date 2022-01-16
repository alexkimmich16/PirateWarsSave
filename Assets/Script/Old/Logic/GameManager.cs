using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAME_MODE
{
    GM_LOCAL,
    GM_SERVER
}

public enum GAME_BG_MODE
{
    GBM_VIDEO,
    GBM_IMAGE,
}

public class GameManager : MonoBehaviour
{
    static public GameManager share;

    public GAME_MODE gameMode;
    public GAME_BG_MODE gameBGMode;

    public string localServerURL = "";
    public string serverURL = "";
    public string metaServerURL = "";

    public int possibleCardCount;
    public int gameCardCount;

    public Sprite[] rareImages;
    public Sprite[] powerImages;
    public Sprite[] cardTypeImages;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
