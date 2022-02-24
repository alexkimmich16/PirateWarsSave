using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cc1Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject skill_effect;
    public static string cc1_state;
    public GameObject spear_effect;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        SpearEffectHide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skill(){
        //KnifeHide();
        cc1_state = "skill";
        m_Animator.SetTrigger(cc1_state);
        spear_effect.SetActive(true);

    }
    public void SpearEffectHide(){
        spear_effect.SetActive(false);
    }

    public void SkillEffect(){
        GameObject ob;
        ob = Instantiate(skill_effect, self_sp.transform.position, self_sp.transform.rotation);
    }
}
