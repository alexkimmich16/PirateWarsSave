using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoGen : MonoBehaviour
{
    public List<float> ElementalDamage;
    public static InfoGen instance;
    private void Awake()
    {
        
        instance = this;
    }
    public float GetEqualValue(int Count)
    {
        return 100 / Count;
    }


    public void GenerateRandomPirate(int Level)
    {

    }


    public int GenerateOutcome(List<float> Chances)
    {
        float Chance = Random.Range(0, 100);
        float LastValue = 0;
        for (int i = 0; i < Chances.Count; i++)
        {
            LastValue += Chances[i];
            if (Chance > LastValue)
            {
                return i;
            }
        }
        Debug.LogError("No Value Found");
        return 100;
    }
}
