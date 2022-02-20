using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Pirate", order = 1)]
public class PirateInfo : ScriptableObject
{
    //public string Name;
    public CharacterClass Class;
    public Sprite icon;
    public GameObject Prefab;
    public AllInfo.StatMultiplierBar BaseStats;
}