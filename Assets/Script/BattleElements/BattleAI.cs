using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public AllInfo.GamePirate pirate;
    [HideInInspector]
    public bool Friendly;


    [HideInInspector]
    public BattleController BC;
    [HideInInspector]
    public BattleAI Target;

    public static float MinDistanceObjective = 0.3f;

    

    public int AttackDamage = 5;

    [HideInInspector]
    public int MaxHealth;
    public int CurrentHealth;

    public float RotationSpeed;
    public float Distance;
    public float TurnRotation;
    private bool FoundPosition = false;
    private Vector3 ObjectivePoint;

    [Header("Animation")]
    private Animator animator;
    public string AttackString;
    public string DeathString;
    public string SkillString;
    
    public float DeathTime;
    private float DeathTimer = 0f;
    public bool Dying = false;
    public bool Attacking()
    {
        if (AttackTimer > AttackTime)
            return true;
        else
            return false;
    }
    [Header("Attack")]
    
    public float AttackTime = 4;
    public float AttackTimer;
    public float AttackWait;
    //private bool Attacking;
    //private float ThrowWaitTimer;
    public KnifeControl knife;

    public float SpecialTime;
    private float SpecialTimer;

    public delegate void SomeCallbackFunction();
    // the event itself
    public event SomeCallbackFunction OnHit;

    public bool SkillActive;
    private void Start()
    {
        BC = BattleController.instance;

        MaxHealth = pirate.Health;
        CurrentHealth = MaxHealth;
        
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    public void TurnTowards(Transform Objective)
    {
        Vector3 direction = (Objective.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnRotation);
    }
    public Vector3 FindDistancePoint(Vector3 center, float range)
    {
        for (int i = 0; i < 500; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * 15;
            Vector3 RandomAdjusted = new Vector3(randomPoint.x, 0, randomPoint.z);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(RandomAdjusted, out hit, 1.0f, NavMesh.AllAreas))
            {
                if (Vector3.Distance(RandomAdjusted, Target.transform.position) > MinDistanceObjective)
                {
                    return hit.position;
                }
            }
        }
        return Vector3.zero;
    }
    void Update()
    {
        if (Attacking() == true)
        {
            AttackTimer = 0f;
            if (Melee(pirate.pirateBase.Class) == true)
            {
                if (SpecialTimer > SpecialTime)
                {
                    StartCoroutine(DoSkill());
                }
                else
                {
                    StartCoroutine(DoAttack());
                }
            }
            else
            {
                if (SpecialTimer > SpecialTime)
                {
                    StartCoroutine(DoSkill());
                }
                else
                {
                    StartCoroutine(DoAttack());
                }
            }
        }        
        if (DeathTimer > DeathTime)
        {
            Destroy(gameObject);
        }
        else if (Dying == true)
        {
            DeathTimer += Time.deltaTime;
        }
        else if (Target != null && Target.Dying == false)
        {
            //distance smaller than whatever
            ObjectivePoint = GetObjective();
            float Distance = Vector3.Distance(transform.position, ObjectivePoint);
            if (Distance < MinDistanceObjective)
            {
                animator.SetBool("Moving", false);
                SpecialTimer += Time.deltaTime;
                AttackTimer += Time.deltaTime;

                TurnTowards(Target.transform);
                navMeshAgent.SetDestination(transform.position);
                //AttackCount();
            }
            else
            {
                animator.SetBool("Moving", true);
                navMeshAgent.SetDestination(Target.transform.position);
            }
            /*
            if (Melee(pirate.pirateBase.Class) == true)
            {
                
            }
            else if (Melee(pirate.pirateBase.Class) == false)
            {
                //ranged
                if (FoundPosition == false)
                {
                    //find reasonable far position and stay
                    ObjectivePoint = FindDistancePoint(BattleController.instance.transform.position, MinDistanceObjective);
                    navMeshAgent.SetDestination(ObjectivePoint);
                    FoundPosition = true;
                }
            }
            */
        }
        else if (Target == null)
        {
            Target = FindTarget();
        }
        Vector3 GetObjective()
        {
            if (Melee(pirate.pirateBase.Class) == true)
            {
                return Target.transform.position;
            }
            else
            {
                //ranged
                if (FoundPosition == false)
                {
                    //find reasonable far position and stay
                    FoundPosition = true;
                    return FindDistancePoint(BattleController.instance.transform.position, MinDistanceObjective);

                }
                else
                {
                    return ObjectivePoint;
                }
            }
        }
    }
    BattleAI FindTarget()
    {
        if (Friendly == false)
        {
            if (BC.Friend.Count > 0)
            {
                float Distance = 1000f;
                int Num = 0;
                for (int i = 0; i < BC.Friend.Count; i++)
                {
                    if (Vector3.Distance(transform.position, BC.Friend[i].transform.position) < Distance)
                    {
                        Num = i;
                        Distance = Vector3.Distance(transform.position, BC.Friend[i].transform.position);
                    }

                }
                return BC.Friend[Num];
            }
            else
                return null;

            
        }
        else
        {
            if (BC.Enemy.Count > 0)
            {
                float Distance = 1000f;
                int Num = 0;
                for (int i = 0; i < BC.Enemy.Count; i++)
                {
                    if (Vector3.Distance(transform.position, BC.Enemy[i].transform.position) < Distance)
                    {
                        Num = i;
                        Distance = Vector3.Distance(transform.position, BC.Enemy[i].transform.position);
                    }

                }
                return BC.Enemy[Num];
            }
            else
                return null;
        }
        
    }
   
    public void Death()
    {
        Dying = true;
        animator.Play(DeathString);

        if (Friendly == true)
        {
            BC.Friend.Remove(this);
        }
        else
        {
            BC.Enemy.Remove(this);
        }

        BC.CheckResults();
    }
    public bool Melee(CharacterClass Class)
    {
        if (Class == CharacterClass.Captain || Class == CharacterClass.MeleeDPS || Class == CharacterClass.QuarterMaster || Class == CharacterClass.Support)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public IEnumerator OverTimeDamage(int Damage, float Interval, float Range)
    {
        float Current = 0;
        
        while (Current < 3)
        {
            SkillController.instance.DealAroundDamage(transform, transform.GetComponent<BattleAI>().Friendly, Range, Damage);
            Current += Interval;
            yield return new WaitForSeconds(Interval);
        }

        //int TotalDamages =  (int)Mathf.Floor(3 / Interval);
    }

    public void DoDamage()
    {
        float CritAdd = AddCrit(out bool Crit);
        float TotalDamage = AttackDamage * CritAdd * BattleController.instance.DamageEffect * SkillMultiplier();
        int FinalDMG = Mathf.RoundToInt(TotalDamage);
        //Debug.Log("FinalDMG: " + TotalDamage + "  CritAdd: " + CritAdd + "  AttackDamage: " + AttackDamage);
        Target.Damage(FinalDMG, Crit);
        
        float SkillMultiplier()
        {
            return 1;
        }
        
        
        int AddCrit(out bool IsCrit)
        {
            int Roll = Random.Range(0, 100);
            //Debug.Log("Roll: " + Roll + "  pirate.CritPercent: " + pirate.CritPercent);
            if (Roll < pirate.CritPercent)
            {
                IsCrit = true;
                return pirate.CritDamage;
            }
            else
            {
                IsCrit = false;
                return 1;
            }
        }
    }

    public void Damage(int DamageDone, bool Crit)
    {
        float TrueDamage = DamageDone * ArmorReduce() * Dodge(out bool DodgeTrue) * BattleController.instance.DamageEffect * SkillMultiplier();
        int DamageInt = Mathf.RoundToInt(TrueDamage);
        Debug.Log("Final: " + TrueDamage + "DamageDone: " + DamageDone + "ArmorReduce(): " + ArmorReduce() + "DodgeTrue: " + DodgeTrue + "SkillMultiplier(): " + SkillMultiplier() + DodgeTrue + "  DamageEffect: " + BattleController.instance.DamageEffect);
        CurrentHealth -= DamageDone;

        if (DodgeTrue)
            PopupManager.instance.CreateStringPopup(Target.transform.position, "Dodge", Crit);
        else
            PopupManager.instance.CreatePopup(Target.transform.position, DamageInt, Crit);



        if (CurrentHealth < 1)
        {
            Death();
        }



        float ArmorReduce()
        {
            return 1 - (BattleController.instance.ArmorEffect * pirate.Armour);
        }
        int Dodge(out bool IsDodge)
        {
            int Roll = Random.Range(0, 100);
            if (Roll > pirate.Dexterity)
            {
                IsDodge = false;
                return 1;
            }
            else
            {
                IsDodge = true;
                return 0;
            }
        }
        float SkillMultiplier()
        {
            if (AllInfo.instance.GetCharNum(pirate.pirateBase) == 0 && gameObject.GetComponent<BattleAI>().SkillActive == true)
            {
                return 0.45f;
            }
            return 1;
        }
    }

    public IEnumerator DoSkill()
    {
        SpecialTimer = 0;
        animator.Play(SkillString);
        yield return new WaitForSeconds(SkillController.instance.SkillWaitTime);
        SkillController.instance.UseSkill(AllInfo.instance.GetCharNum(pirate.pirateBase), transform);
    }
    public IEnumerator DoAttack()
    {
        animator.Play(AttackString);

        yield return new WaitForSeconds(SkillController.instance.SkillWaitTime);

        if (Melee(pirate.pirateBase.Class) == false)
        {
            knife.SendWeapon(Target.transform);
            yield return new WaitForSeconds(SkillController.instance.SkillWaitTime);
        }
        else
        {
            DoDamage();
            StopCoroutine(DoAttack());
        }
    }
}
