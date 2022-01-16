using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public float[] ChanceFail;
    public float[] ChanceNext;
    public int[] Chances;
    public int CurrentRank;
    public int Total;
    // Start is called before the first frame update
    void Start()
    {
        Chances = new int[5];
        for (int i = 0; i < 5; i++)
        {
            int num = GetAmount();
            if (num < 20)
            {
                Chances[i] = num;
            }
            
        }
        
        
    }
    public int GetAmount()
    {
        int Final = 0;
        int Total = 0;
        while (Final < 4)
        {
            float Chance = Random.Range(0f, 100f);
            //Debug.Log("currentRank: " + Final + "chance: " + Chance);
            if (Chance < ChanceFail[Final])
            {
                Final -= 1;
            }
            else if (Chance < ChanceNext[Final])
            {
                Final += 1;
            }
            if (Final == -1)
            {
                Final = 0;
            }
            Total += 1;
        }
        return Total;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
