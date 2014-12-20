using UnityEngine;
using System.Collections;

public class MagnetRepelBehavior : MonoBehaviour {

	void OnTriggerStay2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Player") {
			float distance = Mathf.Abs(Mathf.Sqrt (Mathf.Pow ((transform.position.x - otherObj.transform.position.x),2f) + Mathf.Pow ((transform.position.y - otherObj.transform.position.y),2f)));
			Vector2 forceDirection = transform.position - otherObj.transform.position ;
			otherObj.rigidbody2D.AddForce(forceDirection.normalized * ((distance - 13)/18), ForceMode2D.Impulse);
		}
	}
}
