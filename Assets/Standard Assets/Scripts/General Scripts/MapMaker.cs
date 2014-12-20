using UnityEngine;
using System.Collections;

enum GameObjectTypes {
	Nothing = 0,
	Wall = 1,
	Ball = 2,
	Lava = 3,
	Flag = 4
}

public class MapMaker : MonoBehaviour {
	
	public GameObject flag;
	public GameObject ball;
	public GameObject wall;

	private GameObject _PlayerBall;
	private Rigidbody2D _PlayerRigidBody;

	private int cameraSize = 70;
	
	// Use this for initialization
	void Start () {
		var testFile = Resources.Load<TextAsset> ("test");
		
		var mapInfo = SimpleJSON.JSON.Parse (testFile.text);

		int x = 0;
		int y = 0;

		int objectScale = 1;
		int objectScaleLocal = 20;

		foreach (SimpleJSON.JSONNode t in mapInfo["layers"].AsArray) {
			SimpleJSON.JSONArray data = t["data"].AsArray;
			int height = t["height"].AsInt;
			int width = t["width"].AsInt;

			cameraSize = Mathf.Max(cameraSize, objectScaleLocal * width);

			for(int h = 0; h < height; h++) {
				for(int w = 0; w < width; w++) {
					int index = w * width + h;
					GameObjectTypes type = (GameObjectTypes)data[index].AsInt;
					GameObject obj = null;
					switch(type) {
						case GameObjectTypes.Ball:
							obj = Instantiate(ball, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
							obj.transform.localScale = new Vector3(1, 1, 1);
							obj.transform.position = new Vector3(x, y, 0);
							_PlayerBall = obj;
							_PlayerRigidBody = _PlayerBall.rigidbody2D;
							break;
						case GameObjectTypes.Flag:
							obj = Instantiate(flag, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
							obj.transform.localScale = new Vector3(1, 1, 1);
							obj.transform.position = new Vector3(x, y, 0);
							break;
						case GameObjectTypes.Wall:
							obj = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
							obj.transform.localScale = new Vector3(1, 1, 1);
							obj.transform.position = new Vector3(x, y, 0);
							break;
					}

					x += objectScale * objectScaleLocal / width;
				}
				y += objectScale * objectScaleLocal / height;
			}
		}
	}

	public float speed = 150f;
	public float maxSpeed = 1500f;
	public float currentAngularVelocity = 0;
	public float multiplyer = 10f;

	private bool firstPass = false;
	private bool justLetGoOfSpace = false;


	private bool isZoomedOut = false;

	private SmoothFollow followScript = null;

	// Update is called once per frame
	void Update () {
		// Attach camera to ball //
		if (followScript == null)
			followScript = Camera.main.GetComponent<SmoothFollow> ();
		followScript.target = _PlayerBall.transform;
		if (Input.GetKey (KeyCode.Space) && !justLetGoOfSpace) {
			if(!firstPass) {
				Debug.Log ("Just pressed space");
				firstPass = true;
			}
			_PlayerRigidBody.angularVelocity = Mathf.Max (_PlayerRigidBody.angularVelocity - speed, -maxSpeed);
			currentAngularVelocity = _PlayerRigidBody.angularVelocity;
		} else {
			if(!justLetGoOfSpace && firstPass) {
				justLetGoOfSpace = true;
				Debug.Log ("Just un-pressed space");

				_prepareMaterial();
				_PlayerRigidBody.collider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Wheel");
				_PlayerRigidBody.AddForce(new Vector2(-_PlayerRigidBody.angularVelocity * .99f /* The radius */ * multiplyer, 0));
				_loadMaterial();
			}
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			isZoomedOut = !isZoomedOut;
			if (isZoomedOut) {
				camera.orthographicSize = 30f;
			} else {
				camera.orthographicSize = 70f;
			}
		}
	}

	// Bug in unity...
	private void _loadMaterial() {
		_PlayerRigidBody.collider2D.enabled = true;
	}

	private void _prepareMaterial() {
		_PlayerRigidBody.collider2D.enabled = false;
	}
}