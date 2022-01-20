﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region Enums
public enum Element
{
    Air = 0,
    Water = 1,
    Fire = 2,
    Ice = 3,
    Light = 4,
    Dark = 5,
}
public enum CharacterClass
{
    MeleeDPS = 0,
    RangeDPS = 1,
    QuarterMaster = 2,
    Support = 3,
    Captain = 4,
}
public enum Rarity
{
    Grey = 0,
    Green = 1,
    Blue = 2,
    Purple = 3,
    Gold = 4,
}
#endregion
public class AllInfo : MonoBehaviour
{
    #region Singleton
    public static AllInfo instance;
    void Awake(){instance = this;}
    #endregion

    #region Classes
    //[System.Serializable]

    [System.Serializable]
    public class GamePirate
    {
        public string Name;
        public PirateInfo pirateBase;
        public int Health;
        public int Armour;
        public int AttackSpeed;
        public int Speed;
        public int Rank;
        public int Level;
        public float Experience;
        public List<GameEquipment> gameEquipment = new List<GameEquipment>();

        public Rarity rarity;
    }
    [System.Serializable]
    public class GameEquipment
    {
        public EquipmentInfo equipmentInfo;
        public Rarity rarity;
        //public int Num;
    }
    #endregion
    [Header("Currency")]
    public int Gold;
    public int Diamonds;
    public int ARG;

    [Header("StaticData")]
    public List<PirateInfo> Pirates;
    public List<EquipmentInfo> Equipment;

    [Header("GameData")]
    public List<GamePirate> GamePirates;
    public List<GameEquipment> GameEquipments;

    //applies to all 4 data types for simplicities sake
    
    public float ElementBonus(GamePirate Attack, GamePirate Defense)
    {
        bool IncreaseDamage = false;
        int ElementNum = System.Enum.GetValues(typeof(Element)).Length;
        if ((int)Attack.pirateBase.element == ElementNum && (int)Attack.pirateBase.element == 0)
            IncreaseDamage = true;
        else
            if ((int)Attack.pirateBase.element == (int)Defense.pirateBase.element + 1)
                IncreaseDamage = true;

        if (IncreaseDamage == true)
            return InfoGen.instance.ElementalDamage[Attack.Rank - 1];
        else
            return 0f;
    }

}
