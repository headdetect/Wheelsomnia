using UnityEngine;
using System.Collections;

public class PlayerBlock : MonoBehaviour {
	
	public Sprite sprite1;
	public Sprite sprite2;

	private SpriteRenderer spriteRenderer; 
	private bool isEditMode  = false;
	private bool transformMode = true;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite != null) 
			spriteRenderer.sprite = null;
	}

	void Update() {
		if (Input.GetKeyDown("r")) // If the space bar is pushed down
		{
			transformMode = !transformMode;
			ChangeMode (); // call method to change sprite
		}

		if (Input.GetMouseButton(0)) 
		{
			if (isEditMode)
			{
				if (transformMode)
				{
					this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
				}
				else				
				{
					Quaternion rot = Quaternion.LookRotation(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward );
					transform.rotation = rot;  
					transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z);
				}
			}
		}
		if (Input.GetMouseButtonDown (1)) 
		{
			RaycastHit2D[] hitAll = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), -Vector2.up);
			foreach (RaycastHit2D hit in hitAll) {
				if(hit.collider.gameObject.name == this.gameObject.name && hit.collider.isTrigger == false)
				{					
					isEditMode = !isEditMode;
					ChangeMode ();
					Debug.Log ("hit: " + hit.collider.gameObject.name);
				}
			}
		}
	}

	public void SetMode(bool mode) 
	{
		isEditMode = mode;
		spriteRenderer.sprite = null;
	}

	void ChangeMode() {
		Debug.Log ("ChangeMode: " + this.gameObject.name);
		if (isEditMode)
		{
			GameObject[] playerBlocks = GameObject.FindGameObjectsWithTag("_PLAYERBLOCK");
			PlayerBlock block;

			foreach (GameObject b in playerBlocks)
			{
				block = b.GetComponent<PlayerBlock>();
				if (block != null)
				{
					block.SetMode(false);					
					Debug.Log ("Disabled: " + b.gameObject.name);
				}
			}
			isEditMode = true;

			if (transformMode)
			{
				spriteRenderer.sprite = sprite1;
			}
			else
			{
				spriteRenderer.sprite = sprite2;
			}
		}
		else
		{
			spriteRenderer.sprite = null;
		}
	}

}
