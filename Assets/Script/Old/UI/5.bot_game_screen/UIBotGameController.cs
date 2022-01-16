using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJson;
using UnityEngine.UI;


public class UIBotGameController : MonoBehaviour
{
    static public UIBotGameController share;

    public UIGSBotMyGameCardManager myGameCards;
    public UIGSBotOppoGameCardManager oppoGameCards;

    public UIGSBotMyGameInfo myInfo;
    public UIGSBotOppoGameInfo oppoInfo;

    public UIGameNoticeManager gameNotice;

    public GameObject gameStateNode;
    public Text stateText;

    public GameObject pick_buttom, pick_top, battle_a;

    bool isWaitingResult = false;

    public int gameRound = 1;

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public void OnEnable()
    {
        SoundManager.share.SetBackGroundSound(BG_SOUND.BGS_BATTLE);
        StartCoroutine(InitializeGame());
    }

    public IEnumerator InitializeGame()
    {
        isWaitingResult = false;
        myGameCards.gameObject.SetActive(false);
        oppoGameCards.gameObject.SetActive(false);

        myInfo.SetCardActiveState(false);
        oppoInfo.SetCardActiveState(false);

        StartCoroutine(ShowGameNotice("prepare for battle!"));
        yield return new WaitForSeconds(2.0f);

        isWaitingResult = false;
        myGameCards.gameObject.SetActive(true);
        oppoGameCards.gameObject.SetActive(true);
        myGameCards.InitGameCards();
        oppoGameCards.InitGameCards();

        myInfo.SetCardActiveState(false);
        oppoInfo.SetCardActiveState(false);
    }

    public IEnumerator ShowGameNotice(string text)
    {
        gameNotice.gameObject.SetActive(true);
        gameNotice.ShowNoticeAnim(text);
        yield return new WaitForSeconds(2f);
        gameNotice.gameObject.SetActive(false);
    }

    public void MyCardSelected(Card cData)
    {
        if(Engine.share.botPlayer.GetAvailableCardCount() > 0)
        {
            JsonObject jData = new JsonObject();
            jData.Add("type", PACKET_TYPE.PT_BOTPLAY_USER_CARD_SEL);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("address", Engine.share.mePlayer.address);
            jData.Add("cardIdx", cData.cardIdx);

            //Engine.share.mePlayer.SetBattleCardState(cData.cardIdx, false);

            Engine.share.mePlayer.SetBattleCardIdx(cData.cardIdx);

            NetworkManager.share.SendBySocket(jData.ToString());

            isWaitingResult = true;
            //SetGameState(true);
            myInfo.InitGameInfo(Engine.share.mePlayer.GetBattleCard(cData.cardIdx));
            myGameCards.UpdateCardSelectionState();

            StartCoroutine(IEMyCardSelection());

            //AutoSelectBotCard();
        }
    }

    public void AutoSelectBotCard()
    {
        StartCoroutine(IEAutoSelectBotCard());
    }

    public void GetRandNumberAvailable()
    {

    }

    public IEnumerator IEAutoSelectBotCard()
    {
        float rTime = UnityEngine.Random.RandomRange(0.3f, 1.0f);
        yield return new WaitForSeconds(rTime);

        int rNumber = UnityEngine.Random.RandomRange(0, 3);
        Card bCard = Engine.share.botPlayer.battleCards[rNumber];

        if (Engine.share.botPlayer.GetAvailableCardCount() < 3)
        {
            if(bCard.isUsable == false)
            {
                for(int i = 0; i < Engine.share.botPlayer.battleCards.Count; i ++)
                {
                    if(Engine.share.botPlayer.battleCards[i].isUsable == true)
                    {
                        bCard = Engine.share.botPlayer.battleCards[i];
                        break;
                    }
                }
            }
        }

        Engine.share.botPlayer.SetBattleCardIdx(bCard.cardIdx);

        oppoInfo.InitGameInfo(Engine.share.botPlayer.GetBattleCard(bCard.cardIdx));
        oppoGameCards.UpdateCardSelectionState();
        StartCoroutine(IEOppoCardSelection());
        Debug.Log("bot battle card idx ---" + bCard.GetCardJson());

        yield return new WaitForSeconds(2.0f);

        JsonObject jData = new JsonObject();
        jData.Add("type", PACKET_TYPE.PT_BOTPLAY_BOT_CARD_SEL);
        jData.Add("sessionId", Engine.share.mePlayer.sessionId);
        jData.Add("address", Engine.share.mePlayer.address);
        jData.Add("cardIdx", bCard.cardIdx);
        NetworkManager.share.SendBySocket(jData.ToString());
    }

