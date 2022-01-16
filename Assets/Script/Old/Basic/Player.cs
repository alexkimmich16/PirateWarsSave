using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;

public class Player
{
    public string sessionId;
    public string address;

    public List<Card> availableCards;
    public List<Card> selectedCards;
    public List<Card> battleCards;

    public int score;
    public bool isWaitingOppo;

    public int curBattleCard;

    public void InitializeWithDefault()
    {
        for(int i = 0; i < selectedCards.Count; i ++)
        {
            selectedCards[i].isUsable = true;
        }

        for (int i = 0; i < battleCards.Count; i++)
        {
            battleCards[i].isUsable = true;
        }
    }

    public void SetAvailableCards(List<int> userCards)
    {
        battleCards = new List<Card>();
        for(int i = 0; i < userCards.Count; i ++)
        {
            battleCards.Add(selectedCards[userCards[i]]);
        }
    }

    public void UpdateUpdateBattleData(JsonArray jSelCards, JsonArray jBattleCards)
    {
        selectedCards = new List<Card>();
        for(int i = 0; i < jSelCards.Count; i ++)
        {
            Card nCard = new Card((JsonObject)jSelCards[i]);
            selectedCards.Add(nCard);
        }

        battleCards = new List<Card>();
        for(int i = 0; i < jBattleCards.Count; i++)
        {
            Card nCard = new Card((JsonObject)jBattleCards[i]);
            battleCards.Add(nCard);
        }
    }

    public JsonObject GetJson()
    {
        JsonObject jData = new JsonObject();
        jData.Add("sessionId", sessionId);
        jData.Add("address", address);
        jData.Add("battleCard", curBattleCard);
        jData.Add("score", score);

        JsonArray jabilCards = new JsonArray();
        for(int i = 0; i < availableCards.Count; i ++)
        {
            jabilCards.Add(availableCards[i].GetCardJson());
        }

        jData.Add("cardList", jabilCards);

        JsonArray jbattleCards = new JsonArray();
        for(int i = 0; i < selectedCards.Count; i ++)
        {
            jbattleCards.Add(selectedCards[i].GetCardJson());
        }
        jData.Add("battleCards", jbattleCards);

        JsonArray jActiveBattleCard = new JsonArray();
        for(int i = 0; i < battleCards.Count; i ++)
        {
            jActiveBattleCard.Add(battleCards[i].GetCardJson());
        }

        jData.Add("activeBattleCards", jActiveBattleCard);

        return jData;
    }

    public Player(Player userData)
    {
        sessionId = userData.sessionId;
        address = userData.address;

        score = userData.score;
        isWaitingOppo = userData.isWaitingOppo;
        curBattleCard = userData.curBattleCard;

        availableCards = new List<Card>();
        selectedCards = new List<Card>();
        battleCards = new List<Card>();

        for (int i = 0; i < userData.availableCards.Count; i++)
        {
            Card nCard = new Card(userData.availableCards[i]);
            availableCards.Add(nCard);
        }

        for(int i = 0; i < userData.selectedCards.Count; i ++)
        {
            Card nCard = new Card(userData.selectedCards[i]);
            selectedCards.Add(nCard);
        }

        for(int i = 0; i < userData.battleCards.Count; i ++)
        {
            Card nCard = new Card(userData.battleCards[i]);
            battleCards.Add(nCard);
        }
    }

    public void UpdateBotCardList(List<Card> cardData)
    {
        availableCards = new List<Card>();
        selectedCards = new List<Card>();
        battleCards = new List<Card>();

        for(int i = 0; i < cardData.Count; i ++)
        {
            availableCards.Add(cardData[i]);
        }

        for (int i = 0; i < cardData.Count; i++)
        {
            selectedCards.Add(cardData[i]);
        }
    }

