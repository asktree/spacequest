using UnityEngine;
using System.Collections;

public class TogglePanel : MonoBehaviour {

	public void ToggleAppearance () {
		gameObject.SetActive (!gameObject.activeSelf);
	}
}
