using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader instance;
    void Awake()
    {
        if (Current > HighestPriority)
        {
            HighestPriority = Current;
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    [System.Serializable]
    public class SceneStats
    {
        public string Name;
        public string SceneName;
    }
    #endregion

    public float FadeTime;

    public List<SceneStats> Scenes;
    
    private float Timer;

    public bool ClickActive;

    private static int HighestPriority = 0;
    public int Current = 1;

    public bool IsPirate;
    public int TypeNumInList;

    public AllInfo.StatMultiplier Before;
    public AllInfo.StatMultiplier Added;

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > FadeTime)
        {
            ClickActive = true;
        }
        else
        {
            ClickActive = false;
        }
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
        if (TransitionManager.instance.transitionMenu != null)
        {
            TransitionManager.instance.transitionMenu.SetTrigger("Start");
            yield return new WaitForSeconds(FadeTime);
            //yield return new WaitForSeconds(10f);
        }
        Current += 1;
        DontDestroyOnLoad(gameObject);
        
        Timer = 0;
        SceneManager.LoadScene(sceneStats.SceneName);
        //Time.timeScale = 1f;
    }
}
