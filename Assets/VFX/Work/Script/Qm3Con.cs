using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qm3Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject target_sp;
    public GameObject[] skill_effect;
    public GameObject cook5;
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

    public void SkillEffect(){
       
        GameObject ob;
        ob = Instantiate(skill_effect[0], self_sp.transform.position, self_sp.transform.rotation);
        GameObject ob1;
        ob1 = Instantiate(skill_effect[1], self_sp.transform.position, self_sp.transform.rotation);
        ob1.GetComponentInChildren<Animator>().SetTrigger("before");
        StartCoroutine(TargerEffect("check"));
        StartCoroutine(TargerEffect("show"));
        StartCoroutine(TargerEffect("cook5_stun"));
        SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer r in renderers){
            r.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        print(GetComponent<Renderer>());
    }
    IEnumerator TargerEffect(string type)
    {
        if(type == "check")
        {
            yield return new WaitForSeconds(0.5f);
            GameObject ob;
            ob = Instantiate(skill_effect[2], target_sp.transform.position, target_sp.transform.rotation);
            ob.GetComponentInChildren<Animator>().SetTrigger("after");
        }
        if(type == "show")
        {
            yield return new WaitForSeconds(1.5f);
            SkinnedMeshRenderer[] renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach(SkinnedMeshRenderer r in renderers){
                r.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
        if(type == "cook5_stun"){
            yield return new WaitForSeconds(2f);
            cook5.GetComponent<Cook5Con>().Stun();
        }
    }    
}
