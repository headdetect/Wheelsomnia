using UnityEngine;
using System.Collections;

public class PauseBtn : MonoBehaviour {

	public bool isPaused = false;
	
	void OnMouseUp() {
		isPaused = !isPaused;

		if (isPaused) {
			Time.timeScale = 0;		
		}						
		else 
		{
			Time.timeScale = 1;
		}
	}
}

