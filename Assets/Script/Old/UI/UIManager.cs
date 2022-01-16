using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;

public enum GAME_WINDOW
{
    GW_START,
    GW_DECK_SEL,
    GW_WAITING_ROOM,
    GW_GAME,
    GW_GAME_BOT
}

public class UIManager : MonoBehaviour
{
    static public UIManager share;

    public GAME_WINDOW windowState = GAME_WINDOW.GW_START;

    public GameObject startScreen;
    public GameObject deckSelection;
    public GameObject waitingRoom;
    public GameObject gameScreen;
    public GameObject gameBotScreen;
    public GameObject errorWindow;

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

    public void OnInitializeGame()
    {
        OpenWindow(GAME_WINDOW.GW_START);
    }

    public void OpenWindow(GAME_WINDOW windowType)
    {
        windowState = windowType;

        startScreen.SetActive(false);
        deckSelection.SetActive(false);
        waitingRoom.SetActive(false);
        gameScreen.SetActive(false);
        gameBotScreen.SetActive(false);

        switch(windowState)
        {
            case GAME_WINDOW.GW_START:
                startScreen.SetActive(true);
                break;
            case GAME_WINDOW.GW_DECK_SEL:
                deckSelection.SetActive(true);
                break;
            case GAME_WINDOW.GW_WAITING_ROOM:
                waitingRoom.SetActive(true);
                break;
            case GAME_WINDOW.GW_GAME:
                gameScreen.SetActive(true);
                break;
            case GAME_WINDOW.GW_GAME_BOT:
                PlayBotGame();
                break;
            default:
                startScreen.SetActive(true);
                break;
        }
    }

    public void PlayBotGame()
    {
        Engine.share.InitializeBotPlayer();
        //gameBotScreen.SetActive(true);
    }

    public void RecvBotPlay()
    {
        gameBotScreen.SetActive(true);
    }

    public void ShowError(string text)
    {
        errorWindow.SetActive(true);
        UIErrorWindow.share.ShowError(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
