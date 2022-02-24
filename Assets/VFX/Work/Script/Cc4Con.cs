using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cc4Con : MonoBehaviour
{
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject[] cc4_pati;
    string state;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Skill(){
        state = "skill";
        m_Animator.SetTrigger(state);
    }
    public void SkillEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(cc4_pati[0], self_sp.transform.position, self_sp.transform.rotation);
    }
}
