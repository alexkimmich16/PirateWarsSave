using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/Pirate", order = 1)]
public class PirateInfo : ScriptableObject
{
    public string Name;
    public int Health;
    public int Armour;
    public int AttackSpeed;
    public int Speed;
    public Element element;
    public CharacterClass Class;
    public Sprite icon;
}