using UnityEngine;
using System.Collections;

public class GameGui : MonoBehaviour {
	private const bool isDebugging = true;
	private float rpm;
	private GUIStyle rpmStyle;
	private GameObject ball;


	void OnGUI()
	{
		if (isDebugging) DrawDebugMenu (); 

		GUI.Label (new Rect (417, 473, 150, 50), "RPM: " + rpm, rpmStyle);
	}

	void DrawDebugMenu() {
		int tools = GameObject.FindGameObjectsWithTag("_PLAYERBLOCK").Length;
		int total = GameObject.FindObjectsOfType(typeof(MonoBehaviour)).Length;
		
		Color temp = GUI.backgroundColor;
		GUI.backgroundColor = new Color(0f, 1f / 188f, 1f / 212f, 0.7f);
		
		GUI.Box(new Rect(0, 0, 150, 100), "Debug Tools");
		GUI.Label(new Rect(5, 30, 150, 50), "FPS: " + (1f / Time.deltaTime));
		GUI.Label(new Rect(5, 45, 150, 50), "# of GameObjects: " + total);
		GUI.Label(new Rect(5, 60, 150, 50), "# of Tools: " + tools);
		
		GUI.backgroundColor = temp;
	}

	void Start() {
		rpmStyle = new GUIStyle ();
		rpmStyle.fontSize = 16;
		rpmStyle.normal.textColor = Color.white;

		ball = GameObject.FindGameObjectWithTag ("_PLAYER");

	}

	void Update () {
		if (ball == null) {
			ball = GameObject.FindGameObjectWithTag ("_PLAYER");
		} else {
			rpm = Mathf.Abs(ball.rigidbody2D.angularVelocity * 9.55414012739f);
		}
	}
}
