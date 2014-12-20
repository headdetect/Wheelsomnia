using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {
	
	public GameObject flag;
	public GameObject ball;
	public GameObject wall;
	
	// Use this for initialization
	void Start () {
		var testFile = Resources.Load<TextAsset> ("test");
		
		var mapInfo = SimpleJSON.JSON.Parse (testFile.text);
		
		float tileXSize = mapInfo ["tilewidth"].AsFloat;
		float tileYSize = mapInfo ["tileheight"].AsFloat;
		
		float yadd = mapInfo["height"].AsFloat * tileYSize;
		
		foreach (SimpleJSON.JSONNode t in mapInfo["layers"].AsArray) {
			foreach (SimpleJSON.JSONNode o in t["objects"].AsArray) {
				string type = o["type"].Value;
				
				float x = o["x"].AsFloat;
				float y = -o["y"].AsFloat + yadd;
				float width = o["width"].AsFloat;
				float height = o["height"].AsFloat;
				
				x += (width / 2f);
				y -= (height / 2f);
				
				width /= tileXSize;
				height /= tileYSize;
				if (type == "Flag") {
					GameObject obj = Instantiate(flag, new Vector3(x / tileXSize, y / tileYSize, 0), Quaternion.identity) as GameObject;
					obj.transform.localScale = new Vector3(width, height, 1);
					obj.transform.position = new Vector3(x / tileXSize, y / tileYSize, 0);
				} else if (type == "Ball Start") {
					GameObject obj = Instantiate(ball, new Vector3(x / tileXSize, y / tileYSize, 0), Quaternion.identity) as GameObject;
					obj.AddComponent<Rigidbody>();
					obj.transform.localScale = new Vector3(width, height, 1);
					obj.transform.position = new Vector3(x / tileXSize, y / tileYSize, 0);
				} else {
					GameObject obj = Instantiate(wall, new Vector3(x / tileXSize, y / tileYSize, 0), Quaternion.identity) as GameObject;
					obj.transform.localScale = new Vector3(width, height, 1);
					obj.transform.position = new Vector3(x / tileXSize, y / tileYSize, 0);
				}
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
