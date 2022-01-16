using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Weapon", order = 2)]
public class WeaponInfo : ScriptableObject
{
    public string Name;
    public int DamageIncrease;
    public Sprite icon;
}
