﻿using UnityEngine;
using System.Collections;

public class PlayerBlock : MonoBehaviour {
	
	Sprite sprite1 = Resources.Load<Sprite>("Graphics/Textures/ToolHandle_Move");
	Sprite sprite2 = Resources.Load<Sprite>("Graphics/Textures/ToolHandle_Rotate");

	private SpriteRenderer spriteRenderer; 
	private bool isEditMode  = false;
	private bool transformMode = true;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		isEditMode = true;
		ChangeMode ();
		Debug.Log ("started block");
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
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null) 
		{
			if (mode)
			{
				spriteRenderer.sprite = sprite1;
				Debug.Log ("SetMode: True");
			}
			else
			{
				spriteRenderer.sprite = null;
				Debug.Log ("SetMode: false");
			}								
		}
	}

	public void ChangeMode() {
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
			Debug.Log ("ChangeMode: true");

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
