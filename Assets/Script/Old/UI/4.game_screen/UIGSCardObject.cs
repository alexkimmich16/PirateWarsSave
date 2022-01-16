using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using RSG;
using Proyecto26;

public class UIGSCardObject : MonoBehaviour
{
    public Card cardData;

    public UnityEngine.Video.VideoPlayer videoPlayer;
    public RawImage videoPanelImage;
    public Image characterImage;
    public Image cardBgImage;
    public RenderTexture vRenderer;

    public Image rareImage;
    public Image powerImage;
    public Image cardTypeImage;

    public GameObject detailObj;
    public GameObject maskObj;

    public bool isSelected = false;

    bool isInitialized = false;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (isInitialized)
        {
            InitCardInfo(cardData);
        }
    }

    public void SetCardDetailInfo()
    {
        cardTypeImage.sprite = GameManager.share.cardTypeImages[(int)cardData.cardType];
        //cardTypeImage.transform.gameObject.SetActive(false);
        if (cardData.power > 0)
        {
            powerImage.sprite = GameManager.share.powerImages[cardData.power - 1];
            //powerImage.transform.gameObject.SetActive(false);
        }

        if (cardData.rareTier > 0)
        {
            rareImage.sprite = GameManager.share.rareImages[cardData.rareTier - 1];
            //rareImage.transform.gameObject.SetActive(false);
        }

    }

    public void SetCardBackground(Texture2D texture)
    {
        if (texture == null)
        {
            return;
        }
        cardBgImage.gameObject.SetActive(true);
        var rect = new Rect(0, 0, texture.width, texture.height);
        var sprite = Sprite.Create(texture, rect, Vector2.one / 2);
        cardBgImage.sprite = sprite;
    }

    public void InitCardInfo(Card cInfo)
    {
        detailObj.SetActive(true);
        cardData = cInfo;
        isInitialized = true;
        isSelected = false;

        videoPanelImage.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);

        maskObj.SetActive(false);

        SetCardDetailInfo();

        if (GameManager.share.gameBGMode == GAME_BG_MODE.GBM_IMAGE)
        {
            cardBgImage.gameObject.SetActive(false);
            videoPanelImage.gameObject.SetActive(false);

            LoadAvatar(cardData.cardBgImgURL).Done(this.SetCardBackground);
        }
        else
        {
            if (cardData.cardBgURL != "" && cardData.cardBgURL.Length > 0)
            {
                videoPlayer.url = cardData.cardBgURL;

                vRenderer = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
                vRenderer.Create();

                videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
                videoPlayer.targetTexture = vRenderer;
                videoPlayer.isLooping = true;
                videoPanelImage.gameObject.SetActive(true);
                videoPanelImage.texture = vRenderer;
                videoPlayer.Prepare();
            }
        }

        if (cardData.cardImgURL.Length > 0 && cardData.cardImgURL != "")
        {
            //LoadAvatar(cardData.cardImgURL).Done(this.SetAvatar);
            LoadAvatar(cardData.cardCleanImgURL).Done(this.SetAvatar);
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
    }

    public void SetState(bool objState)
    {
        if (objState)
        {
            detailObj.SetActive(true);
        }
        else
        {
            detailObj.SetActive(false);
        }
    }

    public void CardSelected()
    {
        if(UIManager.share.gameScreen.activeSelf)
        {
            UIGameController.share.MyCardSelected(cardData);
        }
        else if(UIManager.share.gameBotScreen.activeSelf)
        {
            UIBotGameController.share.MyCardSelected(cardData);
        }
    }

    public void UpdateCardState(Card cData)
    {
        if(cData.isUsable)
        {
            maskObj.SetActive(false);
        }
        else
        {
            maskObj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
