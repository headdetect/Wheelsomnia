using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    Image toolImage;
    Text toolCount;

    // Use this for initialization
    void Start()
    {
        toolImage = GetComponentInChildren<Image>();
		toolCount = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnMouseDown() 
	{
		if (toolCount != null) 
		{
			Debug.Log ("toolCount: " + toolCount.text);
			if (int.Parse (toolCount.text) > 0) 
			{
				GameObject[] playerBlocks = GameObject.FindGameObjectsWithTag("_PLAYERBLOCK");
				PlayerBlock playerBlock;
				foreach (GameObject b in playerBlocks)
				{
					playerBlock = b.GetComponent<PlayerBlock>();
					if (playerBlock != null)
					{
						playerBlock.SetMode(false);					
                        Debug.Log ("Disabled: " + b.gameObject.name);
                    }
                }

                GameObject block = (GameObject)Instantiate (Resources.Load ("Prefabs/MagnetPush"));
				block.tag = "_PLAYERBLOCK";
				block.name = "MagnetPush-"+Time.time;
				block.AddComponent ("PlayerBlock");
				block.transform.position = new Vector3 (44, 0, 0);
				Debug.Log ("Created block");
				toolCount.text = "" + (int.Parse (toolCount.text) - 1);
			}
		}
		Debug.Log ("toolbox click done");
	}    
}
