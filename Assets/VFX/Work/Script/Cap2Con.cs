using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap2Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject skill_effect;

    public static string cap2_state;
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
        cap2_state = "skill";
        m_Animator.SetTrigger(cap2_state);
        SkillEffect();
    }
    public void Attack(){
        cap2_state = "attack";
    }

    public void SkillEffect(){
        GameObject ob;
        ob = Instantiate(skill_effect, self_sp.transform.position, self_sp.transform.rotation);
    }
}
