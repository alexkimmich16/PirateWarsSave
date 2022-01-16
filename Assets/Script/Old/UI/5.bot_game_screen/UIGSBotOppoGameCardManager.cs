using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using System;
using DG.Tweening;

public class UIGSBotOppoGameCardManager : MonoBehaviour
{
    public UIGSCardObject[] oppoCardsList;
    public GameObject hiddenCard;
    public List<Vector3> defPos;
    public List<Vector3> defRot;
    public List<Vector3> twoCardPos;
    public List<Vector3> twoCardRot;
    public Vector3 oneCardPos;
    public Vector3 oneCardRot;
    public void InitGameCards()
    {
        defPos = new List<Vector3>();
        defRot = new List<Vector3>();
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            defPos.Add(oppoCardsList[i].gameObject.transform.localPosition);
            defRot.Add(oppoCardsList[i].gameObject.transform.localRotation.eulerAngles);
            oppoCardsList[i].transform.localPosition = new Vector3(defPos[i].x, defPos[i].y + 300, defPos[i].z);
            oppoCardsList[i].InitCardInfo(Engine.share.botPlayer.battleCards[i]);
        }
        defPos.Add(hiddenCard.gameObject.transform.localPosition);
        defRot.Add(hiddenCard.gameObject.transform.localRotation.eulerAngles);
        hiddenCard.transform.localPosition = new Vector3(defPos[2].x, defPos[2].y + 300, defPos[2].z);
        hiddenCard.gameObject.SetActive(true);

        StartCoroutine(IEInitGameCards());
    }

    public void OnDisable()
    {
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            oppoCardsList[i].transform.DOLocalMove(defPos[i], 0f);
            oppoCardsList[i].transform.DOLocalRotate(defRot[i], 0f);
        }

        hiddenCard.transform.DOLocalMove(defPos[2], 0f);
        hiddenCard.transform.DOLocalRotate(defRot[2], 0f);
    }

    public IEnumerator IEInitGameCards()
    {
        yield return new WaitForSeconds(0.35f);
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            oppoCardsList[i].transform.DOLocalMove(defPos[i], 0.3f);
            oppoCardsList[i].transform.DOLocalRotate(defRot[i], 0.3f);
            oppoCardsList[i].GetComponent<AudioSource>().clip = SoundManager.share.GetSoundEffect(EFX_SOUND.EFXS_DECK_APPEAR);
            oppoCardsList[i].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.15f);
        }
        hiddenCard.transform.DOLocalMove(defPos[2], 0.3f);
        hiddenCard.transform.DOLocalRotate(defRot[2], 0.3f);
    }

    public void UpdateGameCard(int cardIdx)
    {
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            if (oppoCardsList[i].cardData.cardIdx != Engine.share.botPlayer.battleCards[i].cardIdx)
            {
                oppoCardsList[i].InitCardInfo(Engine.share.botPlayer.battleCards[i]);
            }
        }
    }
    public void UpdateBotGameCard(int cardIdx)
    {
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            if (oppoCardsList[i].cardData.cardIdx != Engine.share.botPlayer.battleCards[i].cardIdx)
            {
                oppoCardsList[i].InitCardInfo(Engine.share.botPlayer.battleCards[i]);
            }
        }
    }
    public void UpdateCardStates()
    {
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            oppoCardsList[i].UpdateCardState(Engine.share.botPlayer.battleCards[i]);
        }
    }

    public void OppoCardSelected()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetActiveCount()
    {
        int cnt = 0;
        for (int i = 0; i < Engine.share.botPlayer.battleCards.Count; i++)
        {
            if (Engine.share.botPlayer.battleCards[i].isUsable && Engine.share.botPlayer.curBattleCard != Engine.share.botPlayer.battleCards[i].cardIdx)
            {
                cnt++;
            }
        }

        return cnt;
    }

    public void UpdateCardSelectionState()
    {
        int activeCount = GetActiveCount();
        for (int i = 0; i < oppoCardsList.Length; i++)
        {
            //if (CheckIfActiveCard(oppoCardsList[i].cardData.cardIdx))
            //{
            //    //activeCount++;
            //    oppoCardsList[i].SetState(true);
            //}
            //else
            //{
            //    oppoCardsList[i].SetState(false);
            //}

            for (int j = 0; j < Engine.share.botPlayer.battleCards.Count; j++)
            {
                if (oppoCardsList[i].cardData.cardIdx == Engine.share.botPlayer.battleCards[j].cardIdx)
                {
                    if (Engine.share.botPlayer.battleCards[j].isUsable)
                    {
                        oppoCardsList[i].SetState(true);
                    }
                    else
                    {
                        oppoCardsList[i].SetState(false);
                    }
                }
            }
        }

        if (activeCount == 1)
        {
            int cnt = 0;
            for (int i = 0; i < oppoCardsList.Length; i++)
            {
                if (oppoCardsList[i].detailObj.activeSelf)
                {
                    oppoCardsList[i].transform.DOLocalMove(oneCardPos, 0.15f);
                    oppoCardsList[i].transform.DOLocalRotate(oneCardRot, 0.15f);
                    hiddenCard.gameObject.SetActive(false);
                    cnt++;
                }
            }

            if (cnt < 1)
            {
                hiddenCard.gameObject.SetActive(true);
                hiddenCard.gameObject.transform.DOLocalMove(oneCardPos, 0.15f);
                hiddenCard.gameObject.transform.DOLocalRotate(oneCardRot, 0.15f);
            }
        }
        else if (activeCount == 2)
        {
            int cnt = 0;
            for (int i = 0; i < oppoCardsList.Length; i++)
            {
                if (oppoCardsList[i].detailObj.activeSelf)
                {
                    oppoCardsList[i].transform.DOLocalMove(twoCardPos[cnt], 0.15f);
                    oppoCardsList[i].transform.DOLocalRotate(twoCardRot[cnt], 0.15f);
                    cnt++;
                }
            }

            if (cnt != 2)
            {
                hiddenCard.gameObject.SetActive(true);
                hiddenCard.transform.DOLocalMove(twoCardPos[1], 0.15f);
                hiddenCard.transform.DOLocalRotate(twoCardRot[1], 0.15f);
            }
            else
            {
                hiddenCard.gameObject.SetActive(false);
            }
        }
        else if (activeCount == 3)
        {
            int cnt = 0;
            for (int i = 0; i < oppoCardsList.Length; i++)
            {
                oppoCardsList[i].transform.DOLocalMove(defPos[i], 0.15f);
                oppoCardsList[i].transform.DOLocalRotate(defRot[i], 0.15f);
                cnt++;
            }

            hiddenCard.gameObject.SetActive(true);
            hiddenCard.transform.DOLocalMove(defPos[2], 0.15f);
            hiddenCard.transform.DOLocalRotate(defRot[2], 0.15f);
        }
    }

    public bool CheckIfActiveCard(int cardIdx)
    {
        for (int i = 0; i < Engine.share.botPlayer.battleCards.Count; i++)
        {
            if (cardIdx == Engine.share.botPlayer.battleCards[i].cardIdx && !Engine.share.botPlayer.battleCards[i].isUsable)
            {
                return false;
            }
        }

        if (cardIdx == Engine.share.botPlayer.curBattleCard)
        {
            return false;
        }

        return true;
    }
}
