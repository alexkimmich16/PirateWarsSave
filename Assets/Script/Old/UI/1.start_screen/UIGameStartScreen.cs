using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;

public class UIGameStartScreen : MonoBehaviour
{
    static public UIGameStartScreen share;

    public GameObject sessionError;

    public GameObject stateObj;
    public Text gameText;
    public RenderTexture vRenderer;
    public Vector3 firstPos, secondPos;

    public UnityEngine.Video.VideoPlayer backgroundPlayer;
    public RawImage videoPanelImage;
    public UnityEngine.Video.VideoClip[] bgVideos;
    public string[] videoLinks;

    bool isShow = false;
    float startTime = 0f;

    private void OnEnable()
    {
        sessionError.SetActive(false);
        stateObj.SetActive(false);
        videoPanelImage.gameObject.SetActive(false);
        StartCoroutine(LoadBackground());
    }

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public void SelectDeckButtonClicked()
    {
        UIManager.share.OpenWindow(GAME_WINDOW.GW_DECK_SEL);
    }

    public void PlayerVSPlayerButtonClicked()
    {
        if (Engine.share.CheckIfReadyForPlay())
        {
            if (UIEnergyBar.share.curEnergyVal >= 10)
            {
                JsonObject jData = new JsonObject();
                jData.Add("type", PACKET_TYPE.PT_VALIDATE_ENERGY_PVP);
                jData.Add("sessionId", Engine.share.mePlayer.sessionId);
                jData.Add("address", Engine.share.mePlayer.address);

                NetworkManager.share.SendBySocket(jData.ToString());
                //UIManager.share.OpenWindow(GAME_WINDOW.GW_WAITING_ROOM);
            }
            else
            {

            }
        }
        else
        {
            isShow = true;
            startTime = Time.time;
            stateObj.SetActive(true);
            stateObj.transform.localPosition = firstPos;
            gameText.text = "Select Deck first!";
        }
    }

    public void PlayerVSBotClicked()
    {
        if(Engine.share.CheckIfReadyForPlay())
        {
            if(UIEnergyBar.share.curEnergyVal >= 10)
            {
                JsonObject jData = new JsonObject();
                jData.Add("type", PACKET_TYPE.PT_VALIDATE_ENERGY_BOT);
                jData.Add("sessionId", Engine.share.mePlayer.sessionId);
                jData.Add("address", Engine.share.mePlayer.address);

                NetworkManager.share.SendBySocket(jData.ToString());
                //UIManager.share.OpenWindow(GAME_WINDOW.GW_GAME_BOT);
            }
            else
            {

            }
        }
        else
        {
            isShow = true;
            startTime = Time.time;
            stateObj.SetActive(true);
            stateObj.transform.localPosition = secondPos;
            gameText.text = "Select Deck first!";
        }
    }

    public void SessionValidataionError(bool state)
    {
        sessionError.SetActive(!state);
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_START_MENU_OPENING);
        StartCoroutine(LoadBackground());
    }
    public IEnumerator LoadBackground()
    {
        yield return new WaitForSeconds(1.5f);

        int bgRd = UnityEngine.Random.RandomRange(0, videoLinks.Length);

        backgroundPlayer.url = videoLinks[bgRd];//System.IO.Path.Combine(Application.streamingAssetsPath, videoLinks[bgRd]);
        vRenderer = new RenderTexture(1024, 1024, 32, RenderTextureFormat.ARGB32);
        vRenderer.Create();
        backgroundPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        //backgroundPlayer.clip = bgVideos[bgRd];
        backgroundPlayer.isLooping = true;
        backgroundPlayer.targetTexture = vRenderer;
        videoPanelImage.gameObject.SetActive(true);
        videoPanelImage.texture = vRenderer;        
        backgroundPlayer.Prepare();
        //backgroundPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {
            if(Time.time - startTime > 3f)
            {
                stateObj.SetActive(false);
                isShow = false;
            }
        }
    }
}
