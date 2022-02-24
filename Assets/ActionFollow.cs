using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFollow : MonoBehaviour
{
    public Vector2 Max;
    public Vector2 Min;

    private Vector2 CurrentMin;
    private Vector2 CurrentMax;

    private Vector3 ObjectivePosition;

    public float RotateSpeed;
    public float MoveSpeed;

    //public Transform Objective;

    public float CameraDistance;

    public float WallCheckAdd;

    public bool Testing;

    public Transform TestObj;

    private void Update()
    {
        CheckDistance();
    }
    public void CheckDistance()
    {
        CurrentMin = Vector2.zero; CurrentMax = Vector2.zero;
        for (int i = 0; i < BattleController.instance.Friend.Count; i++)
            CheckLimits(BattleController.instance.Friend[i].transform);
        for (int i = 0; i < BattleController.instance.Enemy.Count; i++)
            CheckLimits(BattleController.instance.Enemy[i].transform);
        if (Testing == true)
            ObjectivePosition = TestObj.position;
        else
            ObjectivePosition = new Vector3((CurrentMin.x + CurrentMax.x) / 2, 0, (CurrentMin.y + CurrentMax.y) / 2);

        //Objective.position = ObjectivePosition;

        transform.position = Vector3.Lerp(transform.position, FindPosition(), MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(ObjectivePosition - transform.position), RotateSpeed * Time.deltaTime);

        void CheckLimits(Transform Check)
        {
            if (Check.position.x < CurrentMin.x)
                CurrentMin.x = Check.position.x;
            if (Check.position.z < CurrentMin.y)
                CurrentMin.y = Check.position.z;
            if (Check.position.x > CurrentMax.x)
                CurrentMax.x = Check.position.x;
            if (Check.position.z > CurrentMax.y)
                CurrentMax.y = Check.position.z;
        }
        Vector3 FindPosition()
        {
            ///check for furthest point from wall
            ///check buttom, left right top walls each indiviidually
            Vector2 Best = Vector2.zero;
            float Closest = 1000;
            float CurrentX = Min.x;
            float CurrentY = Min.y;

            //x rising
            while (CurrentX < Max.x)
            {
                if (Closest < Vector3.Distance(new Vector3(CurrentX, 0, Max.y), ObjectivePosition))
                {
                    Closest = Vector3.Distance(new Vector3(CurrentX, 0, Max.y), ObjectivePosition);
                    Best = new Vector2(CurrentX, Max.y);
                }
                CurrentX += WallCheckAdd;
            }
            CurrentX = Min.x;
            while (CurrentX < Max.x)
            {
                if (Closest < Vector3.Distance(new Vector3(CurrentX, 0, Min.y), ObjectivePosition))
                {
                    Closest = Vector3.Distance(new Vector3(CurrentX, 0, Min.y), ObjectivePosition);
                    Best = new Vector2(CurrentX, Min.y);
                }
                CurrentX += WallCheckAdd;
            }

            //y rising
            while (CurrentY < Max.y)
            {
                if (Closest < Vector3.Distance(new Vector3(Max.x, 0, CurrentY), ObjectivePosition))
                {
                    Closest = Vector3.Distance(new Vector3(Max.x, 0, CurrentY), ObjectivePosition);
                    Best = new Vector2(Max.x, CurrentY);
                }
                CurrentY += WallCheckAdd;
            }
            CurrentY = Min.y;
            while (CurrentY < Max.y)
            {
                if (Closest < Vector3.Distance(new Vector3(Min.x, 0, CurrentY), ObjectivePosition))
                {
                    Closest = Vector3.Distance(new Vector3(Min.x, 0, CurrentY), ObjectivePosition);
                    Best = new Vector2(Min.x, CurrentY);
                }
                CurrentY += WallCheckAdd;
            }

            Vector3 Direction = (new Vector3(Best.x, 0 , Best.y) - ObjectivePosition).normalized;
            Vector3 FinalPos = ObjectivePosition + (Direction * CameraDistance);
            return FinalPos;
        }
    }
}
