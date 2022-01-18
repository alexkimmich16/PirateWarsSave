using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ArmorType
{
    Hat = 0,
    Armor = 1,
    Bracelet = 2,
    Ring = 3,
}
[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Armour", order = 3)]
public class ArmourInfo : ScriptableObject
{
    public string Name;
    public int ArmourIncrease;
    public Sprite icon;
    public ArmorType Armor;
    public int MinLevel;
}
