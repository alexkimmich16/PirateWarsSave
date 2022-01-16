using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public Type2 type;
	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;

	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}

}
public enum Type2 { Small, Big, Cannon }
