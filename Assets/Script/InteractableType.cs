﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Interactable
{
    Main = 0,
    Shop = 1,
    Trident = 2,

}
public class InteractableType : MonoBehaviour
{
    public Interactable type;
}
