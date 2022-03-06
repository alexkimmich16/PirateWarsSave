using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    #region Singleton
    public static SkillController instance;
    void Awake() { instance = this; }
    #endregion
    //create skills prefabs

    //code damage

    public List<GameObject> Skills;

    public void UseSkill(int SkillNum, Transform pos)
    {
        Instantiate(Skills[SkillNum], pos.position, pos.rotation);
        if (SkillNum == 0)
        {
            GameObject self_sp = pos.Find("cap1_sp").gameObject;
            GameObject ob = Instantiate(Skills[0], self_sp.transform.position, self_sp.transform.rotation);
            ParticleSystem ps = ob.GetComponent<ParticleSystem>();
            var sh = ps.shape;
            sh.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
            sh.skinnedMeshRenderer = pos.Find("Capt1TA_Body").GetComponent<SkinnedMeshRenderer>();
        }
        else if (SkillNum == 1)
        {
            //get self
            //ob = Instantiate(skill_effect, self_sp.transform.position, self_sp.transform.rotation);
        }
        else if (SkillNum == 2)
        {
            GameObject self_sp = pos.Find("cc1_sp").gameObject;
            GameObject ob = Instantiate(Skills[2], self_sp.transform.position, self_sp.transform.rotation);
            //spear_effect.SetActive(false);
        }
        else if (SkillNum == 3)
        {
           //find real
            //GameObject self_sp = pos.Find("cc2_sp").gameObject;
            //GameObject ob = Instantiate(Skills[3], self_sp.transform.position, self_sp.transform.rotation);
        }
        else if (SkillNum == 4)
        {
            GameObject self_sp = pos.Find("cc3_sp").gameObject;
            pos.GetComponent<TrailManager>().SetTrail();
        }
        else if (SkillNum == 5)
        {
            ///cc4
            GameObject self_sp = pos.Find("cc4_sp").gameObject;
            GameObject ob = Instantiate(Skills[5], self_sp.transform.position, self_sp.transform.rotation);
        }
        else if (SkillNum == 6)
        {
            //find this
            //GameObject self_sp = pos.Find("cc4_sp").gameObject;
            //GameObject skill_effect = Instantiate(Skills[6], self_sp.transform.position, self_sp.transform.rotation);
            for (int i = 0; i < BattleController.instance.Friend.Count; i++)
            {
                GameObject skill_effectFriend = Instantiate(Skills[6], BattleController.instance.Friend[i].transform.position, BattleController.instance.Friend[i].transform.rotation);
            }
        }
        else if (SkillNum == 7)
        {
            //hide knife
            //show flask
            //GameObject self_sp = pos.Find("cc4_sp").gameObject;
            //GameObject Flask = Instantiate(Skills[7], flask_sp.transform.position, flask_sp.transform.rotation);
        }
    }
}
