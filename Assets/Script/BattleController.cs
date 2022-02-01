using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    #region Singleton
    public static BattleController instance;
    void Awake() { instance = this; }
    #endregion

    public List<BattleAI> Friend;
    public List<BattleAI> Enemy;

    public List<Transform> Spawns;

    public float ChangeTargetTime = 2f;

    public void CheckResults()
    {
        if (Friend.Count == 0)
        {
            OnLose();
        }
        else if (Enemy.Count == 0)
        {
            OnWin();
        }
    }
    public void OnLose()
    {

    }
    public void OnWin()
    {

    }
    private void Start()
    {
        //SpawnFriendly(AllInfo.instance.);
    }
    public void SpawnFriendly(List<AllInfo.GamePirate> pirates)
    {
        //int SpawnNum = 0;
        for (int i = 0; i < pirates.Count; i++)
        {
            GameObject Spawned = Instantiate(pirates[i].pirateBase.Prefab, Spawns[i].position, Spawns[i].rotation);
            Spawned.GetComponent<BattleAI>().Friendly = true;
        }
    }
}
