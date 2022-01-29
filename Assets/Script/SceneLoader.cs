using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader instance;
    void Awake() { instance = this; }

    [System.Serializable]
    public class SceneStats
    {
        public string Name;
        public Scene scene;
    }
    #endregion

    public static List<SceneStats> Scenes;

    public Animator transitionMenu;

    void Start()
    {
        /*
        if (UpgradeScript.Level != 0 && UpgradeScript.Level != 1)
        {
            ADManager.Instance.ShowInterstitial();
        }

        if (UpgradeScript.First == true)
        {
            MusicStatic = Music;

            if (UpgradeScript.ShouldSave == true)
            {
                SaveSystem.LoadGameLarge();
            }

        }
        VideoButton = GameObject.Find("WatchVideo");
        ADManager.Instance.Video = VideoButton;
        if (ADManager.Instance.IntersitialAdTime < ADManager.Instance.InterstitialTime)
        {
            ADManager.Instance.InterstitialTime = 0;
        }
        ADManager.Instance.ShowBanner();
        */
    }
    public void LoadScene(string text)
    {
        for (int i = 0; i < Scenes.Count; i++)
        {
            //Debug.Log("PT3");
            if (text == Scenes[i].Name)
            {
                StartCoroutine(LoadTime(Scenes[i]));
                return;
            }
        }
        Debug.LogError("could not find scene of name:" + text);
        
    }

    IEnumerator LoadTime(SceneStats sceneStats)
    {
        /*
        if (Level == 0 || Level == 1)
        {
            Time.timeScale = 1f;

        }
        
        if (Bools[Level] == true)
        {
            //transitionMenu.SetTrigger("Start");
        }
        */

        if (transitionMenu != null)
        {
            transitionMenu.SetTrigger("Start");
            yield return new WaitForSeconds(.8f);
        }
        //don't destroy on loads
        //DontDestroyOnLoad(MusicStatic);

        SceneManager.LoadScene(sceneStats.Name);
        //Time.timeScale = 1f;
    }
}
