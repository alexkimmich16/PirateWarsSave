using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Interactable
{
    Main = 0,
    Shop = 1,
    Trident = 2,
    Battle = 3,
    Statue = 4,
}
public class InteractableType : MonoBehaviour
{
    public Interactable type;
}
