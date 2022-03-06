using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public GameObject Trail;
    public GameObject Effect;
    public GameObject Self;
    public float TrailTime;

    


    void Start()
    {
        gameObject.GetComponent<BattleAI>().OnHit += OnHit;
    }

    public void OnHit()
    {
        if(Trail.activeSelf == true)
        {
            //do effect
            GameObject attackEffect = Instantiate(Effect, Self.transform.position, Self.transform.rotation);
        }
    }

    public void SetTrail()
    {
        StartCoroutine(StopMove());
        Trail.SetActive(true);

    }
    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(TrailTime);
        Trail.SetActive(false);
    }
}
