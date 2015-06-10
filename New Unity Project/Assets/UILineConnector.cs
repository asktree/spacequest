using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UILineConnector : MonoBehaviour {
	public Vector3 targetPoint;

	void Update () {
		Vector3 diff = targetPoint - transform.localPosition;
		if(diff.sqrMagnitude != 0)
		{
			float rot = Mathf.Atan(diff.y/diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f,0f,rot);
			var rectTransform = gameObject.GetComponent<RectTransform> ();
			rectTransform.sizeDelta = new Vector2(2*(diff.magnitude-diff.magnitude%20),rectTransform.sizeDelta.y);
		}
	}
}
