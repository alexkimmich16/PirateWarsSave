using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook4Con : MonoBehaviour
{
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject[] allies;
    public GameObject[] general_pati;

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
        SkillEffect();
    }
    public void Idle(){
        state = "idle";
        m_Animator.SetTrigger(state);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    public void SkillEffect(){
        GameObject skill_effect;
        skill_effect = Instantiate(general_pati[0], self_sp.transform.position, self_sp.transform.rotation);
        skill_effect = Instantiate(general_pati[1], self_sp.transform.position, self_sp.transform.rotation);
        foreach (GameObject sp in allies)
        {
            skill_effect = Instantiate(general_pati[1], sp.transform.position, sp.transform.rotation);
        }
    }
}
