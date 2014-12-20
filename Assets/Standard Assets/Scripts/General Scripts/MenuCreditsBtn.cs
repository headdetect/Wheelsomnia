using UnityEngine;
using System.Collections;

public class MenuCreditsBtn : MonoBehaviour {

	void OnMouseEnter() {
		guiText.color = Color.blue;
	}
	
	void OnMouseExit() {
		guiText.color = Color.white;
	}
	
	void OnMouseUp() {
		Application.LoadLevel(3);
	}
}
