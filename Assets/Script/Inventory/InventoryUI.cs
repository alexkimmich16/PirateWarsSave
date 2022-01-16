using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* This object manages the inventory UI. */

public class InventoryUI : MonoBehaviour
{
	#region Singleton

	public static InventoryUI instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	public GameObject inventoryUI;  // The entire UI
	public GameObject AiMenuUi;
	public GameObject ObjectPlacement;
	public GameObject ShopMenu;
	public Transform itemsParent;   // The parent object of all the items
	public GameObject Dark;
	public GameObject SpeechLog;

	public GameObject Crosshair;

	//opens mouse
	public bool MenuActive;
	public bool AllActive;

	public bool ShopReallyActive;
	public bool ShopActive;

	public bool InventoryActive;
	public bool AiActive;
	public bool PlacementActive;
	public bool SpeechActive;



	Inventory inventory;    // Our current inventory

	void Start()
	{
		if (inventoryUI.activeSelf == true)
		{
			//inventoryUI.active = false;
			//AiMenuUi.active = false;
			InventoryActive = true;
		}
		if (AiMenuUi.active == true)
        {
			AiActive = true;
		}
		if (ObjectPlacement.activeSelf == true)
        {
			PlacementActive = true;
		}
		if (SpeechLog.activeSelf == true)
		{
			SpeechActive = true;
		}
	}

	public void SwitchCross(bool isVisable)
    {
		if(isVisable == true)
        {
			Crosshair.SetActive(true);
		}
		else if(isVisable == false)
		{
			Crosshair.SetActive(false);
        }
	}


	// Check to see if we should open/close the inventory
	void Update()
	{
		if(InventoryActive == true)
        {
			inventoryUI.SetActive(true);
		}
		else if(InventoryActive == false)
        {
			inventoryUI.SetActive(false);
		}
        if (AiActive == true)
        {
			AiMenuUi.SetActive(true);
		}
		else if (AiActive == false)
		{
			AiMenuUi.SetActive(false);
		}
		if (PlacementActive == true)
        {
			ObjectPlacement.SetActive(true);
		}
		else if (PlacementActive == false)
		{
			ObjectPlacement.SetActive(false);
		}
		if (SpeechActive == true)
		{
			SpeechLog.SetActive(true);
		}
		else if (SpeechActive == false)
		{
			SpeechLog.SetActive(false);
		}



		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			InventoryActive = !InventoryActive;
			//MenuActive = !MenuActive;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			AiActive = !AiActive;
		}
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			PlacementActive = !PlacementActive;
		}
        if (Input.GetKeyDown(KeyCode.Z) && ShopReallyActive == true && ShopActive == true)
        {
			ShopReallyActive = false;

		}
		if (Input.GetKeyDown(KeyCode.X) && ShopReallyActive == false && ShopActive == true)
		{
			ShopReallyActive = true;

		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			SpeechActive = !SpeechActive;
		}

		//if a menu is open
		if (ShopReallyActive == true || MenuActive == true)
        {
			Cursor.lockState = CursorLockMode.Confined;
			Crosshair.SetActive(false);
		}
        else
        {
			Cursor.lockState = CursorLockMode.Locked;
			Crosshair.SetActive(true);
		}

		if (ShopReallyActive == false)
		{ 
			if (InventoryActive == true && AiActive == true && PlacementActive == true)
			{
				AllActive = true;
				//Dark.SetActive(true);
			}
			else
			{
				AllActive = true;
				//Dark.SetActive(false);
			}

			//if any are true
			if (InventoryActive == true || AiActive == true || PlacementActive == true)
			{
				MenuActive = true;
				//Debug.Log("menu");
			}

			else if (InventoryActive == false && AiActive == false && PlacementActive == false)
			{
				MenuActive = false;
			}
		}

        else if (ShopActive == true)
        {
			MenuActive = false;
			AllActive = false;
			PlacementActive = false;
			
		}

		if (ShopReallyActive == true)
		{
			ShopMenu.SetActive(true);
		}
		else if (ShopReallyActive == false)
		{
			ShopMenu.SetActive(false);
		}

	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	

}
