using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton

	public static Inventory instance;

	void Awake()
	{
		instance = this;
	}

	#endregion
	
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 10;  // Amount of item spaces

    // Our current list of items in the inventory
    //public List<AllInfo.AllGameInfo> AllItems = new List<AllInfo.AllGameInfo>();
    private void Start()
    {
		/*
		for (int i = 0; i < AllInfo.instance.GamePirates.Count; i++)
        {
			AllItems.Add(AllInfo.instance.GamePirates[i]);
		}
		for (int i = 0; i < AllInfo.instance.GameWeapons.Count; i++)
		{
			AllItems.Add(AllInfo.instance.GameWeapons[i]);
		}
		for (int i = 0; i < AllInfo.instance.GameArmours.Count; i++)
		{
			AllItems.Add(AllInfo.instance.GameArmours[i]);
		}
		*/
	}

	/*
    // Add a new item if enough room
    public void Add(Item item)
	{
		if (item.showInInventory)
		{
			if (items.Count >= space)
			{
				//Debug.Log("Not enough room.");
				
			}

			items.Add(item);

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
	}

	// Remove an item
	public void Remove(Item item)
	{
		//Debug.Log("REMOVE1");
		items.Remove(item);

		if (onItemChangedCallback != null)
        {
			onItemChangedCallback.Invoke();
			//Debug.Log("REMOVE2");
		}
			
	}
	*/
}
