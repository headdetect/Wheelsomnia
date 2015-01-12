using UnityEngine;
using System.Collections;

public class GameGui : MonoBehaviour {
	private const bool isDebugging = true;
	private float rpm;
	private GUIStyle rpmStyle;
	private GUIStyle dedStyle;
	private GameObject ball;

	public bool dead = false;
	public float lastDead;


	void OnGUI()
	{
		if (isDebugging) DrawDebugMenu (); 

		GUI.Label (new Rect (417, 473, 150, 50), "RPM: " + rpm, rpmStyle);

		if (dead) {
			float cur = Time.time * 1000f;

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "You're ded!", dedStyle);

			if (cur - lastDead > 2800) {
				dead = false;
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
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

		dedStyle = new GUIStyle ();
		dedStyle.fontSize = 32;
		dedStyle.alignment = TextAnchor.MiddleCenter; //Maybe UpperCenter ?
		dedStyle.normal.background = CreateSolidTexture (2, 2, new Color (0f, 1f / 188f, 1f / 212f, 0.8f));
		dedStyle.normal.textColor = Color.white;

		ball = GameObject.FindGameObjectWithTag ("_PLAYER");

	}

	private Texture2D CreateSolidTexture(int width, int height, Color color) {
		Color[] pix = new Color[width * height];
		for (int i = 0; i < pix.Length; i++) {
			pix[i] = color;
		}
		Texture2D toReturn = new Texture2D (width, height);
		toReturn.SetPixels (pix);
		toReturn.Apply ();
		return toReturn;
	}

	void Update () {
		if (ball == null) {
			ball = GameObject.FindGameObjectWithTag ("_PLAYER");
		} else {
			rpm = Mathf.Abs(ball.rigidbody2D.angularVelocity * 9.55414012739f);
		}
	}
}
