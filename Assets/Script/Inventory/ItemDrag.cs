using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public static ItemDrag Instance { get; private set; }

	public Image icon;

	public int DragNum;

	public Item item;  // Current item in the slot
	[SerializeField] private Canvas canvas;

	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	private Vector2 StartingPos;
	//private WheelScript;
	//public Item item;

	//public CannonItem Cannon;
	//public Sails SailItem;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		StartingPos = rectTransform.anchoredPosition;
		Instance = this;

		//icon.sprite = item.icon;
		//icon.enabled = true;
	}
	
	public Item GetItem()
    {
		return item;
    }
	public void ItemToNull()
    {
		//Debug.Log("tonull");
		icon.sprite = null;
		//destroy
	}
	public void RecieveItem(Item SentItem)
    {
		//Debug.Log("RECIEVE");
		item = SentItem;
		icon.sprite = item.icon;
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		//Debug.Log("OnBeginDrag");
		canvasGroup.alpha = .6f;
		canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("OnDrag");
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("OnEndDrag");
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		//rectTransform.localPosition = Vector3.zero;
		rectTransform.anchoredPosition = StartingPos;
	}
	///*

	//public ShipEquipmentSlot.EquipSlot GetItemType()
	//{

	//}

	//*/
}
