using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var testFile = Resources.Load<TextAsset> ("test");

		var mapInfo = SimpleJSON.JSON.Parse (testFile.text);
		foreach (SimpleJSON.JSONNode t in mapInfo["layers"].AsArray) {
			foreach (SimpleJSON.JSONNode o in t["objects"].AsArray) {
				float x = o["x"].AsFloat;
				float y = o["y"].AsFloat;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
