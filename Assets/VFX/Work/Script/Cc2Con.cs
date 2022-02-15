using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cc2Con : MonoBehaviour
{
    Animator m_Animator;
    public GameObject target_sp;
    public GameObject[] cc2_pati;
    string state;
    void Start()
    {
        //m_Animator = gameObject.GetComponent<Animator>();
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
        attackEffect = Instantiate(cc2_pati[0], target_sp.transform.position, target_sp.transform.rotation);
    }
}
