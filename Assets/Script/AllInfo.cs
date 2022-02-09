﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region Enums
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
        public int Damage;
        public int Armour;
        public int CritPercent;
        public int CritDamage;
        public int Intellect;
        public int Dexterity;

        public int Rank;
        public int Level;
        public float Experience;
        public List<GameEquipment> gameEquipment = new List<GameEquipment>();

        public Rarity rarity;

        public int NumInList;

        public void AddExperience(int ExperienceAdd)
        {
            Experience += ExperienceAdd;
            CheckExperience();
            
            
            
            //if crosses threshold
        }
        public void CheckExperience()
        {
            if (Experience >= AllInfo.instance.LevelCaps[Rank].Max[Level])
            {
                
                Experience -= AllInfo.instance.LevelCaps[Rank].Max[Level];
                Level += 1;
                if (Level > AllInfo.instance.LevelCaps[Rank].Max.Count - 1)
                {
                    Rank += 1;
                    Level = 0;
                    Rankup();
                }
                else
                {
                    Levelup();
                }
            }
        }
        public void Levelup()
        {
            float Multiplier = 100f;
            Health += (int)Multiplier;
            Damage += (int)Multiplier;
            Armour += (int)Multiplier;
            CritPercent += (int)Multiplier;
            CritDamage += (int)Multiplier;
            Intellect += (int)Multiplier;
            Dexterity += (int)Multiplier;
        }
        public void Rankup()
        {
            float Multiplier = 2f;
            Health += (int)Multiplier;
            Damage += (int)Multiplier;
            Armour += (int)Multiplier;
            CritPercent += (int)Multiplier;
            CritDamage += (int)Multiplier;
            Intellect += (int)Multiplier;
            Dexterity += (int)Multiplier;
        }
    }
    [System.Serializable]
    public class GameEquipment
    {
        public EquipmentInfo equipmentInfo;
        public Rarity rarity;
        public int Health;
        public int Damage;
        public int Armour;
        public int CritPercent;
        public int CritDamage;
        public int Intellect;
        public int Dexterity;

        public int Rank;
        public int Level;
        public float Experience;
        public void AddExperience(int ExperienceAdd)
        {
            Experience += ExperienceAdd;
            //if crosses threshold
        }
    }
    [System.Serializable]
    public class StatMultiplier
    {
        public string Name;
        public int Health;
        public int Damage;
        public int Armour;
        public int CritPercent;
        public int CritDamage;
        public int Intellect;
        public int Dexterity;
    }

    [System.Serializable]
    public class Rank
    {
        public string RankName;
        public int RankMultiplier;
        public List<int> Max;
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
    public List<StatMultiplier> StatMultipliers;

    [Header("LevelData")]
    public List<Rank> LevelCaps;
    public void AddToCharacter(int Num)
    {
        GamePirates[0].AddExperience(Num);
    }

    public void RecieveCurrency(int gold, int diamonds, int arg)
    {
        Gold = gold;
        Diamonds = diamonds;
        ARG = arg;
    }
    public void RecieveGameData(List<GamePirate> gamePirates, List<GameEquipment> gameEquipments)
    {
        GamePirates = gamePirates;
        GameEquipments = gameEquipments;
    }
    private void Start()
    {
        for (int i = 0; i < GamePirates.Count; i++)
        {
            GamePirates[i].NumInList = i;
        }
    }
    public int EquipmentNum(GameEquipment Equipment)
    {
        for (int i = 0; i < GameEquipments.Count; i++)
        {
            if (GameEquipments[i] == Equipment)
            {
                return i;
            }
        }
        return 1000;
    }
    public int PirateNum(GamePirate Pirate)
    {
        for (int i = 0; i < GamePirates.Count; i++)
        {
            if (GamePirates[i] == Pirate)
            {
                return i;
            }
        }
        return 1000;
    }


    //applies to all 4 data types for simplicities sake
    /*
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
    */


}
