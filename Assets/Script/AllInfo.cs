using System.Collections;
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
            bool Reached = false;
            while(Reached == false)
            {
                Debug.Log(Level);
                if (Experience >= AllInfo.instance.LevelCaps[Rank].Max[Level])
                {
                    Experience -= AllInfo.instance.LevelCaps[Rank].Max[Level];
                    if (Level + 1 > AllInfo.instance.LevelCaps[Rank].Max.Count - 1)
                    {
                        Level = 0;
                        Rankup();
                    }
                    else
                    {
                        Levelup();
                    }
                }
                else
                {
                    Reached = true;
                }
            }
            
        }
        public void Levelup()
        {
            Level += 1;
            Debug.Log("levelup");
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
            Rank += 1;
            float Multiplier = 2f;
            Health += (int)Multiplier;
            Damage += (int)Multiplier;
            Armour += (int)Multiplier;
            CritPercent += (int)Multiplier;
            CritDamage += (int)Multiplier;
            Intellect += (int)Multiplier;
            Dexterity += (int)Multiplier;
            Debug.Log("rank");
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
    public class StatMultiplierFloat
    {
        public string Name;
        public float Health;
        public float Damage;
        public float Armour;
        public float CritPercent;
        public float CritDamage;
        public float Intellect;
        public float Dexterity;
    }

    [System.Serializable]
    public class Rank
    {
        public string RankName;
        public int RankMultiplier;
        public List<int> Max;
        public List<MaxMin> Base;
    }

    [System.Serializable]
    public class MaxMin
    {
        public int Max;
        public int Min;
    }
    #endregion
    [Header("Bases")]
    public List<PirateInfo> PirateBases;

    [Header("Currency")]
    public int Gold;
    public int Diamonds;
    public int ARG;
    public StatMultiplier Base;

    

    [Header("GameData")]
    public List<GamePirate> GamePirates;
    public List<GameEquipment> GameEquipments;
    

    [Header("LevelData")]
    public List<Rank> LevelCaps;
    public List<StatMultiplierFloat> ClassMultiplier;
    public List<StatMultiplierFloat> RankMultipliers;
    public List<StatMultiplierFloat> RarityMultipliers;
    //public List<int> LevelFuseEXP;
    [Range(0, 1)]
    public float FusePercentAdd;

    public int GetTotalEXP(bool IsPirate, int ListNum)
    {
        int Total = 0;
        int TotalLevels;
        if (IsPirate == true)
        {
            GamePirate pirate = GamePirates[ListNum];
            TotalLevels = (pirate.Rank * 10) + pirate.Level;
        }
        else
        {
            GameEquipment pirate = GameEquipments[ListNum];
            TotalLevels = (pirate.Rank * 10) + pirate.Level;
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if ((i * 10) + j < TotalLevels)
                {
                    Total += LevelCaps[i].Max[j];
                }
            }
        }
        Debug.Log(Total);
        return Total;
    }
    public void GenerateRandomCharacter(int Chance)
    {
        GamePirate NewPirate = new GamePirate();
        int RandomNum = Random.Range(0, PirateBases.Count);
        NewPirate.pirateBase = PirateBases[RandomNum];

        int RandomRarity = Random.Range(0, 5);
        NewPirate.rarity = (Rarity)RandomRarity;

        int Class = (int)NewPirate.pirateBase.Class;

        NewPirate.Health = (int)(ClassMultiplier[Class].Health * RankMultipliers[0].Health * RarityMultipliers[RandomRarity].Health);
        NewPirate.Damage = (int)(ClassMultiplier[Class].Damage * RankMultipliers[0].Damage * RarityMultipliers[RandomRarity].Damage);
        NewPirate.Armour = (int)(ClassMultiplier[Class].Armour * RankMultipliers[0].Armour * RarityMultipliers[RandomRarity].Armour);
        NewPirate.CritPercent = (int)(ClassMultiplier[Class].CritPercent * RankMultipliers[0].CritPercent * RarityMultipliers[RandomRarity].CritPercent);
        NewPirate.CritDamage = (int)(ClassMultiplier[Class].CritDamage * RankMultipliers[0].CritDamage * RarityMultipliers[RandomRarity].CritDamage);
        NewPirate.Intellect = (int)(ClassMultiplier[Class].Intellect * RankMultipliers[0].Intellect * RarityMultipliers[RandomRarity].Intellect);
        NewPirate.Dexterity = (int)(ClassMultiplier[Class].Dexterity * RankMultipliers[0].Dexterity * RarityMultipliers[RandomRarity].Dexterity);
    }
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
        SetListNum();
    }
    public void SetListNum()
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
