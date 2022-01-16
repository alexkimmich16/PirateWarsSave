using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJson;
using UnityEngine.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class Engine : MonoBehaviour
{
    static public Engine share;

    public Player mePlayer;
    public Player oppoPlayer;

    public Player botPlayer;

    //public my info

    public string sessionId;
    public string userAddress;

    public List<Card> selectedCardList;
    public List<Card> battleCardList;

    public List<Card> botCardsList;

    //public oppo info

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
        OnGameStart();
    }

    public void OnGameStart()
    {
        JsonObject jdata = URLPharser.share.GetURLParams();

        if(jdata == null)
        {
            Debug.Log("---url param error ----");
        }
        else
        {
            if(jdata.ContainsKey("address"))
            {
                userAddress = Convert.ToString(jdata["address"]);
            }

            if (jdata.ContainsKey("sessionId"))
            {
                sessionId = Convert.ToString(jdata["sessionId"]);
            }

            mePlayer = new Player(sessionId, userAddress);

            NetworkManager.share.ConnectToServer();

            UIEnergyBar.share.InitializeEnergy();
        }
    }

    public void AddressValidationCheck()
    {
        StartCoroutine(IEAddressValidationCheck());
    }

    public IEnumerator IEAddressValidationCheck()
    {
        string addUrl = GameManager.share.metaServerURL + "/session?" + "address=" + mePlayer.address + "&sessionId=" + mePlayer.sessionId;

        UnityWebRequest uri = UnityWebRequest.Get(addUrl);

        yield return uri.SendWebRequest();
        if (uri.isNetworkError)
        {

        }
        else
        {
            string downloadData = uri.downloadHandler.text;
            JsonObject jData = new JsonObject();
            jData.Add("type", PACKET_TYPE.PT_LOGIN);
            jData.Add("sessionId", mePlayer.sessionId);
            jData.Add("address", mePlayer.address);

            NetworkManager.share.SendBySocket(jData.ToString());
        }
    }

    public void ServerConnectionCompleted(bool state)
    {
        if(state)
        {
            //server connection success
            AddressValidationCheck();
        }
        else
        {
            //server connection failed
        }
    }

    public bool CheckIfReadyForPlay()
    {
        if(selectedCardList != null && selectedCardList.Count == 5)
        {
            return true;
        }
        return false;
    }

    public void RecvBattleCardDecided(JsonObject jData)
    {
        Debug.Log("battle card received event : " + jData.ToString());
        JsonArray jArr = (JsonArray)jData["data"];

        battleCardList = new List<Card>();

        for(int i = 0; i < jArr.Count; i ++)
        {
            battleCardList.Add(selectedCardList[Convert.ToInt32(jArr[i])]);
        }

        Engine.share.mePlayer.SetBattleCards(battleCardList);

        UIRoomManager.share.InitRandSelectionCards();
    }

    public void RecvMatchMaking(JsonObject jData)
    {
        JsonObject jMatch = (JsonObject)jData["data"];
        int type = Convert.ToInt32(jMatch["type"]);
        UIRoomManager.share.RecvMatchMakingResult(jMatch);
    }

    public void RecvConfirmMatch(JsonObject jData)
    {
        JsonObject jConfirmMatch = (JsonObject)jData["data"];
        UIRoomManager.share.RecvConfirmMatchMaking(jConfirmMatch);
    }

    public void RecvBattleCardSel(JsonObject jData)
    {
        JsonObject jBattleCardSel = (JsonObject)jData["data"];
        UIGameController.share.RecvGameCardSelectionState(jBattleCardSel);
    }

    public void RecvUpdateBattleCard(JsonObject jData)
    {
        JsonObject jUpdateCard = (JsonObject)jData["data"];
        UIGameController.share.RecvOppoCardUpdate(jUpdateCard);
    }

    public void RecvSessionValidataion(JsonObject jData)
    {
        bool state = Convert.ToBoolean(jData["data"]);
        UIGameStartScreen.share.SessionValidataionError(state);
    }

    public void InitializeBotPlayer()
    {        
        botPlayer = new Player(mePlayer);
        botPlayer.address = "0xbotPlayer" + mePlayer.address;
        botPlayer.sessionId = "botsession" + mePlayer.sessionId;

        botPlayer.UpdateBotCardList(botCardsList);

        mePlayer.ActiveAllCards();
        botPlayer.ActiveAllCards();

        JsonObject jData = new JsonObject();
        jData.Add("type", PACKET_TYPE.PT_START_PLAY_WITH_BOT);
        jData.Add("sessionId", mePlayer.sessionId);
        jData.Add("address", mePlayer.address);
        jData.Add("mePlayer", mePlayer.GetJson());
        jData.Add("botPlayer", botPlayer.GetJson());

        NetworkManager.share.SendBySocket(jData.ToString());

        //botPlayer.SetBotPlayerInfo();
    }

    public void RecvGetBotPlayData(JsonObject jData)
    {
        JsonObject jRecv = (JsonObject)jData["data"];

        JsonArray jMeData = (JsonArray)jRecv["meData"];
        JsonObject jBotData = (JsonObject)jRecv["botData"];

        Debug.Log("user data ---" + jMeData.ToString());
        JsonArray jSelectedCards = (JsonArray)jBotData["battleCards"];
        JsonArray jActiveBattleCards = (JsonArray)jBotData["activeBattleCards"];

        List<int> cardData = new List<int>();
        for(int i = 0; i < jMeData.Count; i ++)
        {
            int idx = Convert.ToInt32(jMeData[i]);
            cardData.Add(idx);
        }

        botPlayer.UpdateUpdateBattleData(jSelectedCards, jActiveBattleCards);
        mePlayer.SetAvailableCards(cardData);
        UIManager.share.RecvBotPlay();
    }

    public void RecvValidateEnergyPVP(JsonObject jData)
    {
        JsonObject recv = (JsonObject)jData["data"];
        bool state = Convert.ToBoolean(recv["state"]);

        if (state)
        {
            UIManager.share.OpenWindow(GAME_WINDOW.GW_WAITING_ROOM);
        }
        else
        {
            UIManager.share.ShowError("you don't have enough energy!");
        }
    }

    public void RecvValidateEnergyBot(JsonObject jData)
    {
        JsonObject recv = (JsonObject)jData["data"];
        bool state = Convert.ToBoolean(recv["state"]);

        if (state)
        {
            UIManager.share.OpenWindow(GAME_WINDOW.GW_GAME_BOT);
        }
        else
        {
            UIManager.share.ShowError("you don't have enough energy!");
        }
    }

    public void RecvBotPlayCardSelResult(JsonObject jData)
    {
        JsonObject data = (JsonObject)jData["data"];
        bool state = Convert.ToBoolean(data["state"]);

        if (state)
        {
            UIBotGameController.share.AutoSelectBotCard();
        }
    }

    public void RecvBotPlayCardMatchEnd(JsonObject jData)
    {
        JsonObject data = (JsonObject)jData["data"];
        UIBotGameController.share.RecvGameCardMatchEnd(data);
    }

    public void RecvSessionValidationError(JsonObject jData)
    {
        UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
        StartCoroutine(ShowErrorValidationError());
    }

    public IEnumerator ShowErrorValidationError()
    {
        UIManager.share.ShowError("This session expired or other user connected with this session!");
        yield return new WaitForSeconds(2.2f);
        UIManager.share.ShowError("This session expired or other user connected with this session!");
        yield return new WaitForSeconds(2.2f);
        UIManager.share.ShowError("This session expired or other user connected with this session!");
    }

    public void RecvGameOverOppoExit(JsonObject jData)
    {

    }

    public void RecvLoginResult(JsonObject jData)
    {
        JsonObject data = (JsonObject)jData["data"];
        JsonArray botIds = (JsonArray)data["botCardIds"];

        botCardsList = new List<Card>();

        for(int i = 0; i < botIds.Count; i ++)
        {
            int cIdx = Convert.ToInt32(botIds[i]);
            StartCoroutine(IEGetCardDetails(cIdx));
        }
    }

    public IEnumerator IEGetCardDetails(int cardIdx)
    {
        string deckURL = GameManager.share.metaServerURL + "/samurai/" + cardIdx.ToString();
        UnityWebRequest uri = UnityWebRequest.Get(deckURL);

        yield return uri.SendWebRequest();
        if (uri.isNetworkError)
        {

        }
        else
        {
            string cardData = uri.downloadHandler.text;
            JsonObject jData = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(cardData);

            if (jData != null)
            {
                Card cData = new Card(jData);
                botCardsList.Add(cData);
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
