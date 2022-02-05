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
    public List<Transform> cameraTransforms;

    public float MoveSpeed;
    public float RotateSpeed;

    public bool Arrived = false;

    public float MinMenuDistance;

    public void MoveTo(Interactable type)
    {
        currentPlace = type;
        Arrived = false;
    }
    void Update()
    {
        int TypeNum = (int)currentPlace;
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTransforms[TypeNum].rotation, RotateSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, cameraTransforms[TypeNum].position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, cameraTransforms[TypeNum].position) < MinMenuDistance && Arrived == false)
        {
            Arrived = true;
            if (currentPlace == Interactable.Main)
            {
                //SceneLoader.instance.LoadScene();
            }
            else if (currentPlace == Interactable.Shop)
            {
                SceneLoader.instance.LoadScene("Shop");
            }
            else if (currentPlace == Interactable.Trident)
            {
                SceneLoader.instance.LoadScene("Trident");
            }
            else if (currentPlace == Interactable.Battle)
            {
                SceneLoader.instance.LoadScene("CharacterSelection");
            }
            else if (currentPlace == Interactable.Statue)
            {
                //SceneLoader.instance.LoadScene("CharacterSelection");
            }
        }
    }
}
