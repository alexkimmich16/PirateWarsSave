using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenuManager : MonoBehaviour
{
    public void LoadLevel(int newLevelNum)
    {
        MapLevels.instance.LoadLevel(newLevelNum);
    }
}
