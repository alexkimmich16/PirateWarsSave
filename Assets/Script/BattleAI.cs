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

    private void Start()
    {
        CurrentHealth = MaxHealth;
        //navMeshAgent.updateRotation = false;
        SetStats();
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

    public void Attack()
    {
        Target.Damage(AttackDamage);
    }

    public void Damage(int DamageDone)
    {
        CurrentHealth -= DamageDone;
        if (CurrentHealth < 1)
        {
            Death();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void TurnTowards(Transform Objective)
    {
        Vector3 direction = (Objective.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnRotation);
    }



    public void MeleeActions()
    {
        if (Target != null)
        {
            Distance = Vector3.Distance(transform.position, Target.transform.position);
            if (Distance > MinDistanceObjective)
            {
                // too far
                Target = FindTarget();
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
    }

    public void RangedActions()
    {
        if (Target != null)
        {
            if (FoundLongPosition == false)
            {
                //find reasonable far position and stay
                
                Vector3 Objective = FindDistancePoint(BattleController.instance.Center, MinDistanceObjective);
                Debug.Log(Objective);
                navMeshAgent.SetDestination(Objective);
                Target = FindTarget();
                FoundLongPosition = true;
            }
            Distance = Vector3.Distance(transform.position, Target.transform.position);
            if (Distance < MinDistanceObjective)
            {
                TurnTowards(Target.transform);
                AttackCount();
            }
        }

        Vector3 FindDistancePoint(Vector3 center, float range)
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
    }

    void Update()
    {
        if (Target == null)
        {
            Target = FindTarget();
            //navMeshAgent.SetDestination(Target.transform.position);
        }

        
        if (pirate.pirateBase.Class == CharacterClass.Captain)
        {
            MeleeActions();
        }
        else if (pirate.pirateBase.Class == CharacterClass.MeleeDPS)
        {
            MeleeActions();
        }
        else if (pirate.pirateBase.Class == CharacterClass.QuarterMaster)
        {
            MeleeActions();
        }
        else if (pirate.pirateBase.Class == CharacterClass.RangeDPS)
        {
            RangedActions();
            //find point and stay, if transform moves out of distance chase, if comes toward me, stay and shoot
        }
        else if (pirate.pirateBase.Class == CharacterClass.Support)
        {
            RangedActions();
        }

        

        

        //LookDirection();
    }
    BattleAI FindTarget()
    {
        if (Friendly == false)
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
        
    }
}
