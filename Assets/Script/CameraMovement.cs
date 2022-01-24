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
            ResetAllMenu();
            if (currentPlace == Interactable.Main)
            {
                MenuUiManager.instance.StartMenu(true);
            }
            else if (currentPlace == Interactable.Shop)
            {
                MenuUiManager.instance.ShopMenu(true);
            }
            else if (currentPlace == Interactable.Trident)
            {
                MenuUiManager.instance.TridentMenu(true);
            }
            else if (currentPlace == Interactable.Battle)
            {
                MenuUiManager.instance.BattleMenu(true);
            }
        }
    }

    public void ResetAllMenu()
    {
        MenuUiManager.instance.TridentMenu(false);
        MenuUiManager.instance.StartMenu(false);
        MenuUiManager.instance.ShopMenu(false);
        MenuUiManager.instance.BattleMenu(false);
    }
}
