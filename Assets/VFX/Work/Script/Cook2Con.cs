using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook2Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject knife;
    public GameObject flask;
    public GameObject flask_sp;

    string state;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(){
        KnifeShow();
        state = "attack";
        m_Animator.SetTrigger(state);
    }
    public void Skill(){
        KnifeHide();
        state = "skill";
        m_Animator.SetTrigger(state);
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
    public void ShowFlask(){
        flask_sp.SetActive(true);
    }
    public void HideFlask(){
        flask_sp.SetActive(false);
        SkillEffect();
    }
    public void SkillEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(flask, flask_sp.transform.position, flask_sp.transform.rotation);
    }
}
