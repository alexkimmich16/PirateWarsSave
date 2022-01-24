using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Singleton
    public static CameraMovement instance;
    void Awake() { instance = this; }
    #endregion

    public Interactable currentPlace;

    public Transform Camera;
    public List<Transform> cameraTransforms;

    public float MoveSpeed;
    public float RotateSpeed;

    public void MoveTo(Interactable type)
    {
        currentPlace = type;
    }
    void Update()
    {
        int TypeNum = (int)currentPlace;
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTransforms[TypeNum].rotation, RotateSpeed * Time.deltaTime);
        Camera.position = Vector3.Lerp(Camera.position, cameraTransforms[TypeNum].position, MoveSpeed * Time.deltaTime);
    }
}
