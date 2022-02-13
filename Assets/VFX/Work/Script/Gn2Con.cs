using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gn2Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject[] gn2_pati;
    public GameObject gun_sp;
    public GameObject skill_sp;
    public GameObject[] target_sp;
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
        state = "attack";
        m_Animator.SetTrigger(state);
    }

    public void Skill7(){
        state = "skill7";
        m_Animator.SetTrigger(state);
        GameObject targetEffect;
        Instantiate(gn2_pati[1], target_sp[1].transform.position, target_sp[1].transform.rotation);
    }
    public void Launch(){
        GameObject thisEffect;
        thisEffect = Instantiate(gn2_pati[0], gun_sp.transform.position, gun_sp.transform.rotation);
    }
}
