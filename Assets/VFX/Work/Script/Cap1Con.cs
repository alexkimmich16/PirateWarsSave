using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap1Con : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    public GameObject self_sp;
    public GameObject skill_effect;
    public static string cap1_state;
    public SkinnedMeshRenderer mesh;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            m_Animator.SetInteger("state", 0);
        }
        if (Input.GetKeyDown("1"))
        {
            m_Animator.SetInteger("state", 1);
        }
        if (Input.GetKeyDown("2"))
        {
            m_Animator.SetInteger("state", 2);
        }
        if (Input.GetKeyDown("3"))
        {
            //m_Animator.SetInteger("state", 3);
            Skill();
        }
        if (Input.GetKeyDown("4"))
        {
            m_Animator.SetInteger("state", 4);
        }
    }
    public void Skill(){
        //KnifeHide();
        cap1_state = "skill";
        m_Animator.SetTrigger(cap1_state);
    }

    public void SkillEffect(){
        GameObject ob;
        ob = Instantiate(skill_effect, self_sp.transform.position, self_sp.transform.rotation);
        ParticleSystem ps = ob.GetComponent<ParticleSystem>();
        var sh = ps.shape;
        sh.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
        sh.skinnedMeshRenderer = mesh;
    }
}