    public void ActiveAllCards()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            selectedCards[i].isUsable = true;
        }

        for (int i = 0; i < battleCards.Count; i++)
        {
            battleCards[i].isUsable = true;
        }
    }

    public Player()
    {
        sessionId = "";
        address = "";

        availableCards = new List<Card>();
        selectedCards = new List<Card>();
        battleCards = new List<Card>();

        score = 0;
        isWaitingOppo = false;
        curBattleCard = -1;
    }

    public Player(string sid, string add)
    {
        sessionId = sid;
        address = add;

        availableCards = new List<Card>();
        selectedCards = new List<Card>();
        battleCards = new List<Card>();

        score = 0;
        isWaitingOppo = false;
        curBattleCard = -1;
    }

    public Player(JsonObject jData)
    {
        sessionId = "";
        address = "";
        availableCards = new List<Card>();
        selectedCards = new List<Card>();
        battleCards = new List<Card>();

        score = 0;
        isWaitingOppo = false;
        curBattleCard = -1;
        if (jData.ContainsKey("sessionId"))
        {
            sessionId = Convert.ToString(jData["sessionId"]);
        }

        if(jData.ContainsKey("address"))
        {
            address = Convert.ToString(jData["address"]);
        }

        if(jData.ContainsKey("battleCards"))
        {
            JsonArray jArr = (JsonArray)jData["battleCards"];

            for(int i = 0; i < jArr.Count; i ++)
            {
                JsonObject jCard = (JsonObject)jArr[i];
                Card nCard = new Card(jCard, true);

                selectedCards.Add(nCard);
            }
        }

        if(jData.ContainsKey("activeBattleCards"))
        {
            JsonArray jArr = (JsonArray)jData["activeBattleCards"];

            for (int i = 0; i < jArr.Count; i++)
            {
                JsonObject jCard = (JsonObject)jArr[i];
                Card nCard = new Card(jCard, true);

                battleCards.Add(nCard);
            }
        }
    }

    public void SetAvailableCards(List<Card> lCards)
    {
        availableCards = new List<Card>();
        availableCards = lCards;
    }

    public void SetBattleCards(List<Card> bCards)
    {
        battleCards = new List<Card>();
        battleCards = bCards;

        for(int i = 0; i < battleCards.Count; i ++)
        {
            battleCards[i].isUsable = true;
        }
    }

    public void SetSelectedCards(List<Card> scards)
    {
        selectedCards = new List<Card>();
        selectedCards = scards;
    }

    public void SetBattleCardIdx(int cardIdx)
    {
        curBattleCard = cardIdx;
    }

    public void SetBattleCardState(int cardIdx, bool state)
    {
        for(int i = 0; i < battleCards.Count; i ++)
        {
            if(battleCards[i].cardIdx == cardIdx)
            {
                battleCards[i].isUsable = state;
            }
        }

        for(int i = 0; i < selectedCards.Count; i ++)
        {
            if(selectedCards[i].cardIdx == cardIdx)
            {
                selectedCards[i].isUsable = state;
            }
        }

        curBattleCard = -1;
    }

    public int CardUpdateForLosedCard(int cardIdx)
    {
        for(int i = 0; i < battleCards.Count; i ++)
        {
            if(battleCards[i].cardIdx == cardIdx)
            {
                Card nCard = GetMissingCard();

                if(nCard != null)
                {
                    battleCards[i] = nCard;
                    return battleCards[i].cardIdx;
                }
            }
        }
        return -1;
    }

    public Card GetMissingCard()
    {
        for(int i = 0; i < selectedCards.Count; i ++)
        {
            int idx = selectedCards[i].cardIdx;
            bool usedState = false;
            if(selectedCards[i].isUsable)
            {
                for(int j = 0; j < battleCards.Count; j ++)
                {
                    if(idx == battleCards[j].cardIdx)
                    {
                        usedState = true;
                    }
                }

                if(!usedState)
                {
                    return selectedCards[i];
                }
            }
        }

        return null;
    }

    public void UpdateUserBattleCardInfo(JsonObject jObj)
    {
        if(jObj.ContainsKey("battleCard"))
        {
            curBattleCard = Convert.ToInt32(jObj["battleCard"]);
        }

        JsonArray jncards = (JsonArray)jObj["activeBattleCards"];

        for (int i = 0; i < jncards.Count; i++)
        {
            JsonObject jNCard = (JsonObject)jncards[i];
            bool isUsable = Convert.ToBoolean(jNCard["isUsable"]);
            int cIdx = Convert.ToInt32(jNCard["id"]);

            for(int j = 0; j < battleCards.Count; j ++)
            {
                if(battleCards[j].cardIdx == cIdx)
                {
                    battleCards[j].isUsable = isUsable;
                }
            }
        }
    }

    public Card GetBattleCard(int cardIdx)
    {
        for(int i = 0; i < battleCards.Count; i ++)
        {
            if(battleCards[i].cardIdx == cardIdx)
            {
                return battleCards[i];
            }
        }

        return null;
    }

    public int GetActiveCardCount()
    {
        int activeCount = 0;
        for(int i = 0; i < battleCards.Count; i ++)
        {

        }

        return activeCount;
    }

    public int GetAvailableCardCount()
    {
        int acount = 0;
        for(int i = 0; i < battleCards.Count; i ++)
        {
            if(battleCards[i].isUsable)
            {
                acount++;
            }
        }

        return acount;
    }

    public void ReplaceBattleCardWithNewCard(int curId, int newId)
    {
        for(int i = 0; i < battleCards.Count; i ++)
        {
            if(battleCards[i].cardIdx == curId)
            {
                for(int j = 0; j < selectedCards.Count; j ++)
                {
                    if(selectedCards[j].cardIdx == newId)
                    {
                        battleCards[i] = selectedCards[j];
                        return;
                    }
                }
            }
        }
    }

    public void SetBotPlayerInfo()
    {
        GetFiveRandCards();
        GetBattleCards();
        curBattleCard = -1;
    }

    public void GetBattleCards()
    {
        int cnt = 0;
        battleCards = new List<Card>();
        while (cnt != 3)
        {
            int idx = UnityEngine.Random.RandomRange(0, selectedCards.Count);
            bool ispicked = false;
            for (int i = 0; i < battleCards.Count; i++)
            {
                if (battleCards[i].cardIdx == selectedCards[idx].cardIdx)
                {
                    ispicked = true;
                }
            }

            if (!ispicked)
            {
                battleCards.Add(availableCards[idx]);
                cnt++;
            }
        }
    }

    public void GetFiveRandCards()
    {
        int cnt = 0;
        selectedCards = new List<Card>();
        while(cnt != 5)
        {
            int idx = UnityEngine.Random.RandomRange(0, availableCards.Count);
            bool ispicked = false;
            for(int i = 0; i < selectedCards.Count; i ++)
            {
                if (selectedCards[i].cardIdx == availableCards[idx].cardIdx)
                {
                    ispicked = true;
                }
            }

            if(!ispicked)
            {
                selectedCards.Add(availableCards[idx]);
                cnt++;
            }
        }
    }
}
