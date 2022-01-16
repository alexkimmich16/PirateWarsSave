using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using UnityEngine.Networking;
using System;

public class UIWRCompareManager : MonoBehaviour
{
    public Text myScore;
    public Text oppoScore;

    public UIGameWaitingCardObject[] myCards;
    public UIGameWaitingCardObject[] oppoCards;
    public GameObject hiddenCardObject;

    public void MakeBattleDecision(bool state)
    {
        if (state)
        {
            JsonObject jData = new JsonObject();

            jData.Add("type", PACKET_TYPE.PT_CONFIRM_MATCH);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("address", Engine.share.mePlayer.address);

            jData.Add("confirm", state);

            NetworkManager.share.SendBySocket(jData.ToString());

            //UIManager.share.OpenWindow(GAME_WINDOW.GW_GAME);
        }
        else
        {
            JsonObject jData = new JsonObject();

            jData.Add("type", PACKET_TYPE.PT_CONFIRM_MATCH);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("address", Engine.share.mePlayer.address);

            jData.Add("confirm", state);

            NetworkManager.share.SendBySocket(jData.ToString());

            UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
        }
    }

    public void InitializeCompareRoom(JsonObject jData)
    {
        Debug.Log("update match making state" + jData.ToString());

        int type = Convert.ToInt32(jData["type"]);

        if(type == 0)
        {
            for(int i = 0; i < myCards.Length; i ++)
            {
                myCards[i].gameObject.SetActive(true);
                myCards[i].InitCardInfo(Engine.share.mePlayer.battleCards[i]);
            }

            for(int i = 0; i < oppoCards.Length; i ++)
            {
                oppoCards[i].gameObject.SetActive(false);
            }

            hiddenCardObject.SetActive(false);
        }
        else if(type == 1)
        {
            JsonObject jFirstUser = (JsonObject)jData["user1"];
            JsonObject jSecondUser = (JsonObject)jData["user2"];

            if(jFirstUser != null && jSecondUser != null)
            {
                string fAdd = "", sAdd = "";
                if(jFirstUser.ContainsKey("address"))
                {
                    fAdd = Convert.ToString(jFirstUser["address"]);
                }

                if (jSecondUser.ContainsKey("address"))
                {
                    sAdd = Convert.ToString(jSecondUser["address"]);
                }

                if(Engine.share.mePlayer.address == fAdd)
                {
                    Engine.share.oppoPlayer = new Player(jSecondUser);
                }
                else
                {
                    Engine.share.oppoPlayer = new Player(jFirstUser);
                }
            }

            for(int i = 0; i < myCards.Length; i ++)
            {
                myCards[i].gameObject.SetActive(true);
                myCards[i].InitCardInfo(Engine.share.mePlayer.battleCards[i]);
            }

            for(int i = 0; i < oppoCards.Length; i ++)
            {
                oppoCards[i].gameObject.SetActive(true);
                oppoCards[i].InitCardInfo(Engine.share.oppoPlayer.battleCards[i]);
            }

            hiddenCardObject.SetActive(true);
        }

        MakeBattleDecision(true);
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
