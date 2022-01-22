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

    public float Timer;


    private float MinDistanceObjective = 0.3f;

    private float AttackTimer;
    private float AttackTime = 4;

    public int AttackDamage = 5;

    public int MaxHealth;
    private int CurrentHealth;

    public float RotationSpeed;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        //navMeshAgent.updateRotation = false;
        Vector2 a = Input.mousePosition;
    }

    public void MeleeMovement()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) < MinDistanceObjective)
        {
            AttackTimer += Time.deltaTime;
            if(AttackTimer > AttackTime)
            {
                Attack();
                AttackTimer = 0f;
            }
        }
        else
        {
            //move towards
        }
    }

    public void Attack()
    {
        Target.Damage(AttackDamage);
    }

    public void Damage(int DamageDone)
    {
        CurrentHealth -= DamageDone;
    }

    void Update()
    {
        
        
        if (pirate.pirateBase.Class == CharacterClass.Captain)
        {
            //
        }
        else if (pirate.pirateBase.Class == CharacterClass.MeleeDPS)
        {

        }
        else if (pirate.pirateBase.Class == CharacterClass.QuarterMaster)
        {
            //melee 
        }
        else if (pirate.pirateBase.Class == CharacterClass.RangeDPS)
        {
            //find point and stay, if transform moves out of distance chase, if comes toward me, stay and shoot
        }
        else if (pirate.pirateBase.Class == CharacterClass.Support)
        {

        }

        if (Timer > BattleController.ChangeTargetTime)
        {
            Target = FindTarget();
            navMeshAgent.SetDestination(Target.transform.position);
            Timer = 0f;
        }
        else
            Timer += Time.deltaTime;

        //LookDirection();
    }
    public void LookDirection()
    {
        Vector3 lookPos = Target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed);
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
