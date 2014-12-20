using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var testFile = Resources.Load<TextAsset> ("test");

		var mapInfo = SimpleJSON.JSON.Parse (testFile.text);
		float yadd = mapInfo["height"].AsFloat * mapInfo["tileheight"].AsFloat;
		foreach (SimpleJSON.JSONNode t in mapInfo["layers"].AsArray) {
			foreach (SimpleJSON.JSONNode o in t["objects"].AsArray) {
				float x = o["x"].AsFloat;
				float y = -o["y"].AsFloat + yadd;
				float width = o["width"].AsFloat;
				float height = o["height"].AsFloat;

				x += (width / 2f);
				y -= (height / 2f);

				width /= 16f;
				height /= 16f;

				GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
				obj.transform.localScale = new Vector3(width, height, 1);
				obj.transform.position = new Vector3(x / 10f, y / 10f, 0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
