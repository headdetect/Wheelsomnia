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

                GameObject block = (GameObject)Instantiate (Resources.Load ("MagnetPush"));
				block.tag = "_PLAYERBLOCK";
				block.AddComponent ("PlayerBlock");
				block.AddComponent ("SpriteRenderer");
				block.transform.position = new Vector3 (44, 0, 0);

				playerBlock = block.GetComponent<PlayerBlock>();
				playerBlock.SetMode (true);
				Debug.Log ("Created block");
			}
		}
		Debug.Log ("toolbox click done");
	}    
}
