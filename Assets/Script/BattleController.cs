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

    public float ChangeTargetTime = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
}
