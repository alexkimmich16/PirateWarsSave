using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevels : MonoBehaviour
{
    #region Singleton
    public static MapLevels instance;
    void Awake() { instance = this; }


    [System.Serializable]
    public class Level
    {
        public string Name;
        public List<AllInfo.GamePirate> Pirates;  
    }
    #endregion
    public void SetActivePirates(List<AllInfo.GamePirate> NewActivePirates)
    {
        MyActivePirates = NewActivePirates;
    }

    public List<AllInfo.GamePirate> MyActivePirates;
    public List<Level> Levels;

    public int levelNum;
    public void LoadLevel(int newLevelNum)
    {
        levelNum = newLevelNum;
        SceneLoader.instance.LoadScene("Battle");
        //load scene
    }
}
