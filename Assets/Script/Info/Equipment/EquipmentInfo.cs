using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType
{
    Hat = 0,
    Gloves = 1,
    Armor = 2,
    Boots = 3,
    Weapon = 4,
}
[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Equipment", order = 3)]
public class EquipmentInfo : ScriptableObject
{
    //public string Name;
    public int ArmourIncrease;
    public int DamageIncrease;
    public Sprite icon;
    public EquipmentType type;
    public int MinLevel;
}
