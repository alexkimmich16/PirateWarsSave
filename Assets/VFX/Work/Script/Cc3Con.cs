using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cc3Con : MonoBehaviour
{
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject weapen_trail;
    public GameObject[] cc3_pati;
    string state;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        weapen_trail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "skill"){
            gameObject.transform.Translate(0, 0, 1.5f * Time.deltaTime);
        }
    }

    public void Skill(){
        state = "skill";
        StartCoroutine(StopMove());
        m_Animator.SetTrigger(state);
        weapen_trail.SetActive(true);
    }
    public void SkillEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(cc3_pati[0], self_sp.transform.position, self_sp.transform.rotation);
    }
    IEnumerator StopMove(){
        yield return new WaitForSeconds(6f);
        state = "attack";
        m_Animator.SetTrigger(state);
        weapen_trail.SetActive(false);
    }
}
