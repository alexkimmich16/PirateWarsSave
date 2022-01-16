using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJson;
using NativeWebSocket;
using System.Text;

public enum PACKET_TYPE
{
    PT_LOGIN,
    PT_UPDATE_AVAILABLE_CARD_LIST,
    PT_CONFIRM_CARD,
    PT_GET_BATTLECARD,
    PT_MATCH_MAKING,
    PT_CONFIRM_MATCH,
    PT_BATTLE_CARD_SEL,
    PT_CARD_MATCH_END,
    PT_CARD_UPDATE_BATTLE_CARD,
    PT_SESSION_VALIDATION,
    PT_START_PLAY_WITH_BOT,
    PT_VALIDATE_ENERGY_PVP,
    PT_VALIDATE_ENERGY_BOT,
    PT_BOTPLAY_USER_CARD_SEL,
    PT_BOTPLAY_BOT_CARD_SEL,
    PT_CARD_BOT_UPDATE_BATTLE_CARD,
    PT_SESSION_VALIDATION_ERROR,
    PT_GAME_OVER_WITH_OPPO_EXIT,
    PT_GAME_TIME_OVER,
    PT_GAME_OVER_MESSAGE
}

public class NetworkManager : MonoBehaviour
{
    static public NetworkManager share;
    static WebSocket ws;

    bool isConnected = false;
    bool isConnecting = false;

    bool bLogin = false;
    JsonObject jLogin = new JsonObject();

    bool bBattleCard = false;
    JsonObject jBattleCard = new JsonObject();

    bool bMatchMaking = false;
    JsonObject jMatchMaking = new JsonObject();

    bool bConfirmMatch = false;
    JsonObject jConfirmMatch = new JsonObject();

    bool bBattleCardSel = false;
    JsonObject jBattleCardSel = new JsonObject();

    bool bUpdateCard = false;
    JsonObject jUpdateCard = new JsonObject();

    bool bSessionValidation = false;
    JsonObject jSessionValidataion = new JsonObject();

    bool bBotGamePlay = false;
    JsonObject jBotGamePlay = new JsonObject();

    bool bValidateEnergyPVP = false;
    JsonObject jValidateEnergyPVP = new JsonObject();

    bool bValidateEnergyBot = false;
    JsonObject jValidateEnergyBot = new JsonObject();

    bool bBotPlayUserCardSel = false;
    JsonObject jBotPlayUserCardSel = new JsonObject();

    bool bBotPlayBotCardSel = false;
    JsonObject jBotPlayBotCardSel = new JsonObject();

    bool bSessionValidationError = false;
    JsonObject jSessionValidationError = new JsonObject();

    bool bGameOverOppoExit = false;
    JsonObject jGameOverOppoExit = new JsonObject();

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public void ConnectToServer()
    {
        isConnected = false;
        isConnecting = true;

        if(GameManager.share.gameMode == GAME_MODE.GM_LOCAL)
        {
            ws = new WebSocket(GameManager.share.localServerURL);
        }
        else
        {
            ws = new WebSocket(GameManager.share.serverURL);
        }

        ws.OnMessage += OnRecv;

        ws.OnOpen += () =>
        {
            isConnected = true;
            isConnecting = false;
            Engine.share.ServerConnectionCompleted(true);

        };

        //ws.OnClose += ReconnectToServer;
        ws.OnError += OnErrorConnect;
        ws.Connect();
    }

    public void OnRecv(byte[] msg)
    {
        //Debug.Log("message from server ---" + msg);
        OnReceiveMessage(Encoding.UTF8.GetString(msg));
    }

    public void OnReceiveMessage(string str)
    {
        Debug.Log("recev from server ---" + str);
        JsonObject jRecv = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(str);
        if (jRecv.ContainsKey("type"))
        {
            PACKET_TYPE pType = (PACKET_TYPE)Convert.ToInt32(jRecv["type"]);

            switch(pType)
            {
                case PACKET_TYPE.PT_LOGIN:
                    bLogin = true;
                    jLogin = jRecv;
                    break;
                case PACKET_TYPE.PT_GET_BATTLECARD:
                    bBattleCard = true;
                    jBattleCard = jRecv;
                    break;
                case PACKET_TYPE.PT_MATCH_MAKING:
                    bMatchMaking = true;
                    jMatchMaking = jRecv;
                    break;
                case PACKET_TYPE.PT_CONFIRM_MATCH:
                    bConfirmMatch = true;
                    jConfirmMatch = jRecv;
                    break;
                case PACKET_TYPE.PT_BATTLE_CARD_SEL:
                    bBattleCardSel = true;
                    jBattleCardSel = jRecv;
                    break;
                case PACKET_TYPE.PT_CARD_UPDATE_BATTLE_CARD:
                    bUpdateCard = true;
                    jUpdateCard = jRecv;
                    break;
                case PACKET_TYPE.PT_SESSION_VALIDATION:
                    bSessionValidation = true;
                    jSessionValidataion = jRecv;
                    break;
                case PACKET_TYPE.PT_START_PLAY_WITH_BOT:
                    bBotGamePlay = true;
                    jBotGamePlay = jRecv;
                    break;
                case PACKET_TYPE.PT_VALIDATE_ENERGY_PVP:
                    bValidateEnergyPVP = true;
                    jValidateEnergyPVP = jRecv;
                    break;
                case PACKET_TYPE.PT_VALIDATE_ENERGY_BOT:
                    bValidateEnergyBot = true;
                    jValidateEnergyBot = jRecv;
                    break;
                case PACKET_TYPE.PT_BOTPLAY_USER_CARD_SEL:
                    bBotPlayUserCardSel = true;
                    jBotPlayUserCardSel = jRecv;
                    break;
                case PACKET_TYPE.PT_BOTPLAY_BOT_CARD_SEL:
                    bBotPlayBotCardSel = true;
                    jBotPlayBotCardSel = jRecv;
                    break;
                case PACKET_TYPE.PT_SESSION_VALIDATION_ERROR:
                    bSessionValidationError = true;
                    jSessionValidationError = jRecv;
                    break;
                case PACKET_TYPE.PT_GAME_OVER_WITH_OPPO_EXIT:
                    bGameOverOppoExit = true;
                    jGameOverOppoExit = jRecv;
                    break;
                default:
                    break;
            }
        }
    }

