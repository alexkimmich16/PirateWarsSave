using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook3Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject wp;
    public GameObject target_sp;
    public GameObject self_sp;
    public GameObject[] cook3_pati;
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
    }
    public void Idle(){
        state = "idle";
        m_Animator.SetTrigger(state);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    public void WpHide(){
        wp.SetActive(false);
    }
    public void WpShow(){
        wp.SetActive(true);
    }
    
    public void SkillEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(cook3_pati[0], self_sp.transform.position, self_sp.transform.rotation);
        attackEffect = Instantiate(cook3_pati[1], target_sp.transform.position, target_sp.transform.rotation);
        attackEffect = Instantiate(general_pati[0], self_sp.transform.position, self_sp.transform.rotation);
        attackEffect = Instantiate(general_pati[1], target_sp.transform.position, target_sp.transform.rotation);
    }
}
