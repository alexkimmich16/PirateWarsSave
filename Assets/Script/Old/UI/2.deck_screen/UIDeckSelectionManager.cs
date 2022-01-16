using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using UnityEngine.Networking;
using System;
using DG.Tweening;

public class UIDeckSelectionManager : MonoBehaviour
{
    static public UIDeckSelectionManager share;

    public Text curSelCardCount;

    public GameObject cardStateObj;

    public List<int> availableCardList;
    public List<Card> cardDataList;

    public List<Card> curselCardList;

    public GameObject gameCardPrefab;
    public GameObject cardListParent;

    public GameObject leftButton;
    public GameObject rightButton;

    public GameObject confirm_disable, confirm_active;

    public int curselDeckCount = 0;
    public int displayPos = 0;

    void Awake()
    {
        if (share == null)
        {
            share = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        InitializeCardDecks();
    }

    public void InitializeCardDecks()
    {
        curselDeckCount = 0;
        displayPos = 0;
        curselCardList = new List<Card>();
        MoveButtonStateUpdate();
        UpdateCardSelectionState();
        StartCoroutine(IEGetDeckList());
        UpdateLatestUpdateStateWithCount();
    }

    public void UpdateCardSelectionState()
    {
        curselDeckCount = curselCardList.Count;
        curSelCardCount.text = curselDeckCount.ToString() + "/" + GameManager.share.possibleCardCount.ToString();

        if (curselDeckCount >= GameManager.share.possibleCardCount)
        {
            confirm_active.SetActive(true);
            confirm_disable.SetActive(false);
            //cardStateObj.SetActive(false);
        }
        else
        {
            confirm_active.SetActive(false);
            confirm_disable.SetActive(true);
            //cardStateObj.SetActive(true);
        }
    }

    public IEnumerator IEGetDeckList()
    {
        string deckURL = GameManager.share.metaServerURL + "/deck/" + Engine.share.mePlayer.address;
        UnityWebRequest uri = UnityWebRequest.Get(deckURL);

        yield return uri.SendWebRequest();

        if(uri.isNetworkError)
        {

        }
        else
        {
            Debug.Log("---url---" + deckURL);
            Debug.Log("---user deck list---" + uri.downloadHandler.text);
            string cardListData = uri.downloadHandler.text;
            JsonArray jArr = (JsonArray)SimpleJson.SimpleJson.DeserializeObject(cardListData);

            if(jArr != null)
            {
                availableCardList = new List<int>();
                for(int i = 0; i < jArr.Count; i ++)
                {
                    int cNumber = Convert.ToInt32(jArr[i]);
                    
                    availableCardList.Add(cNumber);
                }
                GetCardDetails();
            }

            JsonObject jData = new JsonObject();
            jData.Add("type", PACKET_TYPE.PT_UPDATE_AVAILABLE_CARD_LIST);
            jData.Add("cardList", jArr);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("address", Engine.share.mePlayer.address);

            NetworkManager.share.SendBySocket(jData.ToString());
        }
    }

    int requestCount = 0;

    public void GetCardDetails()
    {
        cardDataList = new List<Card>();
        for(int i = 0; i < availableCardList.Count; i ++)
        {
            requestCount++;
            StartCoroutine(IEGetCardDetails(availableCardList[i]));
        }

        Debug.Log("---user card data---" + cardDataList.Count + "---" + Time.time);
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
                cardDataList.Add(cData);                
            }
        }
        requestCount--;
        if (requestCount == 0)
        {
            Debug.Log("----card details ----" + cardDataList.Count + "---" + Time.time);
            ShowCardList();
        }
    }

    public void ShowCardList()
    {
        Engine.share.mePlayer.SetAvailableCards(cardDataList);

        RectTransform rt = cardListParent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(332 * cardDataList.Count, 550);
        int xPos = ((availableCardList.Count / 2) - displayPos) * 332 - 332 * 2;
        cardListParent.transform.localPosition = new Vector3(xPos, 0, 0);

        //SetScrollPosition();

        if (cardListParent.transform.childCount > cardDataList.Count)
        {
            for (int i = 0; i < cardDataList.Count; i++)
            {
                GameObject obj = cardListParent.transform.GetChild(i).gameObject;
                obj.GetComponent<UIGameCardObject>().cardData = cardDataList[i];
                obj.GetComponent<UIGameCardObject>().SetState(false);
                //obj.GetComponent<UIGameCardObject>().InitCardInfo(cardDataList[i]);
            }

            for (int i = cardListParent.transform.childCount - 1; i >= cardDataList.Count; i--)
            {
                Destroy(cardListParent.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for(int i = 0; i < cardListParent.transform.childCount; i ++)
            {
                GameObject obj = cardListParent.transform.GetChild(i).gameObject;
                obj.GetComponent<UIGameCardObject>().cardData = cardDataList[i];
                //obj.GetComponent<UIGameCardObject>().InitCardInfo(cardDataList[i]);
                obj.GetComponent<UIGameCardObject>().SetState(false);
            }

            for(int i = cardListParent.transform.childCount; i < cardDataList.Count; i ++)
            {
                GameObject obj = GameObject.Instantiate(gameCardPrefab) as GameObject;
                obj.transform.parent = cardListParent.transform;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                obj.GetComponent<UIGameCardObject>().cardData = cardDataList[i];
                obj.GetComponent<UIGameCardObject>().SetState(false);
                //obj.GetComponent<UIGameCardObject>().InitCardInfo(cardDataList[i]);
            }
        }

        MoveButtonStateUpdate();
        //UpdateLatestUpdateStateWithCount();
    }

    public void UpdateLatestUpdateStateWithCount()
    {
        for (int i = 0; i < cardListParent.transform.childCount; i++) 
        {
            if (cardListParent.transform.GetChild(i).gameObject.GetComponent<UIGameCardObject>().isSelected)
            {
                curselCardList.Add(cardListParent.transform.GetChild(i).gameObject.GetComponent<UIGameCardObject>().cardData);
            }
        }
        UpdateCardSelectionState();
    }

    public void DisableObject(Card cdata)
    {
        for(int i = 0; i < cardListParent.transform.childCount; i ++)
        {
            GameObject obj = cardListParent.transform.GetChild(i).gameObject;
            int idx = obj.GetComponent<UIGameCardObject>().cardData.cardIdx;
            if(idx == cdata.cardIdx)
            {
                obj.GetComponent<UIGameCardObject>().DisableSelection();
                return;
            }
        }
    }

    public void CardObjectSelected(Card cData, bool selState)
    {
        if (curselCardList.Count >= GameManager.share.possibleCardCount && selState)
        {
            DisableObject(cData);
            return;
        }

        if (selState)
        {
            for(int i = 0; i < curselCardList.Count; i ++)
            {
                if(curselCardList[i].cardIdx == cData.cardIdx)
                {
                    return;
                }
            }
            curselCardList.Add(cData);
        }
        else
        {
            for (int i = 0; i < curselCardList.Count; i++)
            {
                if(curselCardList[i].cardIdx == cData.cardIdx)
                {
                    curselCardList.RemoveAt(i);
                }
            }
        }
        UpdateCardSelectionState();
    }

    public void ConfirmButtonClicked()
    {
        if(curselCardList.Count >= GameManager.share.possibleCardCount)
        {
            Debug.Log("can confirm it");
            Engine.share.selectedCardList = new List<Card>();
            Engine.share.selectedCardList = curselCardList;
            Engine.share.mePlayer.SetSelectedCards(curselCardList);

            JsonObject jData = new JsonObject();
            jData.Add("type", PACKET_TYPE.PT_CONFIRM_CARD);
            jData.Add("sessionId", Engine.share.mePlayer.sessionId);
            jData.Add("address", Engine.share.mePlayer.address);

            JsonArray jCards = new JsonArray();

            for(int i = 0; i < curselCardList.Count; i ++)
            {
                jCards.Add(curselCardList[i].GetCardJson());
            }

            jData.Add("selCards", jCards);
            NetworkManager.share.SendBySocket(jData.ToString());

            UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
        }
        else
        {
            Debug.Log("missing card count");
            cardStateObj.SetActive(true);
        }
    }

    public void CancelButtonClicked()
    {
        UIManager.share.OpenWindow(GAME_WINDOW.GW_START);
    }

    public void SetScrollPosition(bool direct)
    {
        float curXPos = cardListParent.transform.localPosition.x;

        if(direct)
        {
            curXPos -= 332f;
        }
        else
        {
            curXPos += 332f;
        }

        cardListParent.transform.DOLocalMove(new Vector3(curXPos, 0, 0), 0.15f);
    }

    public void MoveButtonStateUpdate()
    {
        if (displayPos <= 0)
        {
            leftButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            leftButton.GetComponent<Button>().interactable = true;
        }

        if (displayPos >= availableCardList.Count - 4)
        {
            rightButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            rightButton.GetComponent<Button>().interactable = true;
        }

        for(int i = 0; i < cardListParent.transform.childCount; i ++)
        {
            if(i >= displayPos && i <= displayPos + 5)
            {
                if(i >= cardListParent.transform.childCount)
                {
                    
                }
                else
                {
                    GameObject obj = cardListParent.transform.GetChild(i).gameObject;
                    obj.GetComponent<UIGameCardObject>().SetState(true);
                }
            }
            else
            {
                GameObject obj = cardListParent.transform.GetChild(i).gameObject;
                obj.GetComponent<UIGameCardObject>().SetState(false);
            }
        }
    }

    public void MoveLeftClicked()
    {
        if(leftButton.GetComponent<Button>().interactable)
        {
            displayPos--;
            SetScrollPosition(false);
        }
        else
        {
            return;
        }
        MoveButtonStateUpdate();
    }

    public void MoveRightClicked()
    {
        if (rightButton.GetComponent<Button>().interactable)
        {
            displayPos++;
            SetScrollPosition(true);
        }
        else
        {
            return;
        }
        MoveButtonStateUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