    public void RecvGameCardMatchEnd(JsonObject jRecv)
    {
        JsonObject jResult = (JsonObject)jRecv["battleResult"];
        int battleResult = Convert.ToInt32(jResult["type"]);

        myInfo.ShowBattleAnim();
        oppoInfo.ShowBattleAnim();
        StartCoroutine(IEShowBattleOperation());

        if (battleResult == 1)
        {
            int cIdx = Engine.share.botPlayer.curBattleCard;
            Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, true);
            Engine.share.botPlayer.SetBattleCardState(Engine.share.botPlayer.curBattleCard, false);
            oppoInfo.ShowGameLoseState();
            SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_WIN);
            BotCardUpdateForLostCard(cIdx);
            StartCoroutine(ShowGameNotice("You win the round"));
        }
        else if(battleResult == -1)
        {
            int cIdx = Engine.share.mePlayer.curBattleCard;
            Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, false);
            Engine.share.botPlayer.SetBattleCardState(Engine.share.botPlayer.curBattleCard, true);
            myInfo.ShowGameLoseState();
            SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_LOSE);
            CardUpdateForLosedCard(cIdx);
            StartCoroutine(ShowGameNotice("You lose the round"));
        }

        StartCoroutine(FinalizeBotPlay());
    }

    public IEnumerator FinalizeBotPlay()
    {
        yield return new WaitForSeconds(1.0f);

        myInfo.DisableCard();
        oppoInfo.DisableCard();

        Debug.Log("wait till show win state");

        myGameCards.UpdateCardSelectionState();
        oppoGameCards.UpdateCardSelectionState();
        Debug.Log("game match end ---my game---" + Engine.share.mePlayer.GetAvailableCardCount() + "----oppo count ---" + Engine.share.botPlayer.GetAvailableCardCount());
        if (Engine.share.mePlayer.GetAvailableCardCount() <= 0 || Engine.share.botPlayer.GetAvailableCardCount() <= 0)
        {
            Debug.Log("game ended ---my game---" + Engine.share.mePlayer.GetAvailableCardCount() + "----oppo count ---" + Engine.share.botPlayer.GetAvailableCardCount());
            StartCoroutine(ShowGameEndAnimation());
        }
    }

    public IEnumerator IEMyCardSelection()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_CARD_PICK);
        pick_buttom.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        pick_buttom.SetActive(false);
    }

    public IEnumerator IEOppoCardSelection()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_CARD_PICK);
        pick_top.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        pick_top.SetActive(false);
    }

    public IEnumerator IEShowBattleOperation()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE);
        battle_a.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        battle_a.SetActive(false);
    }

    public void RecvGameCardSelectionState(JsonObject jData)
    {
        Debug.Log("------card sel state detail ---" + jData.ToString());
        int type;
        JsonObject jUser1 = new JsonObject();
        JsonObject jUser2 = new JsonObject();
        JsonObject battleResult = new JsonObject();
        jUser1 = null;
        jUser2 = null;
        battleResult = null;

        if (jData.ContainsKey("type"))
        {
            type = Convert.ToInt32(jData["type"]);
        }

        if (jData.ContainsKey("user1"))
        {
            jUser1 = (JsonObject)jData["user1"];
        }

        if (jData.ContainsKey("user2"))
        {
            jUser2 = (JsonObject)jData["user2"];
        }

        if (jData.ContainsKey("battleResult"))
        {
            battleResult = (JsonObject)jData["battleResult"];
        }

        if (jUser1 != null && jUser2 != null && battleResult != null)
        {
            StartCoroutine(IEShowGameBattleState(battleResult, jUser1, jUser2));
        }

        //myGameCards.UpdateCardStates();
        //oppoGameCards.UpdateCardStates();
    }

    public IEnumerator IEShowGameBattleState(JsonObject battleResult, JsonObject jUser1, JsonObject jUser2)
    {
        string fAdd = "";
        string sAdd = "";
        int wType = 0;

        if (battleResult.ContainsKey("fUserAdd"))
        {
            fAdd = Convert.ToString(battleResult["fUserAdd"]);
        }

        if (battleResult.ContainsKey("sUserAdd"))
        {
            sAdd = Convert.ToString(battleResult["sUserAdd"]);
        }

        if (battleResult.ContainsKey("type"))
        {
            wType = Convert.ToInt32(battleResult["type"]);
        }

        Debug.Log("-----1------");

        if (Engine.share.mePlayer.address == fAdd)
        {
            Engine.share.mePlayer.UpdateUserBattleCardInfo(jUser1);
            Engine.share.oppoPlayer.UpdateUserBattleCardInfo(jUser2);
            oppoInfo.InitGameInfo(Engine.share.oppoPlayer.GetBattleCard(Engine.share.oppoPlayer.curBattleCard));
            oppoGameCards.UpdateCardSelectionState();
            StartCoroutine(IEOppoCardSelection());
            yield return new WaitForSeconds(1.2f);
            myInfo.ShowBattleAnim();
            oppoInfo.ShowBattleAnim();
            StartCoroutine(IEShowBattleOperation());
            if (wType == 0)
            {

            }
            else if (wType == 1)
            {
                Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, true);
                Engine.share.oppoPlayer.SetBattleCardState(Engine.share.oppoPlayer.curBattleCard, false);
                oppoInfo.ShowGameLoseState();
                SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_WIN);
                StartCoroutine(ShowGameNotice("You win the round"));

                //List<string> gameMessage = new List<string>();
                //gameMessage.Add("Your Card Win The Game!");
                //gameMessage.Add("You Can Use Your Card Again!");
                //gameMessage.Add("Please Select Your Card Again!");
                //StartCoroutine(ShowGameStateMessages(gameMessage));
            }
            else if (wType == -1)
            {
                int cIdx = Engine.share.mePlayer.curBattleCard;
                Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, false);
                Engine.share.oppoPlayer.SetBattleCardState(Engine.share.oppoPlayer.curBattleCard, true);
                myInfo.ShowGameLoseState();
                SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_LOSE);
                CardUpdateForLosedCard(cIdx);
                StartCoroutine(ShowGameNotice("You lose the round"));
                //List<string> gameMessage = new List<string>();
                //gameMessage.Add("Your Card Lose The Game!");
                //gameMessage.Add("You Can't Use Your Card Again!");
                //gameMessage.Add("Please Select Your Another Card!");
                //StartCoroutine(ShowGameStateMessages(gameMessage));
            }
        }
        else
        {
            Engine.share.mePlayer.UpdateUserBattleCardInfo(jUser2);
            Engine.share.oppoPlayer.UpdateUserBattleCardInfo(jUser1);
            oppoInfo.InitGameInfo(Engine.share.oppoPlayer.GetBattleCard(Engine.share.oppoPlayer.curBattleCard));
            oppoGameCards.UpdateCardSelectionState();
            StartCoroutine(IEOppoCardSelection());
            yield return new WaitForSeconds(1.2f);
            myInfo.ShowBattleAnim();
            oppoInfo.ShowBattleAnim();
            StartCoroutine(IEShowBattleOperation());

            if (wType == 0)
            {

            }
            else if (wType == 1)
            {
                int cIdx = Engine.share.mePlayer.curBattleCard;
                Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, false);
                Engine.share.oppoPlayer.SetBattleCardState(Engine.share.oppoPlayer.curBattleCard, true);
                myInfo.ShowGameLoseState();
                SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_LOSE);
                CardUpdateForLosedCard(cIdx);
                StartCoroutine(ShowGameNotice("You lose the round"));

                //List<string> gameMessage = new List<string>();
                //gameMessage.Add("Your Card Lose The Game!");
                //gameMessage.Add("You Can't Use Your Card Again!");
                //gameMessage.Add("Please Select Your Another Card!");
                //StartCoroutine(ShowGameStateMessages(gameMessage));
            }
            else if (wType == -1)
            {
                Engine.share.mePlayer.SetBattleCardState(Engine.share.mePlayer.curBattleCard, true);
                Engine.share.oppoPlayer.SetBattleCardState(Engine.share.oppoPlayer.curBattleCard, false);
                oppoInfo.ShowGameLoseState();
                SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_ROUND_WIN);
                StartCoroutine(ShowGameNotice("You win the round"));

                //List<string> gameMessage = new List<string>();
                //gameMessage.Add("Your Card Win The Game!");
                //gameMessage.Add("You Can Use Your Card Again!");
                //gameMessage.Add("Please Select Your Card Again!");
                //StartCoroutine(ShowGameStateMessages(gameMessage));
            }
        }

        SendCardCompareEnd();

        Debug.Log(":------------!!!!-------------");

        yield return new WaitForSeconds(2.0f);

        myInfo.DisableCard();
        oppoInfo.DisableCard();

        Debug.Log("wait till show win state");

        myGameCards.UpdateCardSelectionState();
        oppoGameCards.UpdateCardSelectionState();

        if (Engine.share.mePlayer.GetAvailableCardCount() <= 0 || Engine.share.oppoPlayer.GetAvailableCardCount() <= 0)
        {
            Debug.Log("game ended ---my game---" + Engine.share.mePlayer.GetAvailableCardCount() + "----oppo count ---" + Engine.share.oppoPlayer.GetAvailableCardCount());
            StartCoroutine(ShowGameEndAnimation());
        }
    }

    public void BotCardUpdateForLostCard(int curBattleCard)
    {
        int updatedCardIdx = Engine.share.botPlayer.CardUpdateForLosedCard(curBattleCard);
        if (updatedCardIdx < 0)
        {

        }
        else
        {
            JsonObject jobj = new JsonObject();
            jobj.Add("type", PACKET_TYPE.PT_CARD_BOT_UPDATE_BATTLE_CARD);
            jobj.Add("curCardId", curBattleCard);
            jobj.Add("newCardId", updatedCardIdx);
            jobj.Add("sessionId", Engine.share.mePlayer.sessionId);
            jobj.Add("address", Engine.share.mePlayer.address);

            NetworkManager.share.SendBySocket(jobj.ToString());

            oppoGameCards.UpdateBotGameCard(curBattleCard);

        }
    }

    public void CardUpdateForLosedCard(int curBattleCard)
    {
        Debug.Log("----- battle card ----" + curBattleCard);
        int updatedCardIdx = Engine.share.mePlayer.CardUpdateForLosedCard(curBattleCard);
        if (updatedCardIdx < 0)
        {

        }
        else
        {
            JsonObject jobj = new JsonObject();
            jobj.Add("type", PACKET_TYPE.PT_CARD_UPDATE_BATTLE_CARD);
            jobj.Add("curCardId", curBattleCard);
            jobj.Add("newCardId", updatedCardIdx);
            jobj.Add("sessionId", Engine.share.mePlayer.sessionId);
            jobj.Add("address", Engine.share.mePlayer.address);

            NetworkManager.share.SendBySocket(jobj.ToString());

            myGameCards.UpdateMyGameCard(curBattleCard);

        }
    }

    public IEnumerator ShowGameEndAnimation()
    {
        yield return new WaitForSeconds(2.0f);

        myGameCards.gameObject.SetActive(false);
        oppoGameCards.gameObject.SetActive(false);
        myInfo.DisableCard();
        oppoInfo.DisableCard();

        if (Engine.share.mePlayer.GetAvailableCardCount() <= 0)
        {
            SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATLLE_LOSE);

            JsonObject jData = new JsonObject();
            jData.Add("address", Engine.share.mePlayer.address);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("type", PACKET_TYPE.PT_GAME_OVER_MESSAGE);
            jData.Add("userData", Engine.share.mePlayer.GetJson());
            jData.Add("oppoData", Engine.share.botPlayer.GetJson());
            jData.Add("gameType", "BOT");
            jData.Add("gameResult", -1);

            NetworkManager.share.SendBySocket(jData.ToString());

            StartCoroutine(ShowGameNotice("you lose the game"));
        }
        else
        {
            SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_BATTLE_WIN);

            JsonObject jData = new JsonObject();
            jData.Add("address", Engine.share.mePlayer.address);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("type", PACKET_TYPE.PT_GAME_OVER_MESSAGE);
            jData.Add("userData", Engine.share.mePlayer.GetJson());
            jData.Add("oppoData", Engine.share.botPlayer.GetJson());
            jData.Add("gameType", "BOT");
            jData.Add("gameResult", 1);

            NetworkManager.share.SendBySocket(jData.ToString());

            StartCoroutine(ShowGameNotice("you win the game"));
        }

        yield return new WaitForSeconds(2.5f);

        Engine.share.mePlayer.InitializeWithDefault();
        Engine.share.botPlayer.InitializeWithDefault();

        UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
    }



    public void ShowBattleAction(bool winner)
    {

    }

    public IEnumerator ShowGameStateMessages(List<string> messageList)
    {
        yield return new WaitForSeconds(0.1f);
        gameStateNode.SetActive(true);
        stateText.text = "";
        string strText = "";
        for (int i = 0; i < messageList.Count - 1; i++)
        {
            strText += messageList[i];
            strText += "\n";
            //yield return new WaitForSeconds(0.5f);
        }

        strText += messageList[messageList.Count - 1];

        stateText.text = strText;

        yield return new WaitForSeconds(3f);
        JsonObject jData = new JsonObject();

        jData.Add("address", Engine.share.mePlayer.address);
        jData.Add("sessionId", Engine.share.mePlayer.sessionId);
        jData.Add("type", PACKET_TYPE.PT_CARD_MATCH_END);

        NetworkManager.share.SendBySocket(jData.ToString());

        gameStateNode.SetActive(false);

    }

    public void SendCardCompareEnd()
    {
        JsonObject jData = new JsonObject();

        jData.Add("address", Engine.share.mePlayer.address);
        jData.Add("sessionId", Engine.share.mePlayer.sessionId);
        jData.Add("type", PACKET_TYPE.PT_CARD_MATCH_END);

        NetworkManager.share.SendBySocket(jData.ToString());
    }

    public void SetGameState(bool state)
    {
        isWaitingResult = true;
        if (isWaitingResult)
        {
            gameStateNode.SetActive(true);
            stateText.text = "Waiting For Game Card";
        }
        else
        {
            gameStateNode.SetActive(false);
            stateText.text = "";
        }
    }

    public void RecvOppoCardUpdate(JsonObject jData)
    {
        var curId = Convert.ToInt32(jData["curCardId"]);
        var newId = Convert.ToInt32(jData["newCardId"]);

        Engine.share.oppoPlayer.ReplaceBattleCardWithNewCard(curId, newId);

        oppoGameCards.UpdateGameCard(newId);
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
