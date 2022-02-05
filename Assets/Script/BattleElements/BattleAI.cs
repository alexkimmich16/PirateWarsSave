using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AllInfo.GamePirate pirate;
    public bool Friendly;
    public BattleController BC;
    public BattleAI Target;

    public float MinDistanceObjective = 0.3f;

    public float AttackTimer;
    public float AttackTime = 4;

    public int AttackDamage = 5;

    public int MaxHealth;
    public int CurrentHealth;

    public float RotationSpeed;
    public float Distance;
    public float TurnRotation;
    private bool FoundLongPosition = false;
    private Vector3 ObjectivePoint;

    [Header("Animation")]
    public Animator animator;
    public string AttackString;
    public string DeathString;
    public float DeathTime;
    private float DeathTimer = 0f;
    public bool Dying = false;

    public float ThrowWaitTime;
    private bool ThrowWaiting;
    private float ThrowWaitTimer;
    public KnifeControl knife;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        SetStats();
        BC = BattleController.instance;
    }
    public void SetStats()
    {
        //health
        //max health
        //speed
        //attack
        //damage
        //armor
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
            if (ThrowWaitTimer > ThrowWaitTime)
            {
                ThrowWaiting = false;
                if (knife != null)
                {
                    ThrowWaitTimer = 0f;
                    knife.SendWeapon(Target.transform);
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
        int CritAdd = AddCrit();
        int TotalDamage = AttackDamage + CritAdd;
        if (CritAdd != 0)
            PopupManager.instance.CreatePopup(Target.transform.position, TotalDamage, true);
        else
            PopupManager.instance.CreatePopup(Target.transform.position, TotalDamage, false);

        Target.Damage(TotalDamage);

        int AddCrit()
        {
            int Roll = Random.Range(0, 100);
            if (Roll < pirate.CritPercent)
            {
                return pirate.CritDamage;
            }
            else
            {
                return 0;
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
        if (Class == CharacterClass.Captain || Class == CharacterClass.MeleeDPS || Class == CharacterClass.QuarterMaster)
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
        

        int ActualDamage = DamageDone / (1 + pirate.Armour / DamageDone);
        CurrentHealth -= ActualDamage;

        if (CurrentHealth < 1)
        {
            Death();
        }
    }
}
