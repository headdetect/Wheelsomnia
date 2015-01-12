using UnityEngine;
using System.Collections;

public class PauseBtn : MonoBehaviour {

	bool isPaused = false;
	
	void OnMouseDown() {
		isPaused = !isPaused;
		Debug.Log ("clicked it!");
		if (isPaused) {
			Time.timeScale = 0;	
			GameObject menu = (GameObject)Instantiate (Resources.Load ("Prefabs/PauseMenu"));
			menu.tag = "_PAUSEMENU";
		}						
		else 
		{
			Time.timeScale = 1;
			GameObject menu = GameObject.FindGameObjectWithTag("_PAUSEMENU");
			if (menu != null)
				Destroy(menu, 0f);
		}
	}
}

