﻿using UnityEngine;
using System.Collections;

public class MenuStartBtn : MonoBehaviour {


	public Color defaultColor;
	
	void OnMouseEnter() {
		guiText.color = Color.red;
	}
	
	void OnMouseExit() {
		guiText.color = defaultColor;
	}

	void OnMouseUp() {
		Application.LoadLevel(2);
	}
}
