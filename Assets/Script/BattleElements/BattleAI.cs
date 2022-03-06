﻿using System.Collections;
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

    public float MinDistanceObjective = 0.3f;

    public float AttackTimer;
    public float AttackTime = 4;

    public int AttackDamage = 5;

    [HideInInspector]
    public int MaxHealth;
    public int CurrentHealth;

    public float RotationSpeed;
    public float Distance;
    public float TurnRotation;
    private bool FoundLongPosition = false;
    private Vector3 ObjectivePoint;

    [Header("Animation")]
    private Animator animator;
    public string AttackString;
    public string DeathString;
    public string SkillString;
    
    public float DeathTime;
    private float DeathTimer = 0f;
    public bool Dying = false;

    public float AttackWait;
    private bool ThrowWaiting;
    private float ThrowWaitTimer;
    public KnifeControl knife;


    public float SpecialTime;
    private float SpecialTimer;

    public delegate void SomeCallbackFunction();
    // the event itself
    public event SomeCallbackFunction OnHit;

    private void Start()
    {
        BC = BattleController.instance;

        MaxHealth = pirate.Health;
        CurrentHealth = MaxHealth;
        
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    public void AttackCount()
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer > AttackTime)
        {
            Attack();
            AttackTimer = 0f;
        }
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
        if(ThrowWaiting == true)
        {
            ThrowWaitTimer += Time.deltaTime;
            SpecialTimer += Time.deltaTime;
            if (Melee(pirate.pirateBase.Class) == true)
            {
                if (ThrowWaitTimer > AttackWait)
                {
                    if (SpecialTimer > SpecialTime)
                    {
                        SkillController.instance.UseSkill(AllInfo.instance.GetCharNum(pirate.pirateBase), transform);
                        animator.Play(SkillString);
                    }
                    else
                    {
                        if (Melee(pirate.pirateBase.Class) == false)
                            knife.SendWeapon(Target.transform);
                        else
                            DoDamage();
                    }

                    ThrowWaiting = false;
                    
                    ThrowWaitTimer = 0f;
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
            if (Vector3.Distance(transform.position, ObjectivePoint) < 2f)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }

            if (Melee(pirate.pirateBase.Class) == true)
            {
                Distance = Vector3.Distance(transform.position, Target.transform.position);
                if (Distance > MinDistanceObjective)
                {
                    // too far
                    Target = FindTarget();
                    ObjectivePoint = Target.transform.position;
                    navMeshAgent.SetDestination(Target.transform.position);
                }
                else
                {
                    // close
                    navMeshAgent.SetDestination(transform.position);
                    Target = FindTarget();
                    TurnTowards(Target.transform);
                    AttackCount();
                }
            }
            else if (Melee(pirate.pirateBase.Class) == false)
            {
                //ranged
                if (FoundLongPosition == false)
                {
                    //find reasonable far position and stay
                    ObjectivePoint = FindDistancePoint(BattleController.instance.transform.position, MinDistanceObjective);
                    navMeshAgent.SetDestination(ObjectivePoint);
                    Target = FindTarget();
                    FoundLongPosition = true;
                }
                Distance = Vector3.Distance(transform.position, ObjectivePoint);

                //found position
                if (Distance < 0.1f)
                {
                    TurnTowards(Target.transform);
                    AttackCount();
                }
                //find point and stay, if transform moves out of distance chase, if comes toward me, stay and shoot
            }
        }
        else if (Target == null)
        {
            Target = FindTarget();
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
    public void DoDamage()
    {
        float CritAdd = AddCrit(out bool Crit);
        int DodgeEffect = Dodge();
        float DamageFloat = ((AttackDamage - (AttackDamage * BattleController.instance.ArmorEffect * Target.pirate.Armour)) * CritAdd) * DodgeEffect * BattleController.instance.DamageEffect;
        int FinalDMG = Mathf.RoundToInt(DamageFloat);
        Debug.Log("FinalDMG: " + DamageFloat + "  DodgeEffect: " + DodgeEffect + "  CritAdd: " + CritAdd + "  AttackDamage: " + AttackDamage + "  Armor: " + Target.pirate.Armour + "  AttackDamage: " + AttackDamage);
        PopupManager.instance.CreatePopup(Target.transform.position, FinalDMG, Crit);
        Target.Damage(FinalDMG);
        int Dodge()
        {
            int Roll = Random.Range(0, 100);
            if (Roll > Target.pirate.Dexterity)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        int ArmorRedue(float Damage)
        {
            int Armor = Target.pirate.Armour;
            float ArmorMultiplier = Armor / 100;
            float InverseDamage = 1 - ArmorMultiplier;
            float RealDamage = Damage * InverseDamage;
            return (int)RealDamage;
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
    public void Attack()
    {
        animator.Play(AttackString);
        ThrowWaiting = true;
       
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
    public void Damage(int DamageDone)
    {
        CurrentHealth -= DamageDone;
        if (CurrentHealth < 1)
        {
            Death();
        }
    }
}
