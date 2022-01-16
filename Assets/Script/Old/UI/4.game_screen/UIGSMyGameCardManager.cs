using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJson;
using DG.Tweening;

public class UIGSMyGameCardManager : MonoBehaviour
{
    public UIGSCardObject[] gameCards;
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
        for (int i = 0; i < gameCards.Length; i++)
        {
            defPos.Add(gameCards[i].transform.localPosition);
            defRot.Add(gameCards[i].transform.localRotation.eulerAngles);
            gameCards[i].transform.localPosition = new Vector3(defPos[i].x, defPos[i].y - 300, defPos[i].z);
        }
        StartCoroutine(InitCardsWithAnim());
    }

    public void OnDisable()
    {
        if(defPos != null && defPos.Count == gameCards.Length)
        {
            for (int i = 0; i < gameCards.Length; i++)
            {
                gameCards[i].transform.localPosition = defPos[i];
                Quaternion rot = gameCards[i].transform.localRotation;
                rot.eulerAngles = defRot[i];
                gameCards[i].transform.localRotation = rot;
            }
        }
    }

    public IEnumerator InitCardsWithAnim()
    {
        for (int i = 0; i < gameCards.Length; i++)
        {
            gameCards[i].InitCardInfo(Engine.share.mePlayer.battleCards[i]);
        }

        yield return new WaitForSeconds(0.35f);

        for(int i = 0; i < gameCards.Length; i ++)
        {
            yield return new WaitForSeconds(0.1f);
            gameCards[i].transform.DOLocalMove(defPos[i], 0.3f);
            gameCards[i].transform.DOLocalRotate(defRot[i], 0.3f);
            gameCards[i].GetComponent<AudioSource>().clip = SoundManager.share.GetSoundEffect(EFX_SOUND.EFXS_DECK_APPEAR);
            gameCards[i].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void UpdateCardStates()
    {
        for(int i = 0; i < gameCards.Length; i ++)
        {
            gameCards[i].UpdateCardState(Engine.share.mePlayer.battleCards[i]);
        }
    }

    public void UpdateMyGameCard(int cardIdx)
    {
        for(int i = 0; i < gameCards.Length; i ++)
        {
            if(gameCards[i].cardData.cardIdx != Engine.share.mePlayer.battleCards[i].cardIdx)
            {
                gameCards[i].InitCardInfo(Engine.share.mePlayer.battleCards[i]);
            }
        }
    }

    public void UpdateCardSelectionState()
    {
        int activeCount = 0;
        for(int i = 0; i < gameCards.Length; i ++)
        {
            if(CheckIfActiveCard(gameCards[i].cardData.cardIdx))
            {
                activeCount++;
                gameCards[i].SetState(true);
            }
            else
            {
                gameCards[i].SetState(false);
            }
        }

        if(activeCount == 1)
        {
            for(int i = 0; i < gameCards.Length; i ++)
            {
                if(gameCards[i].detailObj.activeSelf)
                {
                    gameCards[i].transform.DOLocalMove(oneCardPos, 0.15f);
                    gameCards[i].transform.DOLocalRotate(oneCardRot, 0.15f);
                }
            }
        }
        else if(activeCount == 2)
        {
            int cnt = 0;
            for(int i = 0; i < gameCards.Length; i ++)
            {
                if (gameCards[i].detailObj.activeSelf)
                {
                    gameCards[i].transform.DOLocalMove(twoCardPos[cnt], 0.15f);
                    gameCards[i].transform.DOLocalRotate(twoCardRot[cnt], 0.15f);
                    cnt++;
                }
            }
        }
        else if(activeCount == 3)
        {
            for(int i = 0; i < gameCards.Length; i ++)
            {
                gameCards[i].transform.DOLocalMove(defPos[i], 0.15f);
                gameCards[i].transform.DOLocalRotate(defRot[i], 0.15f);
            }
        }
    }

    public bool CheckIfActiveCard(int cardIdx)
    {
        for(int i = 0; i < Engine.share.mePlayer.battleCards.Count; i ++)
        {
            if(cardIdx == Engine.share.mePlayer.battleCards[i].cardIdx && !Engine.share.mePlayer.battleCards[i].isUsable)
            {
                return false;
            }
        }

        if(cardIdx == Engine.share.mePlayer.curBattleCard)
        {
            return false;
        }

        return true;
    }

    public void GameCardSelected(Card cardData)
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
}
