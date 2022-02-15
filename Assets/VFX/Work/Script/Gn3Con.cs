using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gn3Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject[] gn3_pati;
    public GameObject skill_sp;
    public GameObject cnball_sp;
    public GameObject cannonball;
    public GameObject cannonLoad_sp;
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
        BallShow();
        state = "attack";
        m_Animator.SetTrigger(state);
    }
    public void Skill(){
        BallShow();
        state = "skill";
        m_Animator.SetTrigger(state);
        CannonLoadEffect();
    }
    public void Idle(){
        BallShow();
        state = "idle";
        m_Animator.SetTrigger(state);
    }
    public void BallHide(){
        //GameObject.Find("cannonball").SetActive(false);
        AttackEffect();
        cannonball.SetActive(false);
        print("event");
    }
    public void BallShow(){
        //GameObject.Find("cannonball").SetActive(true);
        cannonball.SetActive(true);
    }
    public void AttackEffect(){
        GameObject attackEffect;
        attackEffect = Instantiate(gn3_pati[0], cnball_sp.transform.position, cnball_sp.transform.rotation);
    }
    public void SkillEffect(){
        GameObject skillEffect;
        skillEffect = Instantiate(gn3_pati[1], skill_sp.transform.position, skill_sp.transform.rotation); 
    }
    public void CannonLoadEffect(){
        GameObject loadEffect;
        loadEffect = Instantiate(gn3_pati[2], cannonLoad_sp.transform.position, cannonLoad_sp.transform.rotation);
        StartCoroutine(Launch());
    }
    IEnumerator Launch()
    {
        yield return new WaitForSeconds(3.4f);
        SkillEffect();
    }
    
    
}
