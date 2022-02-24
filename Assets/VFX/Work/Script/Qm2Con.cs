using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qm2Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject skill_effect;
    public GameObject stun_effect;
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
        //KnifeHide();
        state = "skill";
        m_Animator.SetTrigger(state);
        SkillEffect();
    }

    public void SkillEffect(){
        GameObject ob;
        ob = Instantiate(skill_effect, self_sp.transform.position, self_sp.transform.rotation);
    }
    void Attack(){
        if(Cap2Con.cap2_state == "skill"){
            m_Animator.SetTrigger("stun");
            GameObject ob;
            ob = Instantiate(stun_effect, self_sp.transform.position, self_sp.transform.rotation);
        }
    }
}
