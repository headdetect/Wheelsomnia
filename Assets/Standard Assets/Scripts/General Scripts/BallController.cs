using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{

	public float speed = 150f;
	public float maxSpeed = 1500f;
	public float currentAngularVelocity = 0;
	public float multiplyer = 10f;
	private bool firstPass = false;
	private bool justLetGoOfSpace = false;
	private bool isZoomedOut = false;
	private Rigidbody2D _PlayerRigidBody;
	
	// Bug in unity...
	private void _loadMaterial ()
	{
		_PlayerRigidBody.collider2D.enabled = true;
	}
	
	private void _prepareMaterial ()
	{
		_PlayerRigidBody.collider2D.enabled = false;
	}

	void Start ()
	{
		_PlayerRigidBody = rigidbody2D;
	}

	void Update ()
	{
		// Attach camera to ball //
		//if (followScript == null)
		//	followScript = Camera.main.GetComponent<SmoothFollow> ();
		//followScript.target = transform;
		if (Input.GetKey (KeyCode.Space) && !justLetGoOfSpace) {
			if (!firstPass) {
				Debug.Log ("Just pressed space");
				firstPass = true;
			}
			_PlayerRigidBody.angularVelocity = Mathf.Max (_PlayerRigidBody.angularVelocity - speed, -maxSpeed);
			currentAngularVelocity = _PlayerRigidBody.angularVelocity;
		} else {
			if (!justLetGoOfSpace && firstPass) {
				justLetGoOfSpace = true;
				Debug.Log ("Just un-pressed space");
	
				_prepareMaterial ();
				_PlayerRigidBody.collider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("Wheel");
				_PlayerRigidBody.AddForce (new Vector2 (-_PlayerRigidBody.angularVelocity * .99f /* The radius */ * multiplyer, 0));
				_loadMaterial ();
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
}
