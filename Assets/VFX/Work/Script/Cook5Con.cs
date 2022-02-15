using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook5Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject pan;
    public GameObject target_sp;
    public GameObject[] cook5_pati;

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
    public void PanHide(){
        pan.SetActive(false);
    }
    public void PanShow(){
        pan.SetActive(true);
    }
    
    public void SkillEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(cook5_pati[0], target_sp.transform.position, Quaternion.identity);
    }
}
