using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using UnityEngine.Networking;
using System;

public class UIRoomManager : MonoBehaviour
{
    static public UIRoomManager share;

    public UIWRMyCardSelection myCardSelectionManager;
    public UIWRCompareManager oppoCardCompareManager;

    public Text oppoStateText;


    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public void OnEnable()
    {
        InitializeState();
    }

    public void InitializeState()
    {
        JsonObject jData = new JsonObject();
        jData.Add("type", PACKET_TYPE.PT_GET_BATTLECARD);
        jData.Add("sessionId", Engine.share.mePlayer.sessionId);
        jData.Add("address", Engine.share.mePlayer.address);

        NetworkManager.share.SendBySocket(jData.ToString());

        myCardSelectionManager.gameObject.SetActive(true);
        oppoCardCompareManager.gameObject.SetActive(false);

    }

    public void InitRandSelectionCards()
    {
        myCardSelectionManager.InitializeBattleCards();
    }

    public void NextToWaitingRoom()
    {
        Debug.Log("move to next waiting screen");

        myCardSelectionManager.gameObject.SetActive(false);
        oppoCardCompareManager.gameObject.SetActive(true);

        JsonObject jData = new JsonObject();
        jData.Add("type", PACKET_TYPE.PT_MATCH_MAKING);
        jData.Add("sessionId", Engine.share.mePlayer.sessionId);
        jData.Add("address", Engine.share.mePlayer.address);

        NetworkManager.share.SendBySocket(jData.ToString());
    }

    public void RecvMatchMakingResult(JsonObject jData)
    {
        oppoCardCompareManager.InitializeCompareRoom(jData);
    }

    public void RecvConfirmMatchMaking(JsonObject jData)
    {
        int type = Convert.ToInt32(jData["type"]);

        JsonObject firstUser = (JsonObject)jData["user1"];
        JsonObject secondUser = (JsonObject)jData["user2"];

        if(secondUser == null)
        {
            //Engine.share.oppoPlayer = new Player();
            //UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
        }
        else
        {
            if(type == 0 || type == 3 || type == 5)
            {
                Engine.share.oppoPlayer = new Player();
                UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
            }
            else
            {
                if(type == 1)
                {
                    if (firstUser != null && secondUser != null)
                    {
                        string fAdd = "", sAdd = "";
                        if (firstUser.ContainsKey("address"))
                        {
                            fAdd = Convert.ToString(firstUser["address"]);
                        }

                        if (secondUser.ContainsKey("address"))
                        {
                            sAdd = Convert.ToString(secondUser["address"]);
                        }

                        if (Engine.share.mePlayer.address == fAdd)
                        {
                            Engine.share.oppoPlayer = new Player(secondUser);
                        }
                        else
                        {
                            Engine.share.oppoPlayer = new Player(firstUser);
                        }
                    }
                    UIManager.share.OpenWindow(GAME_WINDOW.GW_GAME);
                }
                else if(type == 2)
                {
                    //Engine.share.oppoPlayer = new Player();
                    Debug.Log("wait for other user's confirmation");
                }
                else if(type == 4)
                {
                    //Engine.share.oppoPlayer = new Player();
                    Debug.Log("return to main screen");
                }
            }
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
