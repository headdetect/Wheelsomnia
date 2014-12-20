using UnityEngine;
using System.Collections;

public class MenuExitBtn : MonoBehaviour {

	void OnMouseEnter() {
		guiText.color = Color.blue;
	}
	
	void OnMouseExit() {
		guiText.color = Color.white;
	}
	
	void OnMouseUp() {
		Application.Quit();
	}
}