    public void ReconnectToServer(WebSocketCloseCode closeCode)
    {
        //Debug.Log("connection close called" + "---" + closeCode);
        ConnectToServer();
    }

    public void OnErrorConnect(string evt)
    {
        //Debug.Log("closed error --- " + evt);
    }

    public void SendBySocket(string msg)
    {
        Debug.Log("senddata---" + msg);
        //ws.Send(Encoding.UTF8.GetBytes(msg));
        ws.SendText(msg);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (ws != null)
        {
            ws.DispatchMessageQueue();
        }
#endif
        //if (isConnecting && ws.GetState() == )
        //{
        //    return;
        //}

        //if (isConnecting && ws.GetState() == WebSocketState.Open)
        //{
        //    isConnected = true;
        //    isConnecting = false;
        //    Engine.share.ServerConnectionCompleted(true);
        //    return;
        //}

        //if (isConnected && ws.GetState() == WebSocketState.Closed)
        //{
        //    ConnectToServer();
        //}
        if (isConnected)
        {
            if(bLogin)
            {
                Engine.share.RecvLoginResult(jLogin);
                bLogin = false;
                jLogin = new JsonObject();
            }
            if(bBattleCard)
            {
                Engine.share.RecvBattleCardDecided(jBattleCard);
                bBattleCard = false;
                jBattleCard = new JsonObject();
            }

            if(bMatchMaking)
            {
                Engine.share.RecvMatchMaking(jMatchMaking);
                bMatchMaking = false;
                jMatchMaking = new JsonObject();
            }

            if(bConfirmMatch)
            {
                Engine.share.RecvConfirmMatch(jConfirmMatch);
                bConfirmMatch = false;
                jConfirmMatch = new JsonObject();
            }

            if(bBattleCardSel)
            {
                Engine.share.RecvBattleCardSel(jBattleCardSel);
                bBattleCardSel = false;
                jBattleCardSel = new JsonObject();
            }

            if(bUpdateCard)
            {
                Engine.share.RecvUpdateBattleCard(jUpdateCard);
                bUpdateCard = false;
                jUpdateCard = new JsonObject();
            }

            if(bSessionValidation)
            {
                Engine.share.RecvSessionValidataion(jSessionValidataion);
                bSessionValidation = false;
                jSessionValidataion = new JsonObject();
            }

            if(bBotGamePlay)
            {
                Engine.share.RecvGetBotPlayData(jBotGamePlay);
                bBotGamePlay = false;
                jBotGamePlay = new JsonObject();
            }

            if(bValidateEnergyPVP)
            {
                Engine.share.RecvValidateEnergyPVP(jValidateEnergyPVP);
                bValidateEnergyPVP = false;
                jValidateEnergyPVP = new JsonObject();
            }

            if(bValidateEnergyBot)
            {
                Engine.share.RecvValidateEnergyBot(jValidateEnergyBot);
                bValidateEnergyBot = false;
                jValidateEnergyBot = new JsonObject();
            }

            if(bBotPlayUserCardSel)
            {
                Engine.share.RecvBotPlayCardSelResult(jBotPlayUserCardSel);
                bBotPlayUserCardSel = false;
                jBotPlayUserCardSel = new JsonObject();
            }

            if(bBotPlayBotCardSel)
            {
                Engine.share.RecvBotPlayCardMatchEnd(jBotPlayBotCardSel);
                bBotPlayBotCardSel = false;
                jBotPlayBotCardSel = new JsonObject();
            }

            if (bSessionValidationError)
            {
                Engine.share.RecvSessionValidationError(jSessionValidationError);
                bSessionValidationError = false;
                jSessionValidationError = new JsonObject();
            }

            if(bGameOverOppoExit)
            {
                Engine.share.RecvGameOverOppoExit(jGameOverOppoExit);
                bGameOverOppoExit = false;
                jGameOverOppoExit = new JsonObject();
            }
        }
    }
}
