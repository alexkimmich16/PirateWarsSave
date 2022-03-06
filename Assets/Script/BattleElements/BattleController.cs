﻿using System.Collections;
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

    public List<Transform> EnemySpawns;
    public List<Transform> FriendlySpawns;

    public float ChangeTargetTime = 2f;

    [Range(0, 1)]
    public float ArmorEffect = 0.002f;

    [Range(0, 10)]
    public float DamageEffect = 0.002f;
    public GameObject LoseOBJ;

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
        LoseOBJ.SetActive(true);
    }
    public void OnWin()
    {

    }
    private void Start()
    {
        SpawnFriendly(MapLevels.instance.MyActivePirates);
        SpawnEnemy(MapLevels.instance.Levels[MapLevels.instance.levelNum].Pirates);
    }

    public void SpawnFriendly(List<AllInfo.GamePirate> pirates)
    {
        for (int i = 0; i < pirates.Count; i++)
        {
            GameObject Spawned = Instantiate(pirates[i].pirateBase.Prefab, FriendlySpawns[i].position, FriendlySpawns[i].rotation);
            Spawned.name = pirates[i].pirateBase.name;
            Spawned.GetComponent<BattleAI>().Friendly = true;
            Spawned.GetComponent<BattleAI>().pirate = pirates[i];
            Friend.Add(Spawned.GetComponent<BattleAI>());
            
            Spawned.GetComponent<BattleAI>().pirate.CheckExperience();
        }
    }

    public void SpawnEnemy(List<AllInfo.GamePirate> pirates)
    {
        for (int i = 0; i < pirates.Count; i++)
        {
            GameObject Spawned = Instantiate(pirates[i].pirateBase.Prefab, EnemySpawns[i].position, EnemySpawns[i].rotation);
            Spawned.name = pirates[i].pirateBase.name;
            Spawned.GetComponent<BattleAI>().Friendly = false;
            Enemy.Add(Spawned.GetComponent<BattleAI>());
            Spawned.GetComponent<BattleAI>().pirate.Experience = pirates[i].Experience;
            Spawned.GetComponent<BattleAI>().pirate.CheckExperience();
        }
    }
}
