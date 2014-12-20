using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 0.0f;


	// Update is called once per frame
	void Update () {
		// Early out if we don't have a target
		if (!target)
			return;

		var wantedHeight = target.position.y + height;
		var currentHeight = transform.position.y;

		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		
		// Set the height of the camera
		transform.position = new Vector3(target.position.x, currentHeight, -10f);

		
		// Always look at the target
		transform.LookAt (target);

		transform.rotation = new Quaternion (0, 0, 0, 0);
	}
}