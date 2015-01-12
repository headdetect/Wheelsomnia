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
	public GameObject wall;
	
	private float cameraMinX, cameraMinY, cameraMaxX, cameraMaxY;

    // Bug in unity...
    private void _loadMaterial()
    {
        _PlayerRigidBody.collider2D.enabled = true;
    }

    private void _prepareMaterial()
    {
        _PlayerRigidBody.collider2D.enabled = false;
    }

    void Start()
    {
        _PlayerRigidBody = rigidbody2D;

		cameraMinY = Camera.main.transform.position.y - Camera.main.orthographicSize;
		cameraMinX = Camera.main.transform.position.x - Camera.main.orthographicSize;
		cameraMaxX = Camera.main.transform.position.x + Camera.main.orthographicSize;
		cameraMaxY = Camera.main.transform.position.y + Camera.main.orthographicSize;

		Debug.Log (cameraMinX + " : " + cameraMinY + "       " + cameraMaxX + " : " + cameraMaxY);

		var wall = GameObject.FindGameObjectWithTag ("_LEVELBLOCK");
		Instantiate(wall, new Vector3(cameraMinX, cameraMinY, 0), Quaternion.identity);
		Instantiate (wall, new Vector3 (cameraMaxX, cameraMaxY, 0), Quaternion.identity);
    }

    void Update()
    {
        // Attach camera to ball //
        //if (followScript == null)
        //	followScript = Camera.main.GetComponent<SmoothFollow> ();
        //followScript.target = transform;
        if (Input.GetKey(KeyCode.Space) && !justLetGoOfSpace)
        {
            if (!firstPass)
            {
                Debug.Log("Just pressed space");
                firstPass = true;
            }
            _PlayerRigidBody.angularVelocity = Mathf.Max(_PlayerRigidBody.angularVelocity - speed, -maxSpeed);
            currentAngularVelocity = _PlayerRigidBody.angularVelocity;
        }
        else
        {
            if (!justLetGoOfSpace && firstPass)
            {
                justLetGoOfSpace = true;
                Debug.Log("Just un-pressed space");

                _prepareMaterial();
                _PlayerRigidBody.collider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Wheel");
                _PlayerRigidBody.AddForce(new Vector2(-_PlayerRigidBody.angularVelocity * .99f /* The radius */ * multiplyer, 0));
                _loadMaterial();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            isZoomedOut = !isZoomedOut;
            if (isZoomedOut)
            {
                camera.orthographicSize = 30f;
            }
            else
            {
                camera.orthographicSize = 70f;
            }
        }

		/*if (transform.position.x <= cameraMinX || transform.position.x >= cameraMaxX) {
			var vel = _PlayerRigidBody.velocity;
			vel.x *= -0.8f;
			_PlayerRigidBody.velocity = vel;
		}
		if (transform.position.y <= cameraMinY || transform.position.y >= cameraMaxY) {
			var vel = _PlayerRigidBody.velocity;
			vel.y *= -0.8f;
			_PlayerRigidBody.velocity = vel;
		}

		var pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, cameraMinX, cameraMaxX);
		pos.y = Mathf.Clamp(pos.y, cameraMinY, cameraMaxY);

		transform.position = pos;*/

		var gui = Camera.main.GetComponent<GameGui>();
		if (gui != null && !gui.dead && (transform.position.x <= cameraMinX || transform.position.x >= cameraMaxX || 
		    transform.position.y <= cameraMinY || transform.position.y >= cameraMaxY)) {
			gui.dead = true;
			gui.lastDead = (Time.time * 1000f);
		}
    }
}
