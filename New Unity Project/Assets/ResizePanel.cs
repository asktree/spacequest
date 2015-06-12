using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
	
	public Vector2 minSize = new Vector2(100,100);
	public Vector2 maxSize = new Vector2(1000,1000);
	
	private RectTransform rectTransform;
	private Vector2 currentPointerPosition;
	private Vector2 previousPointerPosition;

	private Vector2 startPointerPosition;
	private bool inDrag = false;
	private int dragState;

	void Awake () {
		rectTransform = transform.GetComponent<RectTransform>();
	}
	
	public void OnPointerDown (PointerEventData data) {
		rectTransform.SetAsLastSibling();
		RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);
		if (inDrag == false) {
			RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, data.position, data.pressEventCamera, out startPointerPosition);
			dragState = TestClickBounds();
			if(dragState < 0)
			{
				return;
			}


			inDrag = true;
		}
	}

	public void OnPointerUp (PointerEventData data) {
		inDrag = false;
	}

	private int TestClickBounds() {
		bool lowx = startPointerPosition.x <= (-1*gameObject.GetComponent<RectTransform>().sizeDelta.x/2)+8;
		bool lowy = startPointerPosition.y <= (-1 * gameObject.GetComponent<RectTransform> ().sizeDelta.y / 2) + 8;
		bool highx = startPointerPosition.x > (gameObject.GetComponent<RectTransform>().sizeDelta.x/2)-8;
		bool highy = startPointerPosition.y > (gameObject.GetComponent<RectTransform> ().sizeDelta.y / 2) - 16;
		int state = -1;

		//0 1 2
		//3 4 5
		//6 7 8

		if (lowx)
			state = 0;
		else if (highx)
			state = 2;
		else
			state = 1;

		if (lowy)
			state += 6;
		else if (highy)
			state += 0;
		else
			state += 3;

		if (state < 0)
			state = -1;

		print (state);
		return state;
	}
	
	public void OnDrag (PointerEventData data) {
		if (rectTransform == null || !inDrag)
			return;

		
		Vector2 sizeDelta = rectTransform.sizeDelta;
		
		RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
		Vector2 resizeValue = currentPointerPosition - previousPointerPosition;


		print (dragState);
		sizeDelta += new Vector2 ((dragState%3==0 ? -1 : (dragState%3==2 ? 1 : 0))*resizeValue.x, (dragState > 5 ? -1 : (dragState < 3 ? 1 : 0))*resizeValue.y);
		sizeDelta = new Vector2 (
			Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x),
			Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y)
			);
		
		rectTransform.sizeDelta = sizeDelta;
		
		previousPointerPosition = currentPointerPosition;
	}
}