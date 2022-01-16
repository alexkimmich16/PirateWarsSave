using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Armour", order = 3)]
public class ArmourInfo : ScriptableObject
{
    public string Name;
    public int ArmourIncrease;
    public Sprite icon;
}
