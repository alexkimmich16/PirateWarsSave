using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctro : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gn1_pati;
    public GameObject gun_sp_l;
    public GameObject gun_sp_r;
    public GameObject bullet_sp_l;
    public GameObject bullet_sp_r;
    public GameObject skill_sp;
    GameObject  skillEffect;
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    string state;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        //m_ClipName = m_CurrentClipInfo[0].clip.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run(){
        state = "run";
        m_Animator.SetTrigger(state);
        skillEnd();
    }
    public void Walk(){
        state = "walk";
        m_Animator.SetTrigger(state);
        skillEnd();
    }
    public void Attack(){
        state = "attack";
        m_Animator.SetTrigger(state);
        skillEnd();
    }
    public void Skill(){
        state = "skill";
        m_Animator.SetTrigger(state);
        Destroy (skillEffect);
        skillStart();
    }
    public void Return(){
        state = "return";
        m_Animator.SetTrigger(state);
        skillEnd();
    }
    public void attackRight( int flg ){
        GameObject thisEffect;
        thisEffect = Instantiate(gn1_pati[0], gun_sp_r.transform.position, gun_sp_r.transform.rotation);
        GameObject dripEffect;
        dripEffect = Instantiate(gn1_pati[1], bullet_sp_r.transform.position, bullet_sp_r.transform.rotation);
    }
    public void attackLeft(int flg){
        GameObject thisEffect;
        thisEffect = Instantiate(gn1_pati[0], gun_sp_l.transform.position, gun_sp_l.transform.rotation);
        GameObject dripEffect;
        dripEffect = Instantiate(gn1_pati[1], bullet_sp_l.transform.position, bullet_sp_l.transform.rotation);
    }
    public void skillStart(){
        skillEffect = Instantiate(gn1_pati[2], skill_sp.transform.position, skill_sp.transform.rotation);
    }
    public void skillEnd(){
        Destroy (skillEffect);
    }

}
