using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook1Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject knife;
    public GameObject[] allies;
    public GameObject self_sp;
    public GameObject sheild;

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
    public void Idle(){
        KnifeShow();
        state = "idle";
        m_Animator.SetTrigger(state);
    }
    public void KnifeHide(){
        knife.SetActive(false);
    }
    public void KnifeShow(){
        knife.SetActive(true);
    }
   
    public void SkillEffect(){
        GameObject skill_effect;
        skill_effect = Instantiate(sheild, self_sp.transform.position, self_sp.transform.rotation);
        foreach (GameObject sp in allies)
            {
                skill_effect = Instantiate(sheild, sp.transform.position, sp.transform.rotation);
            }
        //attackEffect = Instantiate(flask, flask_sp.transform.position, flask_sp.transform.rotation);
    }
}
