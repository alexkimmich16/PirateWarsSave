using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    None = 0,
    Sending = 1,
    Returning = 2,
}
public class KnifeControl : MonoBehaviour
{
    public State CurrentState;
    public float SendSpeed;
    public float ReturnSpeed;
    public Transform Objective;
    private float MinDistance = 0.3f;
    public Transform HandPos;
    private Vector3 SentPosition;

    public GameObject AnimationKnife;
    public GameObject MeshAnimationKnife;
    public GameObject SpareKnife;
    public GameObject MeshSpareKnife;

    public BattleAI AI;

    public float AfterThrowTime;
    private float AfterThrowTimer;
    private bool AfterThrow;

    private void Start()
    {
        SetSpareState(false);
    }
    public void SetAnimationState(bool Set)
    {
        AnimationKnife.SetActive(Set);
        MeshAnimationKnife.SetActive(Set);
    }
    public void SetSpareState(bool Set)
    {
        SpareKnife.SetActive(Set);
        MeshSpareKnife.SetActive(Set);
    }
    public void SendWeapon(Transform NewPos)
    {
        SpareKnife.transform.position = HandPos.position;
        CurrentState = State.Sending;
        Objective = NewPos;
        SentPosition = HandPos.position;

        SetAnimationState(false);
        SetSpareState(true);
    }
    void Update()
    {
        if(AfterThrow == true)
        {
            AfterThrowTimer += Time.deltaTime;
            if (AfterThrowTimer > AfterThrowTime)
            {
                AfterThrowTimer = 0f;
                AfterThrow = false;

                SetSpareState(false);
                SetAnimationState(true);
            }
        }
        
        if (CurrentState == State.Sending)
        {

            SpareKnife.transform.LookAt(Objective.position);
            SpareKnife.transform.rotation = Quaternion.Euler(SpareKnife.transform.rotation.x, SpareKnife.transform.rotation.y + 90, SpareKnife.transform.rotation.z);
            Vector3 Direction = (Objective.position - SentPosition).normalized;
            SpareKnife.transform.position = SpareKnife.transform.position + (Direction * (SendSpeed * Time.deltaTime));
            if (Vector3.Distance(SpareKnife.transform.position, Objective.position) < MinDistance)
            {
                AI.DoDamage();
                CurrentState = State.Returning;
            }
        }
        else if(CurrentState == State.Returning)
        {
            Vector3 Direction = -(Objective.position - SentPosition).normalized;
            SpareKnife.transform.position = SpareKnife.transform.position + (Direction * (ReturnSpeed * Time.deltaTime));
            if (Vector3.Distance(SpareKnife.transform.position, Objective.position) < MinDistance * 2)
            {
                CurrentState = State.None;
                AfterThrow = true;
            }
        }
        else if (CurrentState == State.None)
        {
            //SpareKnife.transform.position = HandPos.position;
            //SpareKnife.transform.rotation = HandPos.rotation;
            
        }
    }


    ///
}
