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

    public Vector3 Center;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
