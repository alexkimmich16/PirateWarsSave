using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using SimpleJson;
using UnityEngine.Networking;
using RSG;
using Proyecto26;
using DG.Tweening;

public class UIGSBotMyGameInfo : MonoBehaviour
{
    public Card cardData;

    public GameObject detailObj;
    public Image characterImage;
    public Image cardTypeImage;
    public Image powerImage;
    public Image rareImage;

    public void InitGameInfo(Card cData)
    {
        detailObj.SetActive(true);
        cardData = cData;
        SetCardDetailInfo();
    }

    public void SetCardDetailInfo()
    {
        characterImage.gameObject.SetActive(false);
        cardTypeImage.sprite = GameManager.share.cardTypeImages[(int)cardData.cardType];
        if (cardData.power > 0)
        {
            powerImage.sprite = GameManager.share.powerImages[cardData.power - 1];
        }

        if (cardData.rareTier > 0)
        {
            rareImage.sprite = GameManager.share.rareImages[cardData.rareTier - 1];
        }

        if (cardData.cardImgURL.Length > 0 && cardData.cardImgURL != "")
        {
            LoadAvatar(cardData.cardCleanImgURL).Done(this.SetAvatar);
            //LoadAvatar(cardData.cardImgURL).Done(this.SetAvatar);
        }
    }

    public IPromise<Texture2D> LoadAvatar(string url)
    {
        return GetAvatar(url);
    }

    public IPromise<Texture2D> GetAvatar(string url)
    {
        var promise = new Promise<Texture2D>();
        RestClient.Get(new RequestHelper
        {
            Uri = url,
            DownloadHandler = new DownloadHandlerTexture(true)
        }).Then(response =>
        {
            var texture = ((DownloadHandlerTexture)response.Request.downloadHandler).texture;
            promise.Resolve(texture);
        }).Catch(err => { promise.Reject(err); });
        return promise;
    }

    public void SetAvatar(Texture2D texture)
    {
        if (texture == null)
        {
            return;
        }
        characterImage.gameObject.SetActive(true);
        var rect = new Rect(0, 0, texture.width, texture.height);
        var sprite = Sprite.Create(texture, rect, Vector2.one / 2);
        characterImage.sprite = sprite;

        characterImage.gameObject.SetActive(true);
    }

    public void SetCardActiveState(bool state)
    {
        if (state)
        {
            detailObj.SetActive(true);
        }
        else
        {
            detailObj.SetActive(false);
        }
    }

    public void ShowBattleAnim()
    {
        characterImage.gameObject.transform.DOLocalMoveX(1000, 0.4f).OnComplete(() =>
        {
            characterImage.gameObject.transform.DOLocalMoveX(463, 0.4f);
        });
    }

    public void ShowGameLoseState()
    {
        StartCoroutine(IEShowLoseState());
    }

    public IEnumerator IEShowLoseState()
    {
        characterImage.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        characterImage.color = Color.white;
    }

    public void DisableCard()
    {
        Debug.Log("disable called");
        detailObj.SetActive(false);
        cardData = new Card();
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
