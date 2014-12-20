using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {

	Rect windowRect = new Rect(20,20,120,50);
	bool found = false;

	void OnTriggerEnter2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Player") {
			found = true;
		}
	}

	void OnTriggerExit2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Player") {
			found = false;
		}
	}


	void OnGUI() {
		if (found)
			windowRect = GUI.Window (0, windowRect, WinningFunction, "You won!");
	}

	void WinningFunction(int windowID) {
		if (GUI.Button (new Rect(10,20,100,20), "You won!"))
		{
		    print("yes");
		    Application.LoadLevel(1);
		}
	}
}
